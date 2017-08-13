using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Infrastructures;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract;
using MDoc.Services.Contract.DataContracts;
using MDoc.Services.Contract.DataContracts.User;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class UserService : BaseService, IUserService
    {
        #region [Variable]

        private readonly IEmailService _emailService;
        #endregion

        #region [Contructor]
        public UserService(IUnitOfWork unitOfWork, IEmailService emailService) : base(unitOfWork)
        {
            _emailService = emailService;
        }

        #endregion


        public UserModel Create(UserModel model)
        {
            var password = MD5Helper.GetPassword();
            var user = new ApplicationUser()
            {
                LoginId = model.LoginId,
                UserName = model.UserName,
                IsDisabled = false,
                RegisterDate = DateTime.Now,
                Email = model.Email,
                Password = password.ToMd5()
            };
            UnitOfWork.GetRepository<ApplicationUser>().Create(user);
            UnitOfWork.SaveChanges();
            var emailToUserModel = new EmailModel()
            {
                ToAddress = model.Email,
                Subject = "[MDOC] - Register new user",
                Body =$"Hello {model.UserName}, <br/> Your login information is:<br/> LoginId: <strong>{model.LoginId}</strong> <br/> Password: <strong>{password}</strong><br/>Please change your password when you start using MDoc ASAP.<br/> If you have any issue, please contact trandev90@gmail.com for more information. <strong>Do not reply</strong> this email."
            };
            _emailService.SendEmailToUser(emailToUserModel);
            return new UserModel() {UserId = user.ApplicationUserId};
        }

        public IQueryable<UserModel> GetUsers(string query = "")
            => UnitOfWork.GetRepository<ApplicationUser>().Get(m => !m.IsDisabled)
                .Where(m => string.IsNullOrEmpty(query) || m.UserName.ToLower().Contains(query.ToLower()))
                .Select(x => new UserModel()
                {
                    UserId = x.ApplicationUserId,
                    UserName = x.UserName,
                    Email = x.Email,
                    Avatar = x.Avatar,
                    RegisterDate = x.RegisterDate,
                    LoginId = x.LoginId
                });

        public UserModel Login(LoginModel model)
        {
            var isEmail = model.LoginId.IsEmail();
            var userEntity =
                (from user in
                    UnitOfWork.GetRepository<ApplicationUser>()
                        .Get(m => !m.IsDisabled && m.Password == model.SecurePassword)
                    where (isEmail && user.Email == model.LoginId) || (!isEmail && user.LoginId == model.LoginId)
                    select user).FirstOrDefault();
            if (userEntity == null)
                return new UserModel
                {
                    UserId = 0
                };
            var result = new UserModel
            {
                UserId = userEntity.ApplicationUserId,
                UserName = userEntity.UserName,
                Avatar = userEntity.Avatar,
                Email = userEntity.Email,
                LoginId = userEntity.LoginId,
                IsSuperAdmin = UnitOfWork.GetRepository<SuperAdmin>().Get().Any(m=> m.IsSuperAdmin && m.UserId == userEntity.ApplicationUserId),
                IsCompanyAdmin = userEntity.AdministrateCompanies.Any()
            };
            return result;
        }

        public UserModel Update(UserModel model)
        {
            throw new NotImplementedException();
        }

        public bool CheckLoginId(string loginId)
            => UnitOfWork.GetRepository<ApplicationUser>().Get().Any(m => m.LoginId == loginId);

        public bool CheckEmail(string email)
            => UnitOfWork.GetRepository<ApplicationUser>().Get().Any(m => m.Email == email && !m.IsDisabled);

        public bool UpdateAvatar(UserModel model)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(ChangePasswordModel model)
        {
            var user =
                UnitOfWork.GetRepository<ApplicationUser>()
                    .Single(m => !m.IsDisabled && m.ApplicationUserId == model.UserId);
            if(user == null) return false;
            user.Password = model.NewPassword.ToMd5();
            UnitOfWork.SaveChanges();
            var emailToUserModel = new EmailModel()
            {
                ToAddress = user.Email,
                Subject = "[MDOC] - Change password",
                Body = $"Hello {user.UserName}, <br/> Your login information is:<br/> LoginId: <strong>{user.LoginId}</strong> <br/> Password: <strong>{model.NewPassword}</strong><br/>Please change your password when you start using MDoc ASAP.<br/> If you have any issue, please contact trandev90@gmail.com for more information. <strong>Do not reply</strong> this email."
            };
            _emailService.SendEmailToUser(emailToUserModel);
            return true;
        }

        public bool Remove(UserModel model)
        {
            var user = UnitOfWork.GetRepository<ApplicationUser>().GetByKeys(model.UserId);
            if(user == null) return false;
            user.IsDisabled = true;
            UnitOfWork.SaveChanges();
            return true;
        }

        public void RenewPassword(ForgotPasswordModel model)
        {
            var currentUser =
                UnitOfWork.GetRepository<ApplicationUser>().Single(m => !m.IsDisabled && m.Email == model.Email);
            if(currentUser == null) return;
            var newPassword = MD5Helper.GetPassword();
            currentUser.Password = newPassword.ToMd5();
            UnitOfWork.SaveChanges();
            var emailToUserModel = new EmailModel()
            {
                ToAddress = currentUser.Email,
                Subject = "[MDOC] - Recover password",
                Body = $"Hello {currentUser.UserName}, <br/> Your login information is:<br/> LoginId: <strong>{currentUser.LoginId}</strong> <br/> Password: <strong>{newPassword}</strong><br/>Please change your password when you start using MDoc ASAP.<br/> If you have any issue, please contact trandev90@gmail.com for more information. <strong>Do not reply</strong> this email."
            };
            _emailService.SendEmailToUser(emailToUserModel);
        }
    }
}
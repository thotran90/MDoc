using System;
using System.Linq;
using MDoc.Entities;
using MDoc.Repositories.Contract;
using MDoc.Services.Contract.DataContracts.User;
using MDoc.Services.Contract.Interfaces;

namespace MDoc.Services.Implements
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public UserModel Create(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserModel> GetUsers(SearchUserModel arg)
        {
            throw new NotImplementedException();
        }

        public UserModel Login(LoginModel model)
        {
            var userEntity =
                UnitOfWork.GetRepository<ApplicationUser>()
                    .Single(m => !m.IsDisabled && m.LoginId == model.LoginId && m.Password == model.SecurePassword);
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
                LoginId = userEntity.LoginId
            };
            return result;
        }

        public UserModel Update(LoginModel model)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Linq;
using MDoc.Services.Contract.DataContracts.User;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IUserService
    {
        UserModel Login(LoginModel model);
        IQueryable<UserModel> GetUsers(string query = "");
        UserModel Create(UserModel model);
        UserModel Update(UserModel model);
        bool CheckLoginId(string loginId);
        bool CheckEmail(string email);
        bool UpdateAvatar(UserModel model);
        bool ChangePassword(ChangePasswordModel model);
        bool Remove(UserModel model);
    }
}
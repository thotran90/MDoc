using System.Linq;
using MDoc.Services.Contract.DataContracts.User;

namespace MDoc.Services.Contract
{
    public interface IUserService
    {
        UserModel Login(LoginModel model);
        IQueryable<UserModel> GetUsers(SearchUserModel arg);
        UserModel Create(LoginModel model);
        UserModel Update(LoginModel model);
    }
}
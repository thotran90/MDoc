using System.Linq;
using MDoc.Services.Contract.DataContracts.User;

namespace MDoc.Services.Contract.Interfaces
{
    public interface IUserService
    {
        UserModel Login(LoginModel model);
        IQueryable<UserModel> GetUsers(string query = "");
        UserModel Create(LoginModel model);
        UserModel Update(LoginModel model);
    }
}
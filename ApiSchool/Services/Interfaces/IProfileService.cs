using ApiSchool.Models;

namespace ApiSchool.Services.Interfaces
{
    public interface IProfileService
    {
        Task CreateUser(UserModel user);

        Task UpdateUser(UserModel user);

        Task DeleteUser(UserModel user);

        Task<List<UserModel>> GetUsers();

        Task<UserModel> GetUserById(int id);

        Task<List<UserModel>> GetUserByName(string name);
    }
}

using ApiSchool.Data;
using ApiSchool.Models;
using ApiSchool.Services.Interfaces;
using ApiSchool.Utils;
using Microsoft.EntityFrameworkCore;

namespace ApiSchool.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ProfileDbContext _profileDbContext;

        public ProfileService(ProfileDbContext profileDbContext)
        {
            _profileDbContext = profileDbContext;
        }

        public async Task CreateUser(UserModel user)
        {
            _profileDbContext.User.Add(user);
            await _profileDbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(UserModel user)
        {
            _profileDbContext.User.Remove(user);
            await _profileDbContext.SaveChangesAsync();
        }

        public async Task UpdateUser(UserModel user)
        {
            _profileDbContext.User.Update(user);
            await _profileDbContext.SaveChangesAsync();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            UserModel? user = new UserModel();
            user = await _profileDbContext.User.FindAsync(id);
            return user;
        }

        public async Task<List<UserModel>> GetUserByName(string name)
        {
            List<UserModel> users = new List<UserModel>();
            string procedure = MapProcedures.MapGetUserByName(name);
            users = await _profileDbContext.User.FromSqlRaw(procedure).ToListAsync();
            return users;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            users = await _profileDbContext.User.ToListAsync();
            return users;
        }
    }
}

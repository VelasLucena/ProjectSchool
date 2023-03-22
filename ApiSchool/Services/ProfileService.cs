using ApiSchool.Data;
using ApiSchool.Mapper;
using ApiSchool.Models;
using ApiSchool.Recourses;
using ApiSchool.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static ApiSchool.Models.Enum.SystemEnum;

namespace ApiSchool.Services
{
    public class ProfileService : IProfileService
    {
        private readonly Data_ProfileDbContext _profileDbContext;

        public ProfileService(Data_ProfileDbContext profileDbContext)
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
            _profileDbContext.Entry(user).State = EntityState.Modified;
            await _profileDbContext.SaveChangesAsync();
        }

        public async Task<UserModel> GetUserById(int? id)
        {
            UserModel? user = new UserModel();
            user = await _profileDbContext.User.FindAsync(id);
            return user;
        }

        public async Task<List<UserModel>> GetUserByName(string? name)
        {
            List<UserModel> users = new List<UserModel>();

            List<object?> parameters = new List<object?>();
            parameters.Add(name);

            string procedure = ProceduresMapper.Map(ProceduresRec.ResourceManager, nameof(ProceduresRec.GetUserByName) ,parameters);
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

using ApiSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSchool.Data
{
    public class ProfileDbContext: DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options) { }

        public DbSet<UserModel> User { get; set; }
    }
}

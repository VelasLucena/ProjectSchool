using ApiSchool.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiSchool.Data
{
    public class Data_ProfileDbContext: IdentityDbContext<UserModel>
    {
        public Data_ProfileDbContext(DbContextOptions<Data_ProfileDbContext> options) : base(options) { }

        public DbSet<UserModel> User { get; set; }
    }
}

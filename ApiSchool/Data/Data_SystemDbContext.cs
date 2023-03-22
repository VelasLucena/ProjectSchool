using ApiSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSchool.Data
{
    public class Data_SystemDbContext : DbContext
    {
        public Data_SystemDbContext(DbContextOptions<Data_SystemDbContext> options): base(options){ }

        public DbSet<LogExceptionModel> LogException { get; set; }
    }
}

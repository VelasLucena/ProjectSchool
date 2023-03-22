using ApiSchool.Data;
using ApiSchool.Models;
using ApiSchool.Services.Interfaces;

namespace ApiSchool.Services
{
    public class SystemService : ISystemService
    {
        private readonly Data_SystemDbContext _systemDbContext;

        public SystemService(Data_SystemDbContext systemDbContext)
        {
            _systemDbContext = systemDbContext;
        }

        public async Task InsertLogException(LogExceptionModel logException)
        {
            _systemDbContext.LogException.Add(logException);
            await _systemDbContext.SaveChangesAsync();
        }
    }
}

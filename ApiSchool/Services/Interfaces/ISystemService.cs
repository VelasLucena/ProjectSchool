using ApiSchool.Models;

namespace ApiSchool.Services.Interfaces
{
    public interface ISystemService
    {
        Task InsertLogException(LogExceptionModel logException);
    }
}

using ApiSchool.Models;
using ApiSchool.Utils;
using Newtonsoft.Json.Linq;
using static ApiSchool.Models.Enum.SystemEnum;

namespace ApiSchool.Mapper
{
    public class ProfileMappers
    {
        public static UserModel CreateUserMapper(UserModel user)
        {
            user.UpdateUserId = Convert.ToInt32(AppStartUp.GetSettingsApp(AppSettingsKeys.DefaultUserId));
            user.UpdateTime = DateTime.Now;
            return user;
        }
    }
}

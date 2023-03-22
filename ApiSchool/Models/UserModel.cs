using Microsoft.AspNetCore.Identity;
using static ApiSchool.Models.Enum.ProfileEnum;

namespace ApiSchool.Models
{
    public class UserModel : IdentityUser
    {
        public Office office { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int? CreateUserId { get; set; }
        public int? UpdateUserId { get; set; }
    }
}

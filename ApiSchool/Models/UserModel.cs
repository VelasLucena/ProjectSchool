using ApiSchool.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiSchool.Models
{
    public class UserModel
    {
        [Key]
        public int? UserId { get; set; }
        [RegularExpression("([0-9])\\w+")]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string? RegisterNumber { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50)]
        public string? Password { get; set; }

        [DataType(DataType.Text)]
        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string? EmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(50)]
        [RegularExpression("([0-9])\\w+")]
        public string? Phone { get; set; }

        [DataType(DataType.PostalCode)]
        [StringLength(50)]
        [RegularExpression("([0-9])\\w+")]
        public string? CEP { get; set; }

        public ProfileEnum.Office? Office { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public int? CreateUserId { get; set;}
        public int? UpdateUserId { get; set; }
    }
}

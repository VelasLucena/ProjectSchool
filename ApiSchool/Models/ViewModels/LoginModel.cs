using ApiSchool.Recourses;
using System.ComponentModel.DataAnnotations;

namespace ApiSchool.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(ApiMsgRec), ErrorMessageResourceName = nameof(ApiMsgRec.API0001))]
        [EmailAddress(ErrorMessageResourceType = typeof(ApiMsgRec), ErrorMessageResourceName = nameof(ApiMsgRec.API0002))]
        public string? Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ApiMsgRec), ErrorMessageResourceName = nameof(ApiMsgRec.API0003))]
        [StringLength(20, ErrorMessageResourceType = typeof(ApiMsgRec), ErrorMessageResourceName = nameof(ApiMsgRec.API0004), MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}

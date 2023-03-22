using System.ComponentModel.DataAnnotations;

namespace ApiSchool.Models.ViewModels
{
    public class RegisterUserModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma senha")]
        [Compare("Password", ErrorMessage = "Senhas não conferem")]
        public string? ConfirmPassword { get; set; }
    }
}

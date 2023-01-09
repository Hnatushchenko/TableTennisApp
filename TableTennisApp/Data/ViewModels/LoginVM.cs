using System.ComponentModel.DataAnnotations;

namespace TableTennisApp.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Ел. пошта")]
        [Required(ErrorMessage = "Введіть електронну адресу")]
        [EmailAddress(ErrorMessage = "Введена електронна адреса некоректна")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}

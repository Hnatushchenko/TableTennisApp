using System.ComponentModel.DataAnnotations;

namespace TableTennisApp.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "FullName")]
        [Required(ErrorMessage = "Введіть ім’я та прізвище")]
        public string? UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Введіть електронну адресу")]
        [EmailAddress(ErrorMessage = "Введена електронна адреса некоректна")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Підтвердіть пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються. Повторіть спробу.")]
        public string? ConfirmPassword { get; set; }
    }
}

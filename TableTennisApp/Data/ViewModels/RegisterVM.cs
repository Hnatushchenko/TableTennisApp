using System.ComponentModel.DataAnnotations;

namespace TableTennisApp.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Ім'я та прізвище")]
        [Required(ErrorMessage = "Введіть ім’я та прізвище")]
        public string? UserName { get; set; }

        [Display(Name = "Ел. пошта")]
        [Required(ErrorMessage = "Введіть електронну адресу")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Підтвердіть пароль")]
        [Required(ErrorMessage = "Підтвердіть пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються. Повторіть спробу.")]
        public string? ConfirmPassword { get; set; }
    }
}

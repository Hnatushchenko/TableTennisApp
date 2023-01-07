using System.ComponentModel.DataAnnotations;

namespace TableTennisApp.Data.ViewModels
{
    public class UserDetailsVM
    {
        public Guid Id { get; set; }
        [Display(Name = "Ім'я та прізвище")]
        [Required(ErrorMessage = "Введіть ім’я та прізвище")]
        public string? UserName { get; set; }

        [Display(Name = "Ел. пошта")]
        [Required(ErrorMessage = "Введіть електронну адресу")]
        public string? Email { get; set; }

        [Display(Name = "Ролі")]
        [Required(ErrorMessage = "Введіть ролі")]
        public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();

        [Display(Name = "Рейтинг")]
        [Required(ErrorMessage = "Вкажіть рейтинг")]
        [Range(0, 5000, ErrorMessage = "Рейтинг повинен бути в межах від 0 до 5000")]
        public int Rating { get; set; }

        [Display(Name = "Кількість ігор")]
        [Required(ErrorMessage = "Вкажіть кількість ігор")]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість ігор повинна бути більше 0")]
        public int TotalNumberOfGames { get; set; }
    }
}

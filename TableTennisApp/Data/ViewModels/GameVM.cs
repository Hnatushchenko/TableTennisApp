using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TableTennisApp.Data.ValidationAttributes;
using TableTennisApp.Models;

namespace TableTennisApp.Data.ViewModels
{
    public class GameVM
    {
        public IEnumerable<ApplicationUser> Players { get; set; } = Array.Empty<ApplicationUser>();

        [PropertiesAreNotEqual(nameof(LoserId))]
        [GuidNotEmpty(ErrorMessage = "Виберіть переможця")]
        public Guid WinnerId { get; set; }

        [Range(11, int.MaxValue, ErrorMessage = "Значення повинно бути більше 10")]
        [Required(ErrorMessage = "Вкажіть рахунок")]
        public int WinnerScore { get; set; }

        [PropertiesAreNotEqual(nameof(WinnerId))]
        [GuidNotEmpty(ErrorMessage = "Виберіть того, хто програв")]
        public Guid LoserId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Значення повинно бути більше або дорівнювати 0")]
        [Required(ErrorMessage = "Вкажіть рахунок")]
        public int LoserScore { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace TableTennisApp.Models
{
    public class Player : IdentityUser<Guid>
    {
        public int Rating { get; set; }
        public int TotalNumberOfGames { get; set; }
        public List<Game>? Games { get; set; }
    }
}

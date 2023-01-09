namespace TableTennisApp.Data.ViewModels
{
    public class UsersVM
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
        public int Rating { get; set; }
    }
}

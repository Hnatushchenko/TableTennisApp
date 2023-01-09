namespace TableTennisApp.Data.Constants
{
    public class UserRoles
    {
        public const string User = "User";
        public const string Referee = "Referee";
        public const string Admin = "Admin";
        public static readonly IReadOnlyCollection<string> AllRoles = new List<string>() { User, Referee, Admin }.AsReadOnly();
    }
}

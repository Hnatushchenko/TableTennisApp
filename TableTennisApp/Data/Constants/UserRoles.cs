namespace TableTennisApp.Data.Constants
{
    public class UserRoles
    {
        public const string User = "User";
        public const string Admin = "Admin";
        public static readonly List<string> AllRoles = new List<string>() { User, Admin };
    }
}

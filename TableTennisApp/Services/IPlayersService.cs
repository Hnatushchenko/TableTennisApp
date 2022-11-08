namespace TableTennisApp.Services
{
    public interface IPlayersService
    {
        Task AddAsync(string name, string login, string password);
    }
}
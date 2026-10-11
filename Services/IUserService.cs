using CSE_325_Team_16_Eunomia.Models;

namespace CSE_325_Team_16_Eunomia.Services;

public interface IUserService
{
    Task<(bool Success, string? ErrorMessage, UserAccount? User)> RegisterAsync(RegisterModel model);
    Task<(bool Success, string? ErrorMessage, UserAccount? User)> LoginAsync(string username, string password);
    Task<List<UserAccount>> GetAllUsersAsync();
    Task<UserAccount?> GetUserByIdAsync(int id);
    Task<UserAccount?> GetUserByUsernameAsync(string username);
}


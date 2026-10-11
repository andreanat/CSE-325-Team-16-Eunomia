using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using CSE_325_Team_16_Eunomia.Data;
using CSE_325_Team_16_Eunomia.Models;

namespace CSE_325_Team_16_Eunomia.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(AppDbContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<(bool Success, string? ErrorMessage, UserAccount? User)> RegisterAsync(RegisterModel model)
    {
        try
        {
            var trimmedUsername = model.Username.Trim();
            var trimmedEmail = model.Email.Trim().ToLowerInvariant();

            // Check if username already exists (case-insensitive)
            var usernameExists = await _context.Users
                .AnyAsync(u => u.Username.ToLower() == trimmedUsername.ToLower());

            if (usernameExists)
            {
                return (false, $"The username '{trimmedUsername}' is already taken. Please choose another one.", null);
            }

            // Check if email already exists (case-insensitive)
            var emailExists = await _context.Users
                .AnyAsync(u => u.Email.ToLower() == trimmedEmail);

            if (emailExists)
            {
                return (false, $"An account with the email '{model.Email.Trim()}' is already registered.", null);
            }

            // Secure password hashing with BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, workFactor: 11);

            var newUser = new UserAccount
            {
                Username = trimmedUsername,
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                Email = trimmedEmail,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully registered new user: {Username} ({Email})", newUser.Username, newUser.Email);

            return (true, null, newUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering user {Username}", model.Username);
            return (false, "An error occurred during registration. Please try again later.", null);
        }
    }

    public async Task<(bool Success, string? ErrorMessage, UserAccount? User)> LoginAsync(string username, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Please provide both username and password.", null);
            }

            var trimmedUsername = username.Trim();

            // Find user by username (case-insensitive)
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == trimmedUsername.ToLower());

            if (user == null)
            {
                return (false, "Invalid username or password.", null);
            }

            // Verify password hash with BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid)
            {
                _logger.LogWarning("Failed login attempt for user: {Username}", trimmedUsername);
                return (false, "Invalid username or password.", null);
            }

            // Update last login timestamp
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successful login for user: {Username}", user.Username);
            return (true, null, user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error authenticating user {Username}", username);
            return (false, "An error occurred while logging in. Please try again later.", null);
        }
    }

    public async Task<List<UserAccount>> GetAllUsersAsync()
    {
        return await _context.Users
            .OrderBy(u => u.Id)
            .ToListAsync();
    }

    public async Task<UserAccount?> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserAccount?> GetUserByUsernameAsync(string username)
    {
        var trimmed = username.Trim().ToLowerInvariant();
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == trimmed);
    }
}


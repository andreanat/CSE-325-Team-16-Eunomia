using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using CSE_325_Team_16_Eunomia.Models;

namespace CSE_325_Team_16_Eunomia.Data;

public static class DbInitializer
{
    public const string DefaultDemoPassword = "Password123!";

    public static async Task InitializeAsync(AppDbContext context, ILogger logger)
    {
        try
        {
            logger.LogInformation("Ensuring PostgreSQL database schema exists...");
            await context.Database.EnsureCreatedAsync();
            logger.LogInformation("Database schema ensured successfully.");

            if (!await context.Users.AnyAsync())
            {
                logger.LogInformation("No users found in database. Seeding 10 fake accounts...");
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(DefaultDemoPassword, workFactor: 11);

                var fakeAccounts = new List<UserAccount>
                {
                    new()
                    {
                        Username = "alex.morgan",
                        FirstName = "Alex",
                        LastName = "Morgan",
                        Email = "alex.morgan@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-30)
                    },
                    new()
                    {
                        Username = "sarah.chen",
                        FirstName = "Sarah",
                        LastName = "Chen",
                        Email = "sarah.chen@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-28)
                    },
                    new()
                    {
                        Username = "marcus.vance",
                        FirstName = "Marcus",
                        LastName = "Vance",
                        Email = "marcus.vance@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-25)
                    },
                    new()
                    {
                        Username = "elena.rostova",
                        FirstName = "Elena",
                        LastName = "Rostova",
                        Email = "elena.rostova@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-20)
                    },
                    new()
                    {
                        Username = "jordan.lee",
                        FirstName = "Jordan",
                        LastName = "Lee",
                        Email = "jordan.lee@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-18)
                    },
                    new()
                    {
                        Username = "maya.patel",
                        FirstName = "Maya",
                        LastName = "Patel",
                        Email = "maya.patel@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-15)
                    },
                    new()
                    {
                        Username = "liam.connor",
                        FirstName = "Liam",
                        LastName = "Connor",
                        Email = "liam.connor@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-12)
                    },
                    new()
                    {
                        Username = "chloe.bennett",
                        FirstName = "Chloe",
                        LastName = "Bennett",
                        Email = "chloe.bennett@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-10)
                    },
                    new()
                    {
                        Username = "diego.ramirez",
                        FirstName = "Diego",
                        LastName = "Ramirez",
                        Email = "diego.ramirez@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-5)
                    },
                    new()
                    {
                        Username = "zara.ahmed",
                        FirstName = "Zara",
                        LastName = "Ahmed",
                        Email = "zara.ahmed@eunomia.app",
                        PasswordHash = hashedPassword,
                        CreatedAt = DateTime.UtcNow.AddDays(-2)
                    }
                };

                await context.Users.AddRangeAsync(fakeAccounts);
                await context.SaveChangesAsync();
                logger.LogInformation("Successfully seeded 10 fake accounts into the database.");
            }
            else
            {
                var count = await context.Users.CountAsync();
                logger.LogInformation("Database already contains {Count} user accounts.", count);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing or seeding the database.");
            throw;
        }
    }
}


using System.ComponentModel.DataAnnotations;

namespace CSE_325_Team_16_Eunomia.Models;

public class UserAccount
{
    private string _name = string.Empty;

    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Full name property supporting both get and set for compatibility
    /// </summary>
    public string Name
    {
        get
        {
            var combined = $"{FirstName} {LastName}".Trim();
            return !string.IsNullOrEmpty(combined) ? combined : _name;
        }
        set
        {
            _name = value ?? string.Empty;
            var parts = _name.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0 && string.IsNullOrEmpty(FirstName))
            {
                FirstName = parts[0];
            }
            if (parts.Length > 1 && string.IsNullOrEmpty(LastName))
            {
                LastName = parts[1];
            }
        }
    }
}
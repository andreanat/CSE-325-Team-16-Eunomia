using System.ComponentModel.DataAnnotations;

namespace CSE_325_Team_16_Eunomia.Models;

public class MediaItem
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = "Movies";

    [Required]
    public string Status { get; set; } = "Want to Try";

    [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
    public int? Rating { get; set; }

    [StringLength(500, ErrorMessage = "Notes cannot be longer than 500 characters.")]
    public string Notes { get; set; } = string.Empty;

    public DateTime? StartedOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.Now;
}
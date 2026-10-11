using CSE_325_Team_16_Eunomia.Models;

namespace CSE_325_Team_16_Eunomia.Services;

public class DashboardStats
{
    public int CompletedCount { get; set; }
    public int InProgressCount { get; set; }
    public int WantToTryCount { get; set; }
    public int TotalCount { get; set; }
}

public interface IMediaService
{
    Task<List<MediaItem>> GetMediaItemsByUserAsync(int userId);
    Task<MediaItem?> GetMediaItemByIdAsync(int id, int userId);
    Task<MediaItem> SaveMediaItemAsync(MediaItem item, int userId);
    Task<bool> DeleteMediaItemAsync(int id, int userId);
    Task<bool> UpdateStatusAsync(int id, int userId, string newStatus);
    Task<DashboardStats> GetDashboardStatsAsync(int userId);
}


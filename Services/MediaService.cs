using Microsoft.EntityFrameworkCore;
using CSE_325_Team_16_Eunomia.Data;
using CSE_325_Team_16_Eunomia.Models;

namespace CSE_325_Team_16_Eunomia.Services;

public class MediaService : IMediaService
{
    private readonly AppDbContext _context;
    private readonly ILogger<MediaService> _logger;

    public MediaService(AppDbContext context, ILogger<MediaService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<MediaItem>> GetMediaItemsByUserAsync(int userId)
    {
        return await _context.MediaItems
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.DateAdded)
            .ToListAsync();
    }

    public async Task<MediaItem?> GetMediaItemByIdAsync(int id, int userId)
    {
        return await _context.MediaItems
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
    }

    public async Task<MediaItem> SaveMediaItemAsync(MediaItem item, int userId)
    {
        item.UserId = userId;

        if (item.Id == 0)
        {
            item.DateAdded = DateTime.Now;
            await _context.MediaItems.AddAsync(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created new media item {Title} for user {UserId}", item.Title, userId);
            return item;
        }
        else
        {
            var existing = await _context.MediaItems
                .FirstOrDefaultAsync(m => m.Id == item.Id && m.UserId == userId);

            if (existing != null)
            {
                existing.Title = item.Title;
                existing.Category = item.Category;
                existing.Status = item.Status;
                existing.Rating = item.Rating;
                existing.Notes = item.Notes;
                existing.StartedOn = item.StartedOn;
                existing.CompletedOn = item.CompletedOn;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated media item {Id} ({Title}) for user {UserId}", item.Id, item.Title, userId);
                return existing;
            }

            return item;
        }
    }

    public async Task<bool> DeleteMediaItemAsync(int id, int userId)
    {
        var item = await _context.MediaItems
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (item != null)
        {
            _context.MediaItems.Remove(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted media item {Id} for user {UserId}", id, userId);
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateStatusAsync(int id, int userId, string newStatus)
    {
        var item = await _context.MediaItems
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (item == null)
            return false;

        item.Status = newStatus;

        if (newStatus == "Currently Enjoying")
        {
            item.StartedOn ??= DateTime.Today;
            item.CompletedOn = null;
        }
        else if (newStatus == "Completed")
        {
            item.StartedOn ??= DateTime.Today;
            item.CompletedOn ??= DateTime.Today;
        }
        else
        {
            item.CompletedOn = null;
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated status for media item {Id} to {Status}", id, newStatus);
        return true;
    }

    public async Task<DashboardStats> GetDashboardStatsAsync(int userId)
    {
        var items = await _context.MediaItems
            .Where(m => m.UserId == userId)
            .ToListAsync();

        return new DashboardStats
        {
            CompletedCount = items.Count(m => m.Status == "Completed"),
            InProgressCount = items.Count(m => m.Status == "Currently Enjoying"),
            WantToTryCount = items.Count(m => m.Status == "Want to Try"),
            TotalCount = items.Count
        };
    }
}


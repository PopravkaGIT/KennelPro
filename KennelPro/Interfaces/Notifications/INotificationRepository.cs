using KennelPro.Models.Notifications;

namespace KennelPro.Interfaces.Notifications;

public interface INotificationRepository
{
    Task<List<Notification>> GetAllAsync();

    Task<Notification?> GetByIdAsync(Guid id);

    Task AddAsync(Notification notification);

    Task UpdateAsync(Notification notification);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}
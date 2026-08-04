using KennelPro.Interfaces.Notifications;
using KennelPro.Models.Notifications;

namespace KennelPro.Services.Notifications;

public class NotificationService
{
    private readonly INotificationRepository _repository;

    public NotificationService(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Notification>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Notification?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Notification notification)
    {
        await _repository.AddAsync(notification);
    }

    public async Task UpdateAsync(Notification notification)
    {
        await _repository.UpdateAsync(notification);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
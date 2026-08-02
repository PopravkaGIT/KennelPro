using KennelPro.Models.Authentication;

namespace KennelPro.Interfaces.Authentication;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);

    Task AddAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}
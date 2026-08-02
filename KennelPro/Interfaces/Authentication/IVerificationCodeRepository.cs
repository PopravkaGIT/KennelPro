using KennelPro.Models.Authentication;

namespace KennelPro.Interfaces.Authentication;

public interface IVerificationCodeRepository
{
    Task<List<VerificationCode>> GetAllAsync();

    Task<VerificationCode?> GetByIdAsync(Guid id);

    Task<VerificationCode?> GetByCodeAsync(string code);

    Task AddAsync(VerificationCode code);

    Task UpdateAsync(VerificationCode code);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}
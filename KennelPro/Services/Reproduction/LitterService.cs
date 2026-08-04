using KennelPro.Interfaces.Litters;
using KennelPro.Models.Litters;

namespace KennelPro.Services.Reproduction;

public class LitterService
{
    private readonly ILitterRepository _repository;

    public LitterService(ILitterRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Litter>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Litter?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Litter litter)
    {
        await _repository.AddAsync(litter);
    }

    public async Task UpdateAsync(Litter litter)
    {
        await _repository.UpdateAsync(litter);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
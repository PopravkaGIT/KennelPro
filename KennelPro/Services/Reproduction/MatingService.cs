using KennelPro.Interfaces.Reproduction;
using KennelPro.Models.Reproduction;

namespace KennelPro.Services.Reproduction;

public class MatingService
{
    private readonly IMatingRepository _repository;

    public MatingService(IMatingRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Mating>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Mating?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Mating mating)
    {
        await _repository.AddAsync(mating);
    }

    public async Task UpdateAsync(Mating mating)
    {
        await _repository.UpdateAsync(mating);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
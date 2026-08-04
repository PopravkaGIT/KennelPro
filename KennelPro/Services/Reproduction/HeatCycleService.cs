using KennelPro.Interfaces.Reproduction;
using KennelPro.Models.Reproduction;

namespace KennelPro.Services.Reproduction;

public class HeatCycleService
{
    private readonly IHeatCycleRepository _repository;

    public HeatCycleService(IHeatCycleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<HeatCycle>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<HeatCycle?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(HeatCycle cycle)
    {
        await _repository.AddAsync(cycle);
    }

    public async Task UpdateAsync(HeatCycle cycle)
    {
        await _repository.UpdateAsync(cycle);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
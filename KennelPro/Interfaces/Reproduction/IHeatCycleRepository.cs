using KennelPro.Models.Reproduction;

namespace KennelPro.Interfaces.Reproduction;

public interface IHeatCycleRepository
{
    Task<List<HeatCycle>> GetAllAsync();

    Task<HeatCycle?> GetByIdAsync(Guid id);

    Task AddAsync(HeatCycle heatCycle);

    Task UpdateAsync(HeatCycle heatCycle);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}
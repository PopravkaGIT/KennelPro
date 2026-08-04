using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;

namespace KennelPro.Services.Medical;

public class ParasiteService
{
    private readonly IParasiteTreatmentRepository _parasiteRepository;

    public ParasiteService(IParasiteTreatmentRepository parasiteRepository)
    {
        _parasiteRepository = parasiteRepository;
    }

    public async Task<List<ParasiteTreatment>> GetAllAsync()
    {
        return await _parasiteRepository.GetAllAsync();
    }

    public async Task<ParasiteTreatment?> GetByIdAsync(Guid id)
    {
        return await _parasiteRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(ParasiteTreatment treatment)
    {
        await _parasiteRepository.AddAsync(treatment);
    }

    public async Task UpdateAsync(ParasiteTreatment treatment)
    {
        await _parasiteRepository.UpdateAsync(treatment);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _parasiteRepository.DeleteAsync(id);
    }
}
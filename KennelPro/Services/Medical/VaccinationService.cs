using KennelPro.Interfaces.Medical;
using KennelPro.Models.Medical;

namespace KennelPro.Services.Medical;

public class VaccinationService
{
    private readonly IVaccinationRepository _vaccinationRepository;

    public VaccinationService(IVaccinationRepository vaccinationRepository)
    {
        _vaccinationRepository = vaccinationRepository;
    }

    public async Task<List<Vaccination>> GetAllAsync()
    {
        return await _vaccinationRepository.GetAllAsync();
    }

    public async Task<Vaccination?> GetByIdAsync(Guid id)
    {
        return await _vaccinationRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Vaccination vaccination)
    {
        await _vaccinationRepository.AddAsync(vaccination);
    }

    public async Task UpdateAsync(Vaccination vaccination)
    {
        await _vaccinationRepository.UpdateAsync(vaccination);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _vaccinationRepository.DeleteAsync(id);
    }
}
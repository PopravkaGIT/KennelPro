using KennelPro.Interfaces.Documents;
using KennelPro.Models.Documents;

namespace KennelPro.Services.Documents;

public class TitleService
{
    private readonly ITitleRepository _titleRepository;

    public TitleService(ITitleRepository titleRepository)
    {
        _titleRepository = titleRepository;
    }

    public async Task<List<Title>> GetTitlesAsync()
    {
        return await _titleRepository.GetAllAsync();
    }

    public async Task<Title?> GetTitleAsync(Guid id)
    {
        return await _titleRepository.GetByIdAsync(id);
    }

    public async Task AddTitleAsync(Title title)
    {
        await _titleRepository.AddAsync(title);
    }

    public async Task UpdateTitleAsync(Title title)
    {
        await _titleRepository.UpdateAsync(title);
    }

    public async Task DeleteTitleAsync(Guid id)
    {
        await _titleRepository.DeleteAsync(id);
    }
}
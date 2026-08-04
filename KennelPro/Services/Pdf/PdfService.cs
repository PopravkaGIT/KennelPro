namespace KennelPro.Services.Pdf;

public class PdfService
{
    public async Task<byte[]> CreatePdfAsync()
    {
        return await Task.FromResult(Array.Empty<byte>());
    }
}
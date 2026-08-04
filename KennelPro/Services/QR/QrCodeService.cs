namespace KennelPro.Services.QR;

public class QrCodeService
{
    public async Task<byte[]> GenerateAsync(string text)
    {
        // Потом подключим библиотеку QRCoder

        return await Task.FromResult(Array.Empty<byte>());
    }
}
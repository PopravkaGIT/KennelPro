namespace KennelPro.Services.Email;

public class EmailService
{
    public async Task SendEmailAsync(
        string email,
        string subject,
        string message)
    {
        // Реализуем позже через SMTP или SendGrid

        await Task.CompletedTask;
    }
}
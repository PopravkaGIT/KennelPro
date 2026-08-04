using KennelPro.Data.Database;
using KennelPro.Data.Repositories.Authentication;
using KennelPro.Data.Repositories.Dogs;
using KennelPro.Data.Repositories.Documents;
using KennelPro.Data.Repositories.Kennels;
using KennelPro.Data.Repositories.Litters;
using KennelPro.Data.Repositories.Medical;
using KennelPro.Data.Repositories.Notifications;
using KennelPro.Data.Repositories.Reproduction;
using KennelPro.Interfaces.Authentication;
using KennelPro.Interfaces.Dogs;
using KennelPro.Interfaces.Documents;
using KennelPro.Interfaces.Kennels;
using KennelPro.Interfaces.Litters;
using KennelPro.Interfaces.Medical;
using KennelPro.Interfaces.Notifications;
using KennelPro.Interfaces.Reproduction;
using KennelPro.Services.Api;
using KennelPro.Services.Authentication;
using KennelPro.Services.Backup;
using KennelPro.Services.Cloud;
using KennelPro.Services.Documents;
using KennelPro.Services.Dogs;
using KennelPro.Services.Email;
using KennelPro.Services.Export;
using KennelPro.Services.Logging;
using KennelPro.Services.Medical;
using KennelPro.Services.Notifications;
using KennelPro.Services.Pdf;
using KennelPro.Services.QR;
using KennelPro.Services.Reproduction;
using KennelPro.Services.Settings;
using KennelPro.Services.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace KennelPro;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Подключение базы KennelPro.db
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            string dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "KennelPro.db");

            options.UseSqlite(
                $"Data Source={dbPath}");
        });

        // Authentication
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();

        // Kennels
        builder.Services.AddScoped<IKennelRepository, KennelRepository>();

        // Dogs
        builder.Services.AddScoped<IDogRepository, DogRepository>();
        builder.Services.AddScoped<IBreedRepository, BreedRepository>();

        // Medical
        builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
        builder.Services.AddScoped<IVaccinationRepository, VaccinationRepository>();
        builder.Services.AddScoped<IParasiteTreatmentRepository, ParasiteTreatmentRepository>();
        builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
        builder.Services.AddScoped<IDiseaseRepository, DiseaseRepository>();

        // Reproduction
        builder.Services.AddScoped<IHeatCycleRepository, HeatCycleRepository>();
        builder.Services.AddScoped<IMatingRepository, MatingRepository>();

        // Litters
        builder.Services.AddScoped<ILitterRepository, LitterRepository>();
        builder.Services.AddScoped<IPuppyRepository, PuppyRepository>();

        // Documents
        builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
        builder.Services.AddScoped<ITitleRepository, TitleRepository>();

        // Notifications
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        
        // Http
        builder.Services.AddHttpClient();

        // Api
        builder.Services.AddScoped<HttpService>();
        builder.Services.AddScoped<DogApiService>();

        // Authentication
        builder.Services.AddScoped<AuthenticationService>();
        builder.Services.AddScoped<PasswordService>();
        builder.Services.AddScoped<VerificationService>();

        // Dogs
        builder.Services.AddScoped<DogService>();
        builder.Services.AddScoped<BreedService>();

        // Documents
        builder.Services.AddScoped<DocumentService>();
        builder.Services.AddScoped<TitleService>();

        // Medical
        builder.Services.AddScoped<MedicalService>();
        builder.Services.AddScoped<VaccinationService>();
        builder.Services.AddScoped<ParasiteService>();

        // Reproduction
        builder.Services.AddScoped<HeatCycleService>();
        builder.Services.AddScoped<MatingService>();
        builder.Services.AddScoped<LitterService>();

        // Notifications
        builder.Services.AddScoped<NotificationService>();

        // Storage
        builder.Services.AddScoped<StorageService>();

        // Backup
        builder.Services.AddScoped<BackupService>();

        // Cloud
        builder.Services.AddScoped<CloudStorageService>();
        builder.Services.AddScoped<CloudSyncService>();

        // Export
        builder.Services.AddScoped<ExportService>();

        // Email
        builder.Services.AddScoped<EmailService>();

        // PDF
        builder.Services.AddScoped<PdfService>();

        // QR
        builder.Services.AddScoped<QrCodeService>();

        // Logging
        builder.Services.AddScoped<LogService>();

        // Settings
        builder.Services.AddScoped<SettingsService>();    

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
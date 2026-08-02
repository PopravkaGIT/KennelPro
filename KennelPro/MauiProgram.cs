using KennelPro.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


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



#if DEBUG
        builder.Logging.AddDebug();
#endif


        return builder.Build();
    }
}
using KennelPro.Data.Database;
using KennelPro.Models.Dogs;
using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Seed;

public static class BreedSeed
{
    private static readonly string[] DefaultBreeds =
    {
        "Labrador Retriever",
        "German Shepherd",
        "Golden Retriever",
        "French Bulldog",
        "Poodle",
        "Mixed Breed",
        "Other"
    };

    public static async Task EnsureDefaultBreedsAsync(AppDbContext context)
    {
        if (await context.Breeds.AnyAsync())
            return;

        foreach (var name in DefaultBreeds)
        {
            await context.Breeds.AddAsync(new Breed
            {
                Id = Guid.NewGuid(),
                Name = name
            });
        }

        await context.SaveChangesAsync();
    }
}

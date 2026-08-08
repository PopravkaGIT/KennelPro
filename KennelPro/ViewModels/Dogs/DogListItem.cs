using KennelPro.Helpers;
using KennelPro.Models.Dogs;
using KennelPro.Models.Enums;

namespace KennelPro.ViewModels.Dogs;

public class DogListItem
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string BreedName { get; init; } = string.Empty;

    public string GenderText { get; init; } = string.Empty;

    public string BirthDateText { get; init; } = string.Empty;

    public static DogListItem FromDog(Dog dog)
    {
        return new DogListItem
        {
            Id = dog.Id,
            Name = dog.Name,
            BreedName = dog.Breed?.Name ?? "-",
            GenderText = dog.Gender == Gender.Male ? "Male" : "Female",
            BirthDateText = DateHelper.FormatDate(dog.BirthDate)
        };
    }
}

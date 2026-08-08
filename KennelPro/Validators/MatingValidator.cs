using KennelPro.Models.Reproduction;
namespace KennelPro.Validators;
public static class MatingValidator { public static bool Validate(Mating item, out string error) { if (item.FemaleDogId == Guid.Empty || item.MaleDogId == Guid.Empty) { error="Both parents are required."; return false; } if (item.FemaleDogId == item.MaleDogId) { error="Parents must be different dogs."; return false; } if (item.Date > DateTime.Today) { error="Mating date cannot be in the future."; return false; } error=string.Empty; return true; } }

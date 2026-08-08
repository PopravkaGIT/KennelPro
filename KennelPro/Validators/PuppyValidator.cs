using KennelPro.Helpers;
using KennelPro.Models.Litters;
namespace KennelPro.Validators;
public static class PuppyValidator { public static bool Validate(Puppy item, out string error) { if (item.LitterId == Guid.Empty) { error="Litter is required."; return false; } if (!ValidationHelper.HasText(item.Name)) { error="Puppy name is required."; return false; } if (item.Number < 1) { error="Puppy number must be positive."; return false; } if (item.BirthDate > DateTime.Today) { error="Birth date cannot be in the future."; return false; } error=string.Empty; return true; } }

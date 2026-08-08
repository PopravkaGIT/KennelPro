using KennelPro.Models.Reproduction;
namespace KennelPro.Validators;
public static class HeatCycleValidator { public static bool Validate(HeatCycle item, out string error) { if (item.DogId == Guid.Empty) { error="Dog is required."; return false; } if (item.StartDate > DateTime.Today) { error="Start date cannot be in the future."; return false; } if (item.EndDate.HasValue && item.EndDate < item.StartDate) { error="End date cannot be before start date."; return false; } error=string.Empty; return true; } }

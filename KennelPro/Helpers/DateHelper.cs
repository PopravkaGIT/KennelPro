namespace KennelPro.Helpers;

public static class DateHelper
{
    public static int GetAge(DateTime birthDate)
    {
        var today = DateTime.Today;

        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    public static bool IsFuture(DateTime date)
    {
        return date > DateTime.Now;
    }

    public static string FormatDate(DateTime? date)
    {
        return date?.ToString("dd.MM.yyyy") ?? "-";
    }

    public static int DaysUntil(DateTime date)
    {
        return (date.Date - DateTime.Today).Days;
    }
}
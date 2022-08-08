namespace Firestone.Domain.Utils;

using Constants;

public class DateUtils
{
    public static int GetTotalMonths(DateOnly first, DateOnly last)
    {
        int yearsApart = last.Year - first.Year;
        int monthsApart = last.Month - first.Month;

        return yearsApart * FirestoneValues.MonthsPerYear + monthsApart;
    }

    public static int GetTotalMonths(DateTime first, DateTime second)
    {
        return GetTotalMonths(DateOnly.FromDateTime(first), DateOnly.FromDateTime(second));
    }
}

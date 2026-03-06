
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Extensions;
public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
        int age = today.Year - dob.Year;
        today = today.AddYears(-age);
        if (dob > today)
        {
            age--;
        }
        return age;
    }
}
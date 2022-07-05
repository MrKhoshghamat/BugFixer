using System.Globalization;

namespace BugFixer.Application.Extensions
{
    public static class DateExtensions
    {
        public static string ToShamsi(this DateTime date)
        {
            var persianCalendar = new PersianCalendar();
            return
                $"{persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date).ToString("00")}/{persianCalendar.GetDayOfMonth(date).ToString("00")}";
        }
    }
}

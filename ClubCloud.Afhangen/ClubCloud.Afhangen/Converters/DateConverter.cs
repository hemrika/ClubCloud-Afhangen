using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml.Data;

namespace ClubCloud.Afhangen.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            if (!(value is DateTime))
                return null;

            var dateTime = (DateTime)value;

            var dateTimeFormatter = new DateTimeFormatter(YearFormat.None,
                MonthFormat.None,
                DayFormat.None,
                DayOfWeekFormat.None,
                HourFormat.Default,
                MinuteFormat.Default,
                SecondFormat.None,
                new[] { "nl-NL" },
                "NL",
                CalendarIdentifiers.Gregorian,
                ClockIdentifiers.TwentyFourHour);

            try { return "Bijgewerkt om "+dateTimeFormatter.Format(dateTime); }
            catch { return null; }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            throw new NotImplementedException();
        }
    }
}

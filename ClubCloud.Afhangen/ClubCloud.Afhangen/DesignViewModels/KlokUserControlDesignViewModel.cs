namespace ClubCloud.Afhangen.DesignViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Core.Prism;
    using ClubCloud.Core.Prism.Interfaces;
    
    
    using System;
    using System.Threading.Tasks;
    using Windows.Globalization;
    using Windows.Globalization.DateTimeFormatting;

    public class KlokUserControlDesignViewModel : IView
    {
        private DateTimeFormatter dateFormatter = new DateTimeFormatter(YearFormat.Full, MonthFormat.Full, DayFormat.Default, DayOfWeekFormat.None, HourFormat.None, MinuteFormat.None, SecondFormat.None, new[] { "nl-NL" }, "NL", CalendarIdentifiers.Gregorian, ClockIdentifiers.TwentyFourHour);
        private DateTimeFormatter timeFormatter = new DateTimeFormatter(YearFormat.None, MonthFormat.None, DayFormat.None, DayOfWeekFormat.None, HourFormat.Default, MinuteFormat.Default, SecondFormat.None, new[] { "nl-NL" }, "NL", CalendarIdentifiers.Gregorian, ClockIdentifiers.TwentyFourHour);

        public KlokUserControlDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Date = dateFormatter.Format(DateTime.Now);
            Time = timeFormatter.Format(DateTime.Now);

            //Date = DateTime.Now.ToString("dd MMMM, yyyy");
            //Time = DateTime.Now.ToString("HH:mm");
        }

        public string Date { get; private set; }

        public string Time { get; private set; }

        object IView.DataContext
        {
            get
            {
                return null;
            }
            set
            {
                object dc = value;
            }
        }
    }
}

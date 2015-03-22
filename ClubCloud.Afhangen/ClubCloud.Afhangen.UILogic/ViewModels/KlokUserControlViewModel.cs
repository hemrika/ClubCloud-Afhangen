using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class KlokUserControlViewModel : ViewModel, INotifyPropertyChanged, IKlokUserControlViewModel, IView
    {
        private readonly INavigationService _navigationService;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly IReserveringRepository _reserveringRepository;
        private readonly IResourceLoader _resourceLoader;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;

        private DispatcherTimer klokTimer;
        private DispatcherTimer inactivityTimer;
        private string _time;
        private string _date;

        private DateTimeFormatter dateFormatter = new DateTimeFormatter(YearFormat.Full, MonthFormat.Full, DayFormat.Default, DayOfWeekFormat.None, HourFormat.None, MinuteFormat.None, SecondFormat.None, new[] { "nl-NL" }, "NL", CalendarIdentifiers.Gregorian, ClockIdentifiers.TwentyFourHour);
        private DateTimeFormatter timeFormatter = new DateTimeFormatter(YearFormat.None, MonthFormat.None, DayFormat.None, DayOfWeekFormat.None, HourFormat.Default, MinuteFormat.Default, SecondFormat.None, new[] { "nl-NL" }, "NL", CalendarIdentifiers.Gregorian, ClockIdentifiers.TwentyFourHour);

        public KlokUserControlViewModel(INavigationService navigationService, IVerenigingRepository verenigingRepository, IReserveringRepository reserveringRepository, IResourceLoader resourceLoader, IAlertMessageService alertMessageService,
                                         IEventAggregator eventAggregator)
        {

            _navigationService = navigationService;
            _verenigingRepository = verenigingRepository;
            _reserveringRepository = reserveringRepository;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            klokTimer = new DispatcherTimer();
            klokTimer.Interval = TimeSpan.FromMinutes(1);
            klokTimer.Tick += klokTimer_Tick;
            klokTimer.Start();

            inactivityTimer = new DispatcherTimer();
            inactivityTimer.Interval = TimeSpan.FromMinutes(10);
            inactivityTimer.Tick += inactivityTimer_Tick;
            inactivityTimer.Start();

            _eventAggregator.GetEvent<KlokEvent>().Subscribe(UpdateKlokAsync);
            _eventAggregator.GetEvent<ActivityEvent>().Subscribe(UpdateActivityAsync);

            UpdateKlokAsync(new TimeSpan());
        }

        public string Time
        {
            get { return _time; }
            private set { SetProperty(ref _time, value); }
        }

        public string Date
        {
            get { return _date; }
            private set { SetProperty(ref _date, value); }
        }

        public async void UpdateKlokAsync(TimeSpan span)
        {
            await UpdateKlokInfoAsync(span);
        }

        private async Task UpdateKlokInfoAsync(TimeSpan span)
        {
            Date = dateFormatter.Format(DateTime.Now);
            Time = timeFormatter.Format(DateTime.Now);
            _verenigingRepository.UpdateKioskModeAsync();
            //Date = DateTime.Now.ToString("dd MMMM, yyyy");
            //Time = DateTime.Now.ToString("HH:mm");

        }

        public async void UpdateActivityAsync(TimeSpan span)
        {
            if(inactivityTimer == null) inactivityTimer = new DispatcherTimer();

            inactivityTimer.Interval = TimeSpan.FromMinutes(10);
            inactivityTimer.Tick += inactivityTimer_Tick;
            inactivityTimer.Start();
        }

        async void klokTimer_Tick(object sender, object e)
        {
            Date = dateFormatter.Format(DateTime.Now);
            Time = timeFormatter.Format(DateTime.Now);

            //Date = DateTime.Now.ToString("dd MMMM, yyyy");
            //Time = DateTime.Now.ToString("HH:mm");
            _eventAggregator.GetEvent<KlokEvent>().Publish(new TimeSpan());
        }

        async void inactivityTimer_Tick(object sender, object e)
        {
            await _reserveringRepository.ClearReserveringAsync();

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Sponsors", null);
            navigationServiceReference.Navigate("Sponsors", null);
            navigateAction();

            _eventAggregator.GetEvent<SponsorEvent>().Publish(null);
        }


        public object DataContext
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

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
using Windows.UI.Xaml;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class KlokUserControlViewModel : ViewModel, INotifyPropertyChanged, IKlokUserControlViewModel, IView
    {
        private readonly INavigationService _navigationService;
        private readonly IReserveringRepository _reserveringRepository;
        private readonly IResourceLoader _resourceLoader;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;

        private DispatcherTimer klokTimer;
        private DispatcherTimer inactivityTimer;
        private string _time;

        public KlokUserControlViewModel(INavigationService navigationService, IReserveringRepository reserveringRepository, IResourceLoader resourceLoader, IAlertMessageService alertMessageService,
                                         IEventAggregator eventAggregator)
        {

            _navigationService = navigationService;
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

        public async void UpdateKlokAsync(TimeSpan span)
        {
            await UpdateKlokInfoAsync(span);
        }

        private async Task UpdateKlokInfoAsync(TimeSpan span)
        {
            Time = DateTime.Now.ToString("HH:mm");

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
            Time = DateTime.Now.ToString("HH:mm");
            _eventAggregator.GetEvent<KlokEvent>().Publish(new TimeSpan());
        }

        async void inactivityTimer_Tick(object sender, object e)
        {
            await _reserveringRepository.ClearReserveringAsync();

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Main", null);
            navigationServiceReference.Navigate("Main", null);
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

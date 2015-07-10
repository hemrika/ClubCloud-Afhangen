using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.Interfaces;
using ClubCloud.Core.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class CloudUserControlViewModel : ViewModel, INotifyPropertyChanged, ICloudUserControlViewModel //, IView
    {
        private readonly IAlertRepository _alertRepository;
        private readonly INavigationService _navigationService;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;

        private DispatcherTimer cloudTimer;
        private DispatcherTimer cloudRefreshTimer;
        private ObservableCollection<CloudImage> CloudsList;
        private ImageSource _current;
        private ImageSource _next;
        private int currentIndex = 0;

        public CloudUserControlViewModel(IAlertRepository alertRepository, INavigationService navigationService, IAlertMessageService alertMessageService,
                                         IEventAggregator eventAggregator)
        {
            _alertRepository = alertRepository;
            _navigationService = navigationService;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            cloudTimer = new DispatcherTimer();

            cloudTimer.Interval = TimeSpan.FromMilliseconds(200);
            cloudTimer.Tick += cloudTimer_Tick;
            cloudTimer.Start();

            cloudRefreshTimer = new DispatcherTimer();
            cloudRefreshTimer.Interval = TimeSpan.FromMinutes(30);
            cloudRefreshTimer.Tick += cloudRefreshTimer_Tick;
            cloudRefreshTimer.Start();

            _eventAggregator.GetEvent<CloudUpdatedEvent>().Subscribe(UpdateCloudsAsync);

            UpdateCloudsAsync(false);
        }

        public void cloudRefreshTimer_Tick(object sender, object e)
        {
            UpdateCloudsAsync(true);
        }

        public long Ticks
        {
            get
            {
                return CloudsList[currentIndex].Ticks;
            }

        }
        public ImageSource Image
        {
            get
            {
                if (_current == null)
                    _current = CloudsList[currentIndex].Image;

                return _current;
            }
            private set
            {
                SetProperty(ref _current, value);
            }
        }

        public ImageSource NextImage
        {
            get
            {
                if (_next == null)
                    _next = CloudsList[currentIndex+1].Image;

                return _next;
            }
            private set
            {
                SetProperty(ref _next, value);
            }
        }

        void cloudTimer_Tick(object sender, object e)
        {
            if (currentIndex < CloudsList.Count -1)
                currentIndex++;
            else
                currentIndex = 0;

            Image = CloudsList[currentIndex].Image;

            try
            {
                NextImage = CloudsList[currentIndex + 1].Image;
            }
            catch
            {
                NextImage = CloudsList[0].Image;
            }
        }

        public async void UpdateCloudsAsync(bool update)
        {
            await UpdateCloudsInfoAsync(update);
        }

        private async Task UpdateCloudsInfoAsync(bool update)
        {
            if (CloudsList == null)           
                CloudsList = new ObservableCollection<CloudImage>();

            CloudsList.Clear();
                /*
                TimeSpan begin = new TimeSpan(DateTime.Now.Hour-1,0,0);
                TimeSpan now = DateTime.Now.TimeOfDay;
                for (TimeSpan span = begin; span < now; span = span.Add(new TimeSpan(0,5,0)))
                {
                    CloudImage gpsimage = null;
                
                    string gpscloud = "http://www2.buienradar.nl/gps/maps_00000_{0}.png";
                    //if (i < 1000)
                    gpsimage = new CloudImage(new Uri(string.Format(gpscloud, span.ToString("hhmm"))));
                    //else
                    //    image = new CloudImage(new Uri(string.Format(cloud, i)));

                    if (gpsimage != null)
                        CloudsList.Add(gpsimage);
                }
                */

                long filetime = DateTime.Now.ToFileTime();
                long filetimeutc = DateTime.Now.ToFileTimeUtc();

                for (int i = 0; i < 25; i++)
                {
                    CloudImage forecastimage = null;
                    string forecastcloud = "http://mijn.buienradar.nl/forecast/forecast_gps_{0}.png?{1}";

                    forecastimage = new CloudImage(new Uri(string.Format(forecastcloud, i, filetime)));
                    if (forecastimage != null)
                        CloudsList.Add(forecastimage);
                }
                //
            //}
        }

        public override async void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            _navigationService.Navigate("Main", null);
            base.OnNavigatedFrom(viewModelState, suspending);
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            if (navigationParameter != null)
                base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

        }
    }
}

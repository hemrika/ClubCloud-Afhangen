using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class WeerUserControlViewModel : ViewModel, INotifyPropertyChanged, IWeerUserControlViewModel //, IView
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly INavigationService _navigationService;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;

        private DispatcherTimer weatherTimer;
        private Afhang _afhang;
        private Geoposition _geoposition;
        private ObservableCollection<LocationModel> _locationModels;
        private CurrentConditionsModel _currentConditionsModel;
        private ObservableCollection<HourlyModel> _hourlyModels;
        private StorageFile _storageFile;
        public Foto _weerIcoon;

        public WeerUserControlViewModel(IWeatherRepository weatherRepository, ILocationRepository locationRepository, IVerenigingRepository verenigingRepository, IAlertRepository alertRepository, INavigationService navigationService, IAlertMessageService alertMessageService,
                                         IEventAggregator eventAggregator)
        {
            _weatherRepository = weatherRepository;
            _locationRepository = locationRepository;
            _verenigingRepository = verenigingRepository;
            _alertRepository = alertRepository;
            _navigationService = navigationService;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            weatherTimer = new DispatcherTimer();
            weatherTimer.Interval = TimeSpan.FromMinutes(30);
            weatherTimer.Tick += weatherTimer_Tick;
            weatherTimer.Start();

            _eventAggregator.GetEvent<WeatherUpdatedEvent>().Subscribe(UpdateWeatherAsync);

            UpdateWeatherAsync(false);
        }

        public CurrentConditionsModel CurrentConditions
        {
            get { return _currentConditionsModel; }
            private set { SetProperty(ref _currentConditionsModel, value); }

        }

        public byte[] WeerIcoon
        {
            get 
            {
                if (_currentConditionsModel == null && _weerIcoon == null) return new byte[0];

                Task.Run(async () => await UpdateWeerIcoonAsync(CurrentConditions));

                
                while(_weerIcoon.ContentData == null) { }

                return _weerIcoon.ContentData;
            }

            //private set { SetProperty(ref _weerIcoon.ContentData, value); }
        }


        public ObservableCollection<HourlyModel> HourlyModels
        {
            get { return _hourlyModels; }
            private set { SetProperty(ref _hourlyModels, value); }

        }

        void weatherTimer_Tick(object sender, object e)
        {
            _eventAggregator.GetEvent<WeatherUpdatedEvent>().Publish(true);
        }

        public async void UpdateWeatherAsync(bool update)
        {
            _weerIcoon = new Foto();
            await UpdateWeatherInfoAsync(update);
            
        }

        private async Task UpdateWeatherInfoAsync(bool update)
        {
            if (_afhang == null) _afhang = await _verenigingRepository.GetVerenigingSettingsAsync();
            if (_geoposition == null) _geoposition = await _locationRepository.GetLocationAsync();
            if (_locationModels == null) _locationModels = await _locationRepository.GetLocationsByLatLon(_geoposition.Coordinate.Point.Position.Latitude, _geoposition.Coordinate.Point.Position.Longitude);

            foreach (LocationModel location in _locationModels)
            {
                CurrentConditions = await _weatherRepository.GetCurrentConditionsAsync(location.LocationId, update, true);
                HourlyModels = await _weatherRepository.GetHourlyAsync(location.LocationId, update);
            }

            if(CurrentConditions != null)
            {
                await UpdateWeerIcoonAsync(CurrentConditions);
                /*
                _weerIcoon = new Foto();
                Uri WeatherIcon = new Uri(string.Format("ms-appx:///Assets/Weather/{0}.png", CurrentConditions.WeatherCode));
                StorageFile _storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(WeatherIcon);

                try
                {
                    IBuffer readbuffer = await FileIO.ReadBufferAsync(_storageFile);
                    _weerIcoon.ContentData = readbuffer.ToArray();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
                */
            }
        }

        private async Task UpdateWeerIcoonAsync(CurrentConditionsModel currentConditions)
        {
            if (_weerIcoon == null)
                _weerIcoon = new Models.Foto();

            _weerIcoon.ContentData = null;

            if (currentConditions == null)
            {
                Uri WeatherIcon = new Uri(string.Format("ms-appx:///Assets/Weather/{0}.png", "01"));
                _storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(WeatherIcon);

                try
                {
                    IBuffer readbuffer = await FileIO.ReadBufferAsync(_storageFile);
                    _weerIcoon.ContentData = readbuffer.ToArray();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
                return;
            }

            if (currentConditions != null)
            {
                Uri WeatherIcon = new Uri(string.Format("ms-appx:///Assets/Weather/{0}.png", CurrentConditions.WeatherCode.ToString("D2")));
                _storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(WeatherIcon);

                try
                {
                    IBuffer readbuffer = await FileIO.ReadBufferAsync(_storageFile);
                    _weerIcoon.ContentData = readbuffer.ToArray();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
                return;
            }
        }
        /*
        public object DataContext
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
        */
    }
}

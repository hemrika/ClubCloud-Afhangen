namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Afhangen.UILogic.Services;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using Microsoft.Practices.Prism.PubSubEvents;
    using Microsoft.Practices.Prism.StoreApps;
    using Microsoft.Practices.Prism.StoreApps.Interfaces;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Windows.Devices.Geolocation;

    public class WeerPageViewModel : ViewModel, IView
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly INavigationService _navigationService;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;

        private Vereniging _vereniging;
        private Afhang _afhang;
        private Geoposition _geoposition;
        private ObservableCollection<LocationModel> _locationModels;
        private CurrentConditionsModel _currentConditionsModel;
        private ObservableCollection<HourlyModel> _hourlyModels;

        public WeerPageViewModel(IWeatherRepository weatherRepository, ILocationRepository locationRepository, IVerenigingRepository verenigingRepository, IAlertRepository alertRepository, INavigationService navigationService, IAlertMessageService alertMessageService,
                                         IEventAggregator eventAggregator)
        {
            _weatherRepository = weatherRepository;
            _locationRepository = locationRepository;
            _verenigingRepository = verenigingRepository;
            _alertRepository = alertRepository;
            _navigationService = navigationService;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<WeatherUpdatedEvent>().Subscribe(UpdateWeatherPageAsync);

            UpdateWeatherAsync(false);
        }

        public CurrentConditionsModel CurrentConditions
        {
            get
            {
                if (_currentConditionsModel == null)
                    _currentConditionsModel = new CurrentConditionsModel();

                return _currentConditionsModel;
            }
            private set { SetProperty(ref _currentConditionsModel, value); }

        }

        public ObservableCollection<HourlyModel> HourlyModels
        {
            get
            {
                if (_hourlyModels == null)
                    _hourlyModels = new ObservableCollection<HourlyModel>();

                return _hourlyModels;
            }
            private set { SetProperty(ref _hourlyModels, value); }

        }
        public async void UpdateWeatherAsync(bool update)
        {
            await UpdateWeatherInfoAsync(update);
        }


        private void UpdateWeatherPageAsync(bool obj)
        {
            UpdateWeatherInfoAsync(false);
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

            //using (ClientContext clientCtx = new ClientContext("https://mijn.clubcloud.nl"))
            //{
            //    clientCtx.AuthenticationMode = ClientAuthenticationMode.FormsAuthentication;
            //    FormsAuthenticationLoginInfo fba = new FormsAuthenticationLoginInfo("12073385","rjm557308453!");
            //    clientCtx.FormsAuthenticationLoginInfo = fba;
            //    clientCtx.FormDigestHandlingEnabled = true;
            //    FormDigestInfo info = clientCtx.GetFormDigestDirect();
            //    string digest = info.DigestValue;
            //}

        }

        public Vereniging Vereniging
        {
            get { return _vereniging; }
            private set { SetProperty(ref _vereniging, value); }
        }

        private object _dataContext;

        object IView.DataContext
        {
            get
            {
                return _dataContext;
            }
            set
            {
                _dataContext = value;
            }
        }

        public override async void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            _navigationService.Navigate("Main", null);
            base.OnNavigatedFrom(viewModelState, suspending);
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            //await _reserveringRepository.ClearReserveringAsync();
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }


    }
}

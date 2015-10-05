namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Afhangen.UILogic.Services;
    using ClubCloud.Core.Prism;
    using ClubCloud.Core.Prism.Interfaces;
    using ClubCloud.Core.Prism.PubSubEvents;
    using System;
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
        private ObservableCollection<UVIndex> _uvindex;
        private ClubPosition _haversineposition;
        private ClubPosition _flatposition;

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

        public ClubPosition HaversinePosition
        {
            get
            {
                if (_haversineposition == null)
                    _haversineposition = new ClubPosition();

                return _haversineposition;
            }
            private set { SetProperty(ref _haversineposition, value); }
        }

        public ClubPosition FlatPosition
        {
            get
            {
                if (_flatposition == null)
                    _flatposition = new ClubPosition();

                return _flatposition;
            }
            private set { SetProperty(ref _flatposition, value); }
        }
        public ObservableCollection<UVIndex> UVIndexList
        {
            get
            {
                if (_uvindex == null)
                {

                    _uvindex = new ObservableCollection<UVIndex>() {
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/00.png"),"Ga gerust buiten tennissen.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/01.png"),"Ga gerust buiten tennissen."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/02.png"),"Ga gerust buiten tennissen."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/03.png"),"Niet langer dan 40 minuten tennissen of in de zon."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/04.png"),"Niet langer dan 40 minuten tennissen of in de zon."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/05.png"),"Niet langer dan 40 minuten tennissen of in de zon."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/06.png"),"Niet langer dan 20 minuten in de zon."+Environment.NewLine+"Ga niet tennissen.","SPF30+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/07.png"),"Niet langer dan 20 minuten in de zon."+Environment.NewLine+"Ga niet tennissen.","SPF30+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/08.png"),"Niet langer dan 20 minuten in de zon."+Environment.NewLine+"Ga niet tennissen.","SPF30+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/09.png"),"Zeer Hoog, ga niet buiten tennissen","SPF50+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/10.png"),"Zeer Hoog, ga niet buiten tennissen","SPF50+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/11.png"),"Extreem Hoog, ga niet buiten tennissen","SPF50+"),
                    };
                }

                return _uvindex;
            }
        }

        private TemperatureRange _TemperatureRange;
        public TemperatureRange TemperatureRange
        {
            get
            {
                if (_TemperatureRange == null)
                {
                    _TemperatureRange = new TemperatureRange { Maximum = new Measurement { Value = 0 }, Minimum = new Measurement { Value = 0 } };
                }
                return _TemperatureRange;
            }

            private set { SetProperty(ref _TemperatureRange, value); }
        }

        public async void UpdateWeatherAsync(bool update)
        {
            await UpdateWeatherInfoAsync(update);
        }


        private void UpdateWeatherPageAsync(bool obj)
        {
            UpdateWeatherInfoAsync(false);
        }

        public double AngleToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public double RadAngleToDegrees(double radAngle)
        {
            return radAngle * (180 / Math.PI);
        }

        public double LatitudeToMercator(double latitude)
        {
            return Math.Log(Math.Tan((90 + latitude) * Math.PI / 360)) / (Math.PI / 180);
            //return Math.Log(Math.Tan(latitude/2 + Math.PI/4));
        }

        public const int EarthGreatRadius = 6378137;

        private double GetResolution(double latitude)
        {
            return (EarthGreatRadius * 2 * Math.PI / 256) * Math.Cos(AngleToRadians(latitude)) / Math.Pow(2, 6);
        }

        public double HaversineDistance(double lat1, double lon1, double lat2,double lon2, DistanceUnit unit =  DistanceUnit.Kilometers)
        {
            double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
            var lat = AngleToRadians(lat2 - lat1);
            var lng = AngleToRadians(lon2 - lon1);

            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(AngleToRadians(lat1)) * Math.Cos(AngleToRadians(lat2)) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }

        public enum DistanceUnit { Miles, Kilometers };

        //private const double EarthRadius = 3958.756;
        private double FlatEarth(double lat1, double lon1, double lat2, double lon2, DistanceUnit unit = DistanceUnit.Kilometers)
        {
            double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
            
            /*
            lat1 = AngleToRadians(lat1);
            lon1 = AngleToRadians(lon1);
            lat2 = AngleToRadians(lat2);
            lon2 = AngleToRadians(lon2);
            */

            double dlat = Math.Abs(lat1 - lat2);
            if (dlat > 180) dlat = 360 - dlat;

            double dlon = Math.Abs(lon1 - lon2);
            if (dlon > 180) dlon = 360 - dlon;

            double x = 69.1 * dlat;
            double y = 53.0 * dlon;

            return Math.Sqrt(x * x + y * y);
        }

        private async Task UpdateWeatherInfoAsync(bool update)
        {

            if (_afhang == null) _afhang = await _verenigingRepository.GetVerenigingSettingsAsync();
            if (_geoposition == null) _geoposition = await _locationRepository.GetLocationAsync();
            if (_locationModels == null) _locationModels = await _locationRepository.GetLocationsByLatLon(_geoposition.Coordinate.Point.Position.Latitude, _geoposition.Coordinate.Point.Position.Longitude);

            foreach (LocationModel location in _locationModels)
            {
                CurrentConditions = await _weatherRepository.GetCurrentConditionsAsync(location.LocationId, update, true);
                ObservableCollection<HourlyModel> hourly = await _weatherRepository.GetHourlyAsync(location.LocationId, update);

                foreach (HourlyModel model in hourly)
                {
                    if (model.Date.Date == DateTime.Now.Date)
                    {
                        //if (_afhang.BaanBegin.Hours <= (int.Parse(model.ShortTime)) && (int.Parse(model.ShortTime) <= _afhang.BaanEinde.Hours))
                        //{
                            /*
                            if (TemperatureRange.Maximum.Value < double.Parse(model.PredictedTemperature))
                                TemperatureRange.Maximum.Value = double.Parse(model.PredictedTemperature);

                            if (TemperatureRange.Minimum.Value > double.Parse(model.PredictedTemperature))
                                TemperatureRange.Minimum.Value = double.Parse(model.PredictedTemperature);
                            */
                            HourlyModels.Add(model);
                        //}
                    }
                    //string time = model.ShortTime;

                }
                TemperatureRange.Maximum.Value++;
                TemperatureRange.Minimum.Value--;
            }

            /*
            var width = 750;
            var height = 750;
            */
            var south = 50.00;// AngleToRadians(50.00);
            var north = 55.00;// AngleToRadians(55.00);
            var west = 1.50;// AngleToRadians(1.50);
            var east = 9.00;// AngleToRadians(9.00);


            var ymin = LatitudeToMercator(south);
            var ymax = LatitudeToMercator(north);
            var xmin = LatitudeToMercator(west);
            var xmax = LatitudeToMercator(east);

            var htotkmy = HaversineDistance(north, west, north, east);
            var htotkmx = HaversineDistance(north, west, south, west);
            var hpxperkmy = 750 / htotkmy;
            var hpxperkmx = 730 / htotkmx;

            var ftotkmy = FlatEarth(north, west, north, east);
            var ftotkmx = FlatEarth(north, west, south, west);
            var fpxperkmy = 750 / ftotkmy;
            var fpxperkmx = 730 / ftotkmx;
            
            //var hkmx = HaversineDistance(north, west, north, 6.597290 /*_geoposition.Coordinate.Point.Position.Longitude*/);
            //var hkmy = HaversineDistance(north, west, 53.173119 /*_geoposition.Coordinate.Point.Position.Latitude*/, west);

            //var fkmx = FlatEarth(north, west, north, 6.597290 /*_geoposition.Coordinate.Point.Position.Longitude*/);
            //var fkmy = FlatEarth(north, west, 53.173119 /*_geoposition.Coordinate.Point.Position.Latitude*/, west);

            var hkmx = HaversineDistance(north, west, north, _geoposition.Coordinate.Point.Position.Longitude);
            var hkmy = HaversineDistance(north, west, _geoposition.Coordinate.Point.Position.Latitude, west);

            var fkmx = FlatEarth(north, west, north, _geoposition.Coordinate.Point.Position.Longitude);
            var fkmy = FlatEarth(north, west, _geoposition.Coordinate.Point.Position.Latitude, west);

            /*
            AltitudeReferenceSystem system = _geoposition.Coordinate.Point.AltitudeReferenceSystem;

            var lat = AngleToRadians(_geoposition.Coordinate.Point.Position.Latitude);
            var lon = AngleToRadians(_geoposition.Coordinate.Point.Position.Longitude);

            var diflat = AngleToRadians(_geoposition.Coordinate.Point.Position.Latitude - 55.974);
            var diflon = AngleToRadians(_geoposition.Coordinate.Point.Position.Longitude - 0.0);

            var h1 = Math.Sin(diflat / 2) * Math.Sin(diflat / 2) + Math.Cos(north) * Math.Cos(lat) * Math.Sin(diflon / 2) * Math.Sin(diflon / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            var meters = 6371 * h2;
            */
            /*
            var xFactor = width/(east - west);
            var yFactor = height/(ymax - ymin);

            //var resolution = GetResolution(_geoposition.Coordinate.Point.Position.Latitude);

            var lat = AngleToRadians(_geoposition.Coordinate.Point.Position.Latitude / 60);
            var lon = AngleToRadians(_geoposition.Coordinate.Point.Position.Longitude / 60);

            var x = lon;
            var y = LatitudeToMercator(lat);

            Position.X = ((x - west) * xFactor);///width;// *(xFactor / resolution);
            Position.Y = ((ymax - y) * yFactor);///height;// *(yFactor / resolution);
            */

            //double y = 180 / Math.PI * Math.Log10((Math.Tan(Math.PI / 4 + _geoposition.Coordinate.Point.Position.Latitude * (Math.PI / 180) / 2)));
            //double x = _geoposition.Coordinate.Point.Position.Latitude;

            HaversinePosition.Y = (hkmy * hpxperkmy);
            HaversinePosition.X = (hkmx * hpxperkmx);

            FlatPosition.Y = (fkmy * fpxperkmy);
            FlatPosition.X = (fkmx * fpxperkmx);

            //var boundaries = new GLatLngBounds(new GLatLng(48.895, 0), new GLatLng(55.974, 10.856));
            //if (_locationModels == null) _locationModels = await _locationRepository.GetLocationsByLatLon(52.160114, 4.49701);
            //double Latitude = 52.160114;
            //double Longitude = 4.49701;

            //var sinx = Math.Sin((Math.PI * (Longitude - 48.895) /*_geoposition.Coordinate.Point.Position.Longitude*/ / 180.0));
            //var x = 365 - (Math.Log((1 + sinx) / (1 - sinx)) / 2) * RadsPerDegre;
            ////var x = PixelPerLatitude * (Longitude - 48.895);
            ////var y = PixelPerLongintude * (10.856 - Longitude);
            ////var x = PixelLongintudeOrigin + (PixelsPerLongintudeDegre *  Longitude);// _geoposition.Coordinate.Point.Position.Longitude;
            //var siny = Math.Sin((Math.PI * (10.856 - Longitude) /*_geoposition.Coordinate.Point.Position.Latitude*/ / 180.0));
            //var y = 365 - (Math.Log((1 + siny) / (1 - siny)) / 2) * RadsPerDegre;

            //Position.X = 365;
            //Position.Y = 365;
     


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
            if (navigationParameter != null)
                base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

        }


    }
}

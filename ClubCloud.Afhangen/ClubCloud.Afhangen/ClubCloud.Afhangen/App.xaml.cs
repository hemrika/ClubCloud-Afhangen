namespace ClubCloud.Afhangen
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Afhangen.UILogic.Services;
    using ClubCloud.Afhangen.UILogic.ViewModels;
    using ClubCloud.Afhangen.Views;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using Microsoft.Practices.Prism.PubSubEvents;
    using Microsoft.Practices.Prism.StoreApps;
    using Microsoft.Practices.Prism.StoreApps.Interfaces;
    using Microsoft.Practices.Unity;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Activation;
    using Windows.ApplicationModel.Resources;
    using Windows.Devices.Geolocation;
    using Windows.System;
    using Windows.UI.ApplicationSettings;
    using Windows.UI.Xaml;

    sealed partial class App : MvvmAppBase
    {
        private readonly IUnityContainer _container = new UnityContainer();

        public IEventAggregator EventAggregator { get; set; }

        public App()
        {
            this.InitializeComponent();
            this.RequestedTheme = ApplicationTheme.Light;
            ExtendedSplashScreenFactory = (splashscreen) => new ExtendedSplashScreen(splashscreen);
        }

        protected async override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                await this.LoadAppResources();
            }

            NavigationService.Navigate("Main", null);
        }

        /*
        protected override async Task OnLaunchApplication(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                await this.LoadAppResources();
            }

            NavigationService.Navigate("Main", null);
        }
        */

        private async Task LoadAppResources()
        {
            LocationServiceProxy locationService = (LocationServiceProxy)_container.Resolve<ILocationService>();
            //CardReaderService cardReaderService = (CardReaderService)_container.Resolve<ICardReaderService>();

            //List<MSRReader> readers = await cardReaderService.Connect();

            //string result = string.Empty;

            //foreach (MSRReader reader in readers)
            //{
            //    result += await cardReaderService.ListenForInput(reader);
            //    result += Environment.NewLine;
            //}

            //int length = result.Length;

            //await Task.Delay(2000);
            if (locationService.GetIsInternetConnected())
            {
                Geoposition position = await locationService.GetLocationAsync();

                VerenigingRepository verenigingRepository = (VerenigingRepository)_container.Resolve<IVerenigingRepository>();
                Vereniging vereniging = await verenigingRepository.GetVerenigingByLocatieAsync(position.Coordinate.Point.Position.Longitude, position.Coordinate.Point.Position.Latitude);
                
                while (vereniging == null) { };

                BaanRepository baanRepository = (BaanRepository)_container.Resolve<IBaanRepository>();
                await baanRepository.GetBanenAsync(vereniging.Id);

                ReserveringRepository reserveringRepository = (ReserveringRepository)_container.Resolve<IReserveringRepository>();
                await reserveringRepository.GetReserveringenAsync();
            }
            else
            {
                NavigationService.Navigate("Settings", null);
            }
            await Task.Delay(2000);
        }

        //protected override void OnInitialize(IActivatedEventArgs args)
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            EventAggregator = new EventAggregator();
            _container.RegisterInstance<INavigationService>(NavigationService);
            _container.RegisterInstance<IEventAggregator>(EventAggregator);
            _container.RegisterInstance(SessionStateService);
            _container.RegisterInstance<INavigationService>(NavigationService);
            _container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));

            _container.RegisterType<ICacheService, TemporaryFolderCacheService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());
            //_container.RegisterType<ICardReaderService, CardReaderService>(new ContainerControlledLifetimeManager());

            // Register repositories 
            _container.RegisterType<IReserveringRepository, ReserveringRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IVerenigingRepository, VerenigingRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IBaanRepository, BaanRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISpelerRepository, SpelerRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAlertRepository, AlertRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ILocationRepository, LocationRepository>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IWeatherRepository, WeatherRepository>(new ContainerControlledLifetimeManager());
            //IAlertRepository
            //ILocationRepository
            //IWeatherRepository
            // Register web service proxies 
            _container.RegisterType<IReserveringService, ReserveringServiceProxy>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IVerenigingService, VerenigingServiceProxy>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISpelerService, SpelerServiceProxy>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IBaanService, BaanServiceProxy>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ILocationService, LocationServiceProxy>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAlertMessageService, AlertMessageService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IWeatherService, WeatherServiceProxy>(new ContainerControlledLifetimeManager());

            // Register child view models
            _container.RegisterType<IReserveringUserControlViewModel, ReserveringUserControlViewModel>();
            _container.RegisterType<ISpelerUserControlViewModel, SpelerUserControlViewModel>();
            _container.RegisterType<IKlokUserControlViewModel, KlokUserControlViewModel>();
            _container.RegisterType<IBaanUserControlViewModel, BaanUserControlViewModel>();            

            
            //DependencyProperty property = ViewModelLocator.AutoWireViewModelProperty;
            //ViewModelLocator.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>

            //ViewModelLocator.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            //{
            //    var viewModelTypeName = string.Format(CultureInfo.InvariantCulture, "AdventureWorks.UILogic.ViewModels.{0}ViewModel, AdventureWorks.UILogic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=634ac3171ee5190a", viewType.Name);
            //    var viewModelType = Type.GetType(viewModelTypeName);
            //    return viewModelType;
            //});

            ViewModelLocationProvider.SetDefaultViewModelFactory((viewModelType) => _container.Resolve(viewModelType));

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewModelTypeName = string.Format(CultureInfo.InvariantCulture, "ClubCloud.Afhangen.UILogic.ViewModels.{0}ViewModel, ClubCloud.Afhangen.UILogic", viewType.Name);
                var viewModelType = Type.GetType(viewModelTypeName);
                return viewModelType;
            });
            
            var resourceLoader = _container.Resolve<IResourceLoader>();

            return base.OnInitializeAsync(args);
        }

        protected override IList<SettingsCommand> GetSettingsCommands()
        {
            var settingsCommands = new List<SettingsCommand>();
            var resourceLoader = _container.Resolve<IResourceLoader>();

            settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("OptionsFlyout/Title"), (c) => new OptionsFlyout().Show()));
            settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("PrivacyPolicyText"), async (c) => await Launcher.LaunchUriAsync(new Uri(resourceLoader.GetString("PrivacyPolicyUrl")))));
            settingsCommands.Add(new SettingsCommand(Guid.NewGuid().ToString(), resourceLoader.GetString("AboutText"), (c) => new AboutFlyout().Show()));

            return settingsCommands;
        }

        protected override object Resolve(Type type)
        {
            object resolved =_container.Resolve(type);
            return _container.Resolve(type);
        }

    }
}

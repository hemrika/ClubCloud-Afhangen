namespace ClubCloud.Afhangen
{
    using ClubCloud.Afhangen.Common;
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Afhangen.UILogic.Services;
    using ClubCloud.Afhangen.UILogic.ViewModels;
    using ClubCloud.Afhangen.Views;
    using ClubCloud.Core.Prism;
    using ClubCloud.Core.Prism.Interfaces;
    using ClubCloud.Core.Prism.PubSubEvents;
    using ClubCloud.Core.Unity;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Activation;
    using Windows.ApplicationModel.Resources;
    using Windows.Devices.Geolocation;
    using Windows.Storage;
    using Windows.System;
    using Windows.UI.ApplicationSettings;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

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
        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {
            try
            {

            var rootFrame = await InitializeFrameAsync(args);

            string tileId = AppManifestHelper.GetApplicationId();

            if (rootFrame != null)
            {
                await OnLaunchApplicationAsync(args);
            }

            // Ensure the current window is active 
            //Window.Current.Activate(); 

            //base.OnLaunched(args);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
        */

        protected async override void OnActivated(IActivatedEventArgs args)
        {
            try
            {

                var rootFrame = await InitializeFrameAsync(args);
                string tileId = AppManifestHelper.GetApplicationId();

                if (rootFrame != null)
                {
                    ProtocolActivatedEventArgs activatedargs = (ProtocolActivatedEventArgs)args;
                    if (args.PreviousExecutionState != ApplicationExecutionState.Running)
                    {
                        await this.LoadAppResources();
                    }

                    NavigationService.Navigate("Main", null);
                }

                //base.OnActivated(args);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

        }

        private async Task LoadAppResources()
        {
            try
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
                await Task.Delay(2000);
                Vereniging vereniging = null;
                object VerenigingsId;
                VerenigingRepository verenigingRepository = (VerenigingRepository)_container.Resolve<IVerenigingRepository>();
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue("VerenigingsId", out VerenigingsId))
                {
                    vereniging = await verenigingRepository.GetVerenigingByNummerAsync(VerenigingsId.ToString());
                }
                else
                {
                    Geoposition position = await locationService.GetLocationAsync();
                    vereniging = await verenigingRepository.GetVerenigingByLocatieAsync(position.Coordinate.Point.Position.Longitude, position.Coordinate.Point.Position.Latitude);

                }

                while (vereniging == null) { };

                BaanRepository baanRepository = (BaanRepository)_container.Resolve<IBaanRepository>();
                await baanRepository.GetBanenAsync(vereniging.Id, vereniging.AccommodatieId);

                ReserveringRepository reserveringRepository = (ReserveringRepository)_container.Resolve<IReserveringRepository>();
                await reserveringRepository.GetReserveringenAsync();

                SponsorRepository sponsorRepository = (SponsorRepository)_container.Resolve<ISponsorRepository>();
                await sponsorRepository.GetSponsorsAsync(vereniging.Id);

                try
                {
                    verenigingRepository.UpdateStoreAgentAsync();
                }
                catch { }
            }
            
            await Task.Delay(2000);
            }
            catch (Exception ex)
            {
                string messsage = ex.Message;
                //throw;
            }
        }

        //protected override void OnInitialize(IActivatedEventArgs args)
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            try
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
                _container.RegisterType<ISponsorRepository, SponsorRepository>(new ContainerControlledLifetimeManager());
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
                _container.RegisterType<ISponsorService, SponsorServiceProxy>(new ContainerControlledLifetimeManager());

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
            catch (Exception ex)
            {
                string message = ex.Message;
                //throw;
            }
            return null;
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

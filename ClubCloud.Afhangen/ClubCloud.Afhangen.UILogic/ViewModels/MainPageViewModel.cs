namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using Microsoft.Practices.Prism.StoreApps;
    using Microsoft.Practices.Prism.StoreApps.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;

    public class MainPageViewModel : ViewModel, IView
    {
        private readonly INavigationService _navigationService;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly IReserveringRepository _reserveringRepository;
        private Vereniging _vereniging;

        public MainPageViewModel(INavigationService navigationService, IVerenigingRepository verenigingRepository, IReserveringRepository reserveringRepository)
        {
            _navigationService = navigationService;
            _verenigingRepository = verenigingRepository;
            _reserveringRepository = reserveringRepository;
            UpdateMainAsync(null);
        }

        public async void UpdateMainAsync(object notUsed)
        {
            await UpdateMainInfoAsync();
        }

        private async Task UpdateMainInfoAsync()
        {
            Vereniging = await _verenigingRepository.GetVerenigingAsync();
        }

        public Vereniging Vereniging
        {
            get { return _vereniging; }
            private set { SetProperty(ref _vereniging, value); }
        }

        public string Version
        {
            get
            {
                PackageVersion version = Package.Current.Id.Version;
                string appVersion = string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
                return appVersion;
            }
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
            //_navigationService.Navigate("Main", null);
            base.OnNavigatedFrom(viewModelState, suspending);
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            await _reserveringRepository.ClearReserveringAsync();
            if (navigationParameter != null)
                base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}

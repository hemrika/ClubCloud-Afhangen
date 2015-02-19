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

    public class WeerPageViewModel : ViewModel, IView
    {
        private readonly INavigationService _navigationService;
        private readonly IVerenigingRepository _verenigingRepository;
        private Vereniging _vereniging;

        public WeerPageViewModel(INavigationService navigationService, IVerenigingRepository verenigingRepository)
        {
            _navigationService = navigationService;
            _verenigingRepository = verenigingRepository;

            UpdateMainAsync(null);
        }

        public async void UpdateMainAsync(object notUsed)
        {
            await UpdateMainInfoAsync();
        }

        private async Task UpdateMainInfoAsync()
        {
            Vereniging = await _verenigingRepository.GetVerenigingAsync();

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

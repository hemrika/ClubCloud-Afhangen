namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using Microsoft.Practices.Prism.StoreApps;
    using Microsoft.Practices.Prism.StoreApps.Interfaces;
    using System.Threading.Tasks;

    public class MainPageViewModel : ViewModel, IView
    {
        private readonly INavigationService _navigationService;
        private readonly IVerenigingRepository _verenigingRepository;
        private Vereniging _vereniging;

        public MainPageViewModel(INavigationService navigationService, IVerenigingRepository verenigingRepository)
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

    }
}

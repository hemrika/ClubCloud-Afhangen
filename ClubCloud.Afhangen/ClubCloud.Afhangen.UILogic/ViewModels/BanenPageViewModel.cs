using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism.Commands;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.Interfaces;
using ClubCloud.Core.Prism.PubSubEvents;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using System.Linq;
using System;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class BanenPageViewModel : ViewModel, IView
    {        
        private IBaanRepository _baanRepository;
        private IVerenigingRepository _verenigingRepository;
        private IReserveringRepository _reserveringRepository;
        private INavigationService _navigationService;
        //private IReserveringUserControlViewModel _reserveringUserControlViewModel;
        private IResourceLoader _resourceLoader;
        private IAlertMessageService _alertMessageService;
        private IEventAggregator _eventAggregator;
        private Vereniging _vereniging;
        public Reservering _reservering;

        private ObservableCollection<BaanUserControlViewModel> _banen;
        public BanenPageViewModel(IBaanRepository baanRepository, IVerenigingRepository verenigingRepository, IReserveringRepository reserveringRepository, INavigationService navigationService,
                                    /* IReserveringUserControlViewModel reserveringUserControlViewModel, */
            IResourceLoader resourceLoader, IAlertMessageService alertMessageService, IEventAggregator eventAggregator)
        {
            _baanRepository = baanRepository;
            _verenigingRepository = verenigingRepository;
            _reserveringRepository = reserveringRepository;
            _navigationService = navigationService;
            //_reserveringUserControlViewModel = reserveringUserControlViewModel;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            GoBackCommand = new DelegateCommand(_navigationService.GoBack);
            GoNextCommand = new DelegateCommand(GoNext);

            if (_eventAggregator != null)
            {
                //_eventAggregator.GetEvent<ReserveringUpdatedEvent>().Subscribe(UpdateBanenAsync);
                //_eventAggregator.GetEvent<SpelerUpdatedEvent>().Subscribe(UpdateBanenAsync);
                _eventAggregator.GetEvent<BaanUpdatedEvent>().Subscribe(UpdateBanenAsync);
            }

            //WijzigBaanCommand = DelegateCommand.FromAsyncHandler(BaanWijzigen);

            UpdateBanenAsync(null);

        }

        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand GoNextCommand { get; private set; }

        //public IReserveringUserControlViewModel ReserveringViewModel { get { return _reserveringUserControlViewModel; } }

        public ObservableCollection<BaanUserControlViewModel> Banen
        {
            get { return _banen; }
            private set { SetProperty(ref _banen, value); }
        }

        public async void UpdateBanenAsync(object notUsed)
        {
            await UpdateBanenInfoAsync();
        }

        private async Task UpdateBanenInfoAsync()
        {
            _vereniging = await _verenigingRepository.GetVerenigingAsync();
            _reservering =  await _reserveringRepository.GetReserveringAsync();
            List<Baan> banen = await _baanRepository.GetBanenAsync(_vereniging.Id, _vereniging.AccommodatieId);
            
            ObservableCollection<BaanUserControlViewModel> UnorderedBanen = new ObservableCollection<BaanUserControlViewModel>();
            foreach (Baan baan in banen)
            {
                UnorderedBanen.Add(new BaanUserControlViewModel(baan, _baanRepository, _reserveringRepository, _verenigingRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator));
            }

            ObservableCollection<BaanUserControlViewModel> OrderedBanen = new ObservableCollection<BaanUserControlViewModel>(UnorderedBanen.OrderBy(b => b.BeginTijd).ThenByDescending(b => b.Duur).ThenBy(b => b.Nummer));

            Banen = new ObservableCollection<BaanUserControlViewModel>();

            foreach (BaanUserControlViewModel baan in OrderedBanen)
            {
                Banen.Add(baan);
            }
            
            /*
            Banen = new ObservableCollection<BaanUserControlViewModel>();
            foreach (Baan baan in banen)
            {
                Banen.Add(new BaanUserControlViewModel(baan, _baanRepository, _reserveringRepository,_verenigingRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator));
            }
            */
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

        private async void GoNext()
        {
            IsReserveringInvalid = ReserveringViewModel.ValidateForm() == false;

            if (IsReserveringInvalid)
            {
                if(ReserveringViewModel.IsBaanSelected())
                {
                    return;
                }

                if (ReserveringViewModel.IsSpelerSelected())
                {
                    _navigationService.Navigate("Spelers", null);
                }
            }

            _navigationService.Navigate("Bevestigen", null);

            string errorMessage = string.Empty;
        }

        public bool IsReserveringInvalid { get; set; }

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

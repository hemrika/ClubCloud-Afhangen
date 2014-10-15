using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class SpelersPageViewModel : ViewModel //, IView
    {
        private ISpelerRepository _spelerRepository;
        private IVerenigingRepository _verenigingRepository;
        private IReserveringRepository _reserveringRepository;
        private INavigationService _navigationService;
        private IReserveringUserControlViewModel _reserveringUserControlViewModel;
        private IResourceLoader _resourceLoader;
        private IAlertMessageService _alertMessageService;
        private IEventAggregator _eventAggregator;
        private Vereniging _vereniging;
        public Reservering _reservering;
        private ObservableCollection<SpelerUserControlViewModel> _spelers;


        public SpelersPageViewModel(ISpelerRepository spelerRepository, IVerenigingRepository verenigingRepository, IReserveringRepository reserveringRepository, INavigationService navigationService,
                                         IReserveringUserControlViewModel reserveringUserControlViewModel, IResourceLoader resourceLoader, IAlertMessageService alertMessageService,
                                         IEventAggregator eventAggregator)
        {
            _spelerRepository = spelerRepository;
            _verenigingRepository = verenigingRepository;
            _reserveringRepository = reserveringRepository;
            _navigationService = navigationService;
            _reserveringUserControlViewModel = reserveringUserControlViewModel;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            GoBackCommand = new DelegateCommand(_navigationService.GoBack);
            GoNextCommand = new DelegateCommand(GoNext);

            if (_eventAggregator != null)
            {
                //_eventAggregator.GetEvent<ReserveringUpdatedEvent>().Subscribe(UpdateSpelersAsync);
                _eventAggregator.GetEvent<SpelerUpdatedEvent>().Subscribe(UpdateSpelersAsync);
                //_eventAggregator.GetEvent<BaanUpdatedEvent>().Subscribe(UpdateSpelersAsync);
            }

            //WijzigBaanCommand = DelegateCommand.FromAsyncHandler(BaanWijzigen);

            UpdateSpelersAsync(null);

        }

        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand GoNextCommand { get; private set; }

        //public IReserveringUserControlViewModel ReserveringViewModel { get { return _reserveringUserControlViewModel; } }

        public ObservableCollection<SpelerUserControlViewModel> Spelers
        {
            get { return _spelers; }
            private set { SetProperty(ref _spelers, value); }
        }


        public override async void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
        }

        public async void UpdateSpelersAsync(object notUsed)
        {
            await UpdateSpelersInfoAsync();
        }

        private async Task UpdateSpelersInfoAsync()
        {
            _vereniging = await _verenigingRepository.GetVerenigingAsync();

            _reservering = await _reserveringRepository.GetReserveringAsync();

            Spelers = new ObservableCollection<SpelerUserControlViewModel>();

            Speler emptySpeler = new Speler { Id = Guid.Empty };
            for (int i = 0; i < 4; i++)
            {
                Speler speler = emptySpeler;
                try
                {
                    if (_reservering.Spelers.Count >= i + 1)
                        speler = _reservering.Spelers[i];

                }
                catch
                {
                    speler = emptySpeler;
                }

                SpelerUserControlViewModel spelerViewModel = new SpelerUserControlViewModel(i, speler, _spelerRepository, _reserveringRepository, _verenigingRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
                //var spelerViewModel = new SpelerViewModel(i, speler, _spelerRepository, _reserveringRepository, _verenigingRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
                Spelers.Insert(i, spelerViewModel);
                //_eventAggregator.GetEvent<SpelerUpdatedEvent>().Publish(speler);
                //SpelerViewModels[i] = spelerViewModel;
                //OnPropertyChanged("Spelers");

            }

            /*
            List<Baan> banen = await _baanRepository.GetBanenAsync(_vereniging.Id);
            Banen = new ObservableCollection<BaanViewModel>();

            foreach (Baan baan in banen)
            {
                Banen.Add(new BaanViewModel(baan, _baanRepository, _reserveringRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator));
            }
            */
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            /*
            if (viewModelState == null) return;

            if (navigationMode == NavigationMode.New)
            {
                viewModelState["ReserveringViewModel"] = new Dictionary<string, object>();
            }

            ReserveringViewModel.OnNavigatedTo(navigationParameter, navigationMode, viewModelState["ReserveringViewModel"] as Dictionary<string, object>);
            */
            /*
            _vereniging = await _verenigingRepository.GetVerenigingAsync();
            _reservering = await _reserveringRepository.GetReserveringAsync();

            SpelerViewModels = new ObservableCollection<SpelerViewModel>();

            Speler emptySpeler = new Speler { Id = Guid.Empty };
            for (int i = 0; i < 4; i++)
            {
                Speler speler = emptySpeler;
                try
                {
                    if (_reservering.Spelers.Count >= i + 1)
                        speler = _reservering.Spelers[i];

                }
                catch
                {
                    speler = emptySpeler;
                }

                var spelerViewModel = new SpelerViewModel(i, speler, _spelerRepository, _reserveringRepository,_verenigingRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
                SpelerViewModels.Insert(i, spelerViewModel);
                
                //SpelerViewModels[i] = spelerViewModel;
                //OnPropertyChanged("Spelers");

            }
            */
            /*
            foreach (Speler speler in _reservering.Spelers)
            {
                Spelers.Add(new SpelerViewModel(speler, _spelerRepository, _reserveringRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator));
            }
            */

            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }

        private async void GoNext()
        {
            IsReserveringInvalid = ReserveringViewModel.ValidateForm() == false;

            if (IsReserveringInvalid)
            {
                if (ReserveringViewModel.IsSpelerSelected())
                {
                    return;
                }

                if (ReserveringViewModel.IsBaanSelected())
                {
                    _navigationService.Navigate("Banen", null); ;
                }
            }

            _navigationService.Navigate("Reservering", null);

            string errorMessage = string.Empty;
        }

        public bool IsReserveringInvalid { get; set; }

        /*
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
        */

    }
}

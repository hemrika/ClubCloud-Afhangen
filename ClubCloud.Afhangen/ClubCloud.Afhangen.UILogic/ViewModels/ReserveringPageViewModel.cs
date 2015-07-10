using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism.Commands;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.Interfaces;
using ClubCloud.Core.Prism.PubSubEvents;
using ClubCloud.Core.Prism.Interfaces;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class ReserveringPageViewModel : ViewModel, IView, INotifyPropertyChanged
    {
        private Reservering _reservering;
        private readonly IReserveringRepository _reserveringRepository;
        private readonly IBaanRepository _baanRepository;
        private readonly ISpelerRepository _spelerRepository;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _resourceLoader;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;

        public ReserveringPageViewModel(IReserveringRepository reserveringRepository, IBaanRepository baanRepository, ISpelerRepository spelerRepository, IVerenigingRepository verenigingRepository, INavigationService navigationService,
                                         IResourceLoader resourceLoader, IAlertMessageService alertMessageService,
                                         IEventAggregator eventAggregator)
        {
            _reserveringRepository = reserveringRepository;
            _baanRepository = baanRepository;
            _spelerRepository = spelerRepository;
            _verenigingRepository = verenigingRepository;
            _navigationService = navigationService;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            BevestigenCommand = new DelegateCommand<Nullable<Guid>>(ReserveringBevestigen);
            AnnulerenCommand = new DelegateCommand(ReserveringAnnuleren);
            VerwijderenCommand = new DelegateCommand<Nullable<Guid>>(ReserveringVerwijderen);

            if (eventAggregator != null)
            {
                eventAggregator.GetEvent<ReserveringUpdatedEvent>().Subscribe(UpdateReserveringAsync);
            }

            UpdateReserveringAsync(null);
        }
        
        private async void ReserveringVerwijderen(Nullable<Guid> reserveringId)
        {
            if(reserveringId.Value == Guid.Empty)
            {
                await _reserveringRepository.ClearReserveringAsync();

                Action navigateAction = null;
                var navigationServiceReference = _navigationService;

                navigateAction = () => navigationServiceReference.Navigate("Main", null);
                navigateAction = async () =>
                {
                    navigationServiceReference.Navigate("Main", null);
                };

                navigateAction();
            }
            else
            {
                bool deleted = await _reserveringRepository.DeleteReserveringAsync(reserveringId.Value);
                if(deleted)
                {
                    await _reserveringRepository.ClearReserveringAsync();

                    Action navigateAction = null;
                    var navigationServiceReference = _navigationService;

                    navigateAction = () => navigationServiceReference.Navigate("Main", null);
                    navigateAction = async () =>
                    {
                        navigationServiceReference.Navigate("Main", null);
                    };

                    navigateAction();
                }
            }
        }

        private async void ReserveringAnnuleren()
        {
            await _reserveringRepository.ClearReserveringAsync();

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Main", null);
            navigateAction = async () =>
            {
                navigationServiceReference.Navigate("Main", null);
            };

            navigateAction();

        }

        private async void ReserveringBevestigen(Nullable<Guid> reserveringId)
        {
            Reservering reservering = await _reserveringRepository.GetReserveringByIdAsync(reserveringId.Value);
            reservering.Soort = ReserveringSoort.Afhangen;
            reservering = await _reserveringRepository.SetReserveringAsync(reservering);

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Main", null);
            navigateAction = async () =>
            {
                navigationServiceReference.Navigate("Main", null);
            };

            navigateAction();

        }

        public DelegateCommand<Nullable<Guid>> BevestigenCommand { get; set; }

        public DelegateCommand AnnulerenCommand { get; set; }

        public DelegateCommand<Nullable<Guid>> VerwijderenCommand { get; set; }

        private string _reserveringType;
        public string ReserveringType
        {
            get { return _reserveringType; }
            private set { SetProperty(ref _reserveringType, value); }
        }

        /*
        public string ActionName
        {
            get
            {
                if (KanBevestigen())
                {
                    return "Bevestigen";
                }

                return "Bevestigen";
            }
        }

        public DelegateCommand Action
        {
            get
            {
                if (KanBevestigen())
                {
                    return new DelegateCommand(ReserveringBevestigen);
                }

                return new DelegateCommand(ReserveringBevestigen);
            }
        }
        */

        private async Task Verwijderen()
        {
            await _reserveringRepository.ClearReserveringAsync();
        }

        public bool KanBevestigen
        {
            get
            {
                if (_reservering == null) return false;

                return (_reservering != null && _reservering.Spelers.Count > 0 && _reservering.Duur > TimeSpan.FromMinutes(0));
            }
        }

        public bool KanVerwijderen
        {
            get
            {
                if (_reservering == null) return false;

                return (_reservering.Id != Guid.Empty);
            }
        }
        public ObservableCollection<Speler> Spelers
        {
            get { return _reservering.Spelers; }
            //private set { SetProperty(ref _spelerViewModels, value); }
        }

        public int AantalSpelers
        {
            get
            {
                return Spelers.Where(s => s.Id != Guid.Empty).Count();
            }
        }
        public Baan Baan
        {
            get { return _reservering.Baan; }
            //private set { SetProperty(ref _baanViewModel, value); }
        }

        public Guid BaanId
        {
            get { return _reservering.BaanId.Value; }
            //private set { SetProperty(ref _baanViewModel, value); }
        }

        public TimeSpan BeginTijd
        {
            get { return _reservering.BeginTijd; }
            //private set { SetProperty(ref _baanViewModel, value); }
        }

        public DateTime Datum
        {
            get { return _reservering.Datum; }
            //private set { SetProperty(ref _baanViewModel, value); }
        }

        public TimeSpan Duur
        {
            get { return _reservering.Duur; }
            //private set { SetProperty(ref _baanViewModel, value); }
        }

        public TimeSpan EindTijd
        {
            get { return _reservering.EindTijd; }
            //private set { SetProperty(ref _baanViewModel, value); }
        }

        public bool Final
        {
            get { return _reservering.Final; }
            //private set { SetProperty(ref _baanViewModel, value); }
        }

        public Guid Id
        {
            get { return _reservering.Id; }
            //private set { SetProperty(ref _baanViewModel, value); }
        }

        private async Task GoToNextPageAsync()
        {
            // Set up navigate action depending on the application's state
            var navigateAction = await ResolveNavigationActionAsync();

            // Execute the navigate action
            navigateAction();
        }

        private async Task<Action> ResolveNavigationActionAsync()
        {
            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Reservering", null);

            var reserveringReference = _reservering;
            var reserveringRepositoryReference = _reserveringRepository;
            navigateAction = async () =>
            {
                //await reserveringRepositoryReference.SetReserveringAsync(reserveringReference);
                navigationServiceReference.Navigate("Reservering", null);
            };

            return navigateAction;
        }

        /*
        private async Task Verwijderen(SpelerViewModel speler)
        {
            if (speler == null) return;

            string errorMessage = string.Empty;
            try
            {
                int index = SpelerViewModels.IndexOf(speler);
                await _reserveringRepository.RemoveSpelerFromReserveringAsync(index,speler.Id);
                SpelerViewModels.RemoveAt(index);//.Remove(speler);

                BevestigenCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("Spelers");
            }
            catch (Exception ex)
            {
                errorMessage = string.Format(CultureInfo.CurrentCulture, _resourceLoader.GetString("GeneralServiceErrorMessage"), Environment.NewLine, ex.Message);
            }

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                await _alertMessageService.ShowAsync(errorMessage, _resourceLoader.GetString("ErrorServiceUnreachable"));
            }
        }
        */

        public async void UpdateReserveringAsync(object notUsed)
        {
            await UpdateReserveringInfoAsync();
        }

        private async Task UpdateReserveringInfoAsync()
        {
            string errorMessage = string.Empty;

            try
            {
                _reservering = await _reserveringRepository.GetReserveringAsync();

                ReserveringType = (_reservering.Id == Guid.Empty) ? "Reservering Maken" : "Reservering Wijzigen";
                /*
                if (_reservering != null)
                {
                    if(_reservering.Spelers != null)


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
                        spelerViewModel.PropertyChanged += spelerViewModel_PropertyChanged;
                        SpelerViewModels.Insert(i, spelerViewModel);
                        //SpelerViewModels[i] = spelerViewModel;
                        BevestigenCommand.RaiseCanExecuteChanged();
                        OnPropertyChanged("Spelers");

                    }

                    if(_reservering.Baan != null)
                    {
                        Baan = new BaanViewModel(_reservering.Baan, _baanRepository, _reserveringRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
                        Baan.PropertyChanged += Baan_PropertyChanged;
                        BevestigenCommand.RaiseCanExecuteChanged();
                        OnPropertyChanged("Baan");

                    }
                }
                */
            }
            catch (Exception ex)
            {
                errorMessage = string.Format(CultureInfo.CurrentCulture, _resourceLoader.GetString("GeneralServiceErrorMessage"), Environment.NewLine, ex.Message);
            }

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                await _alertMessageService.ShowAsync(errorMessage, _resourceLoader.GetString("ErrorServiceUnreachable"));
            }
        }

        public override async void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            if (navigationParameter != null)
            {
                if (navigationParameter.GetType() == typeof(Guid))
                {
                    Guid id = (Guid)navigationParameter;
                    _reservering = await _reserveringRepository.GetReserveringByIdAsync(id);
                }
                
            }
            ReserveringType = (_reservering.Id == Guid.Empty) ? "Reservering Maken" : "Reservering Wijzigen";

            if (navigationParameter != null)
                base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
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

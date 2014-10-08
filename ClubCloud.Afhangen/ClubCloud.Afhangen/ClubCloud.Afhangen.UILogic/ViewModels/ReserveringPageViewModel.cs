using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class ReserveringPageViewModel : ViewModel, IView
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

            BevestigenCommand = new DelegateCommand(ReserveringBevestigen);
            AnnulerenCommand = new DelegateCommand(ReserveringAnnuleren);
            VerwijderenCommand = new DelegateCommand(ReserveringVerwijderen);

            // Subscribe to the ShoppingCartUpdated event
            if (eventAggregator != null)
            {
                eventAggregator.GetEvent<ReserveringUpdatedEvent>().Subscribe(UpdateReserveringAsync);
            }
            UpdateReserveringAsync(null);
        }
        
        private async void ReserveringVerwijderen()
        {
            throw new NotImplementedException();
        }

        private async void ReserveringAnnuleren()
        {
            throw new NotImplementedException();
        }

        private async void ReserveringBevestigen()
        {
            throw new NotImplementedException();
        }

        public DelegateCommand BevestigenCommand { get; set; }

        public DelegateCommand AnnulerenCommand { get; set; }

        public DelegateCommand VerwijderenCommand { get; set; }


        public string ActionName
        {
            get
            {
                if (KanBevestigen())
                {
                    return "Bevestigen";
                }
                /*
                else
                {
                    if (_reservering.Id == Guid.Empty)
                        return "Annuleren";

                    return "Verwijderen";
                }
                */
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
                /*
                else
                {
                    if(_reservering.Id == Guid.Empty)
                        return new DelegateCommand(ReserveringAnnuleren);
                    
                    return new DelegateCommand(ReserveringVerwijderen);
                }
                */
            }
        }

        private async Task Verwijderen()
        {
            await _reserveringRepository.ClearReserveringAsync();
        }

        private bool KanBevestigen()
        {
            if (_reservering == null) return false;

            return (_reservering != null && _reservering.Spelers.Count > 0 && _reservering.Duur > TimeSpan.FromMinutes(0));
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

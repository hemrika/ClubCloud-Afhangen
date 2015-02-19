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
using System.ComponentModel;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class ReserveringUserControlViewModel : ViewModel, IReserveringUserControlViewModel, INotifyPropertyChanged //, IView
    {
        private Reservering _reservering;
        private ObservableCollection<Speler> _spelers;
        private readonly IReserveringRepository _reserveringRepository;
        private readonly IBaanRepository _baanRepository;
        private readonly ISpelerRepository _spelerRepository;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _resourceLoader;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;
        private Baan _baan;
        private TimeSpan _begintijd;
        private TimeSpan _duur;
        private TimeSpan _eindtijd;
        private int _aantalSpelers;
        private string _actionable;

        public ReserveringUserControlViewModel(IReserveringRepository reserveringRepository, IBaanRepository baanRepository, ISpelerRepository spelerRepository, IVerenigingRepository verenigingRepository, INavigationService navigationService,
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

            WijzigSpelersCommand = DelegateCommand.FromAsyncHandler(SpelersWijzigen);
            WijzigBaanCommand = DelegateCommand.FromAsyncHandler(BaanWijzigen);

            if (_eventAggregator != null)
            {
                _eventAggregator.GetEvent<ReserveringUpdatedEvent>().Subscribe(UpdateReserveringAsync);
                _eventAggregator.GetEvent<SpelerUpdatedEvent>().Subscribe(UpdateReserveringAsync);
                _eventAggregator.GetEvent<BaanUpdatedEvent>().Subscribe(UpdateReserveringAsync);
            }

            UpdateReserveringAsync(null);
        }

        public ObservableCollection<Speler> Spelers
        {
            get { return _spelers; }
            private set { SetProperty(ref _spelers, value); }
        }

        public Baan Baan
        {
            get { return _baan; }
            private set { SetProperty(ref _baan, value); }
        }

        public TimeSpan BeginTijd
        {
            get { return _begintijd; }
            private set { SetProperty(ref _begintijd, value); }
        }

        public TimeSpan Duur
        {
            get { return _duur; }
            private set { SetProperty(ref _duur, value); }
        }

        public TimeSpan EindTijd
        {
            get { return _eindtijd; }
            private set { SetProperty(ref _eindtijd, value); }
        }

        public int AantalSpelers
        {
            get { return _aantalSpelers; }
            private set { SetProperty(ref _aantalSpelers, value); }
        }

        public DelegateCommand BevestigenCommand { get; set; }
        
        public DelegateCommand AnnulerenCommand { get; set; }


        public DelegateCommand VerwijderenCommand { get; set; }


        public DelegateCommand WijzigSpelersCommand { get; set; }

        public DelegateCommand WijzigBaanCommand { get; set; }

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

        public string Actionable
        {
            private set
            {
                SetProperty(ref _actionable, value);
            }

            get
            {
                if (KanBevestigen())
                {
                    SetProperty(ref _actionable, "#FF2582D3");
                }
                else
                {
                    SetProperty(ref _actionable, "#808080");
                }

                return _actionable;
            }
        }


        public bool KanBevestigen()
        {
            return ((_spelers != null && _spelers.Count > 0 && Duur > TimeSpan.FromMinutes(0)) && (_baan != null  && _baan.Id != Guid.Empty));
        }

        private async void ReserveringBevestigen()
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

            navigateAction();

        }

        private async void ReserveringAnnuleren()
        {
            await _reserveringRepository.ClearReserveringAsync();

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Main", null);

            var reserveringReference = _reservering;
            var reserveringRepositoryReference = _reserveringRepository;
            navigateAction = async () =>
            {
                //await reserveringRepositoryReference.SetReserveringAsync(reserveringReference);
                navigationServiceReference.Navigate("Main", null);
            };

            navigateAction();

        }

        private async void ReserveringVerwijderen()
        {
            //await _reserveringRepository.ClearReserveringAsync();
            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Main", null);

            var reserveringReference = _reservering;
            var reserveringRepositoryReference = _reserveringRepository;
            navigateAction = async () =>
            {
                //await reserveringRepositoryReference.SetReserveringAsync(reserveringReference);
                navigationServiceReference.Navigate("Main", null);
            };

            navigateAction();
        }

        /*
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
        */

        public async Task SpelersWijzigen()
        {
            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Spelers", null);
            navigationServiceReference.Navigate("Spelers", null);
            navigateAction();
        }

        public async Task BaanWijzigen()
        {
            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Banen", null);
            navigationServiceReference.Navigate("Banen", null);
            navigateAction();
        }

        public async void UpdateReserveringAsync(object notUsed)
        {
            await UpdateReserveringInfoAsync();
        }

        private async Task UpdateReserveringInfoAsync()
        {
            //if (_reservering == null) return;

            string errorMessage = string.Empty;

            try
            {
                _reservering = await _reserveringRepository.GetReserveringAsync();

                if (_reservering != null && _reservering.Spelers != null)
                {
                    Spelers = new ObservableCollection<Speler>();

                    Speler emptySpeler = new Speler { Id = Guid.Empty };
                    for (int i = 0; i < 4; i++)
                    {
                        Speler speler = emptySpeler;
                        try
                        {
                            if(_reservering.Spelers.Count >= i+1)
                                speler = _reservering.Spelers[i];
                        }
                        catch
                        {
                            speler = emptySpeler;
                        }

                        //var spelerViewModel = new SpelerViewModel(i,speler, _spelerRepository, _reserveringRepository,_verenigingRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
                        //spelerViewModel.PropertyChanged += spelerViewModel_PropertyChanged;
                        Spelers.Insert(i, speler);
                        speler.PropertyChanged += speler_PropertyChanged;
                        //SpelerViewModels[i] = spelerViewModel;
                        //BevestigenCommand.RaiseCanExecuteChanged();
                        //OnPropertyChanged("Spelers");

                    }
                    /*
                    SpelerViewModels = new ObservableCollection<SpelerViewModel>();
                    foreach (var item in _reservering.Spelers)
                    {
                        var spelerViewModel = new SpelerViewModel(item, _spelerRepository, _reserveringRepository,_navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
                        spelerViewModel.PropertyChanged += spelerViewModel_PropertyChanged;
                        SpelerViewModels.Add(spelerViewModel);
                        BevestigenCommand.RaiseCanExecuteChanged();
                        OnPropertyChanged("Spelers");

                    }
                    */
                    if (_reservering.Baan != null)
                    {
                        Baan = _reservering.Baan;
                        Baan.PropertyChanged += Baan_PropertyChanged;
                    }

                    //if (_reservering.Baan != null)
                    //{
                    //    BaanViewModel = new BaanViewModel(_reservering.Baan, _baanRepository, _reserveringRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
                    //}
                    //BaanViewModel.PropertyChanged += baanViewModel_PropertyChanged;
                    //BevestigenCommand.RaiseCanExecuteChanged();
                    //OnPropertyChanged("Baan");

                    BeginTijd = _reservering.BeginTijd;
                    Duur = _reservering.Duur;
                    EindTijd = _reservering.EindTijd;

                    AantalSpelers = Spelers.Where(s => s.Id != Guid.Empty).Count();

                }
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

        void speler_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Spelers")
            {

            }
        }

        void Baan_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Baan")
            {
                //OnPropertyChanged("Baan");
            }
        }

        /*
        private void baanViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Baan")
            {
                //OnPropertyChanged("Baan");
            }
        }

        private void spelerViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Spelers")
            {
                //OnPropertyChanged("FullPrice");
                //OnPropertyChanged("TotalDiscount");
                //OnPropertyChanged("TotalPrice");
            }
        }
        */

        public bool ValidateForm()
        {
            throw new NotImplementedException();
        }

        public bool IsBaanSelected()
        {
            throw new NotImplementedException();
        }

        public bool IsSpelerSelected()
        {
            throw new NotImplementedException();
        }

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

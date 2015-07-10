using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.Interfaces;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.PubSubEvents;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    [DataContract]
    public class ReserveringViewModel : ViewModel, IView
    {
        private ObservableCollection<Speler> _spelers;
        private readonly IReserveringRepository _reserveringRepository;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _resourceLoader;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBaanRepository _baanRepository;
        private readonly ISpelerRepository _spelerRepository;
        private Reservering _reservering;
        private BaanUserControlViewModel _baanViewModel;

        public ReserveringViewModel(Reservering reservering, IBaanRepository baanRepository, ISpelerRepository spelerRepository, IReserveringRepository reserveringRepository, IVerenigingRepository verenigingRepository, INavigationService navigationService,
                                         IResourceLoader resourceLoader, IAlertMessageService alertMessageService,
                                         IEventAggregator eventAggregator)
        {
            _baanRepository = baanRepository;
            _spelerRepository = spelerRepository;
            _reserveringRepository = reserveringRepository;
            _verenigingRepository = verenigingRepository;
            _navigationService = navigationService;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;


            if (reservering == null)
            {
                throw new ArgumentNullException("Reservering", "reservering cannot be null");
            }

            _reservering = reservering;

            Spelers = new ObservableCollection<Speler>();

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

                //var spelerViewModel = new SpelerUserControlViewModel(i,speler, _spelerRepository, _reserveringRepository, _verenigingRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
                //SpelerViewModels.Insert(i, spelerViewModel);
                //SpelerViewModels[i] = spelerViewModel;
                //OnPropertyChanged("Spelers");
                Spelers.Insert(i, speler);

            }
            /*
            SpelerViewModels = new ObservableCollection<SpelerViewModel>();
            
            foreach (Speler speler in reservering.Spelers)
            {
                SpelerViewModels.Add(new SpelerViewModel(speler, _spelerRepository, _reserveringRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator));
            }
            */
            BaanViewModel = new BaanUserControlViewModel(reservering.Baan, _baanRepository, _reserveringRepository, _verenigingRepository, _navigationService, _resourceLoader, _alertMessageService, _eventAggregator);
        }


        public Guid Id { get { return _reservering.Id; } }

        public ObservableCollection<Speler> Spelers
        {
            get { return _spelers; }
            private set { SetProperty(ref _spelers, value); }
        }

        public BaanUserControlViewModel BaanViewModel
        {
            get { return _baanViewModel; }
            private set { SetProperty(ref _baanViewModel, value); }
        }

        public Guid? BaanId { get { return _reservering.BaanId.Value; } }

        public DateTime Datum { get { return _reservering.Datum; } }

        public TimeSpan BeginTijd { get { return _reservering.BeginTijd; } }

        public TimeSpan Duur { get { return _reservering.Duur; } }

        public TimeSpan EindTijd { get { return (_reservering.BeginTijd.Add(Duur)); } }

        public bool Final { get { return _reservering.Final; } }

        //TODO
        internal static bool ValidateForm()
        {
            return true;
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


        internal static bool IsSpelerSelected()
        {
            return true;
        }

        internal static bool IsBaanSelected()
        {
            return true;
        }
    }
}

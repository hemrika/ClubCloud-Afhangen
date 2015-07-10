using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism.Commands;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.Interfaces;
using ClubCloud.Core.Prism.PubSubEvents;
using ClubCloud.Core.Prism.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class SponsorsPageViewModel : ViewModel //, IView
    {
        private ISponsorRepository _sponsorRepository;
        private IVerenigingRepository _verenigingRepository;
        private INavigationService _navigationService;
        private IResourceLoader _resourceLoader;
        private IAlertMessageService _alertMessageService;
        private IEventAggregator _eventAggregator;
        private Vereniging _vereniging;
        private ObservableCollection<Sponsor> _sponsors;
        private DispatcherTimer indexTimer;

        public SponsorsPageViewModel(ISponsorRepository sponsorRepository, IVerenigingRepository verenigingRepository, INavigationService navigationService,
                                         IResourceLoader resourceLoader, IAlertMessageService alertMessageService, IEventAggregator eventAggregator)
        {
            _sponsorRepository = sponsorRepository;
            _verenigingRepository = verenigingRepository;
            _navigationService = navigationService;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            GoBackCommand = new DelegateCommand(_navigationService.GoBack);


            indexTimer = new DispatcherTimer();
            indexTimer.Interval = TimeSpan.FromSeconds(30);
            indexTimer.Tick += indexTimer_Tick;
            indexTimer.Start();

            if (_eventAggregator != null)
            {
                _eventAggregator.GetEvent<SponsorUpdatedEvent>().Subscribe(UpdateSponsorsAsync);
            }

            //WijzigBaanCommand = DelegateCommand.FromAsyncHandler(BaanWijzigen);

            UpdateSponsorsAsync(null);

        }

        private int change = 1;
        void indexTimer_Tick(object sender, object e)
        {
            if (Sponsors.Count == 1)
            {
                Index = 0;
            }
            else
            {
                // If we'd go out of bounds then reverse
                int newIndex = _index + change;
                if (newIndex >= Sponsors.Count || newIndex < 0)
                {
                    change *= -1;
                }

                Index += change;
            }
            
            _eventAggregator.GetEvent<ActivityEvent>().Publish(DateTime.Now.TimeOfDay);
        }

        public DelegateCommand GoBackCommand { get; set; }

        public ObservableCollection<Sponsor> Sponsors
        {
            get
            {
                if (_sponsors == null)
                    UpdateSponsorsAsync(null);

                return _sponsors;
            }
            private set { SetProperty(ref _sponsors, value); }
        }

        private int _index = 0;
        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }

        public async void UpdateSponsorsAsync(object notUsed)
        {
            await UpdateSponsorsInfoAsync();
        }

        private static Uri _baseUri = new Uri("ms-appdata:///temp/Sponsors/"); 

        private async Task UpdateSponsorsInfoAsync()
        {
            _vereniging = await _verenigingRepository.GetVerenigingAsync();
            Sponsors = await _sponsorRepository.GetSponsorsAsync(_vereniging.Id);

            if (Sponsors == null || Sponsors.Count == 0)
            {
                if(Sponsors == null)
                    Sponsors = new ObservableCollection<Sponsor>();
                
                Sponsors.Add(new Sponsor{ Id = Guid.NewGuid(), Naam = "Er zijn momenteel geen sponsoren opgegeven.", Type = "item", Path = new Uri("ms-appx:///Assets/placeHolderSponsor.png")});
            }
            //Index = 0;
        }

        public override async void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            _navigationService.Navigate("Main", null);
            base.OnNavigatedFrom(viewModelState, suspending);
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

            if (navigationParameter != null)
                base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }

        /*
        private async void GoBackCommand()
        {
            _navigationService.Navigate("Main", null);

            string errorMessage = string.Empty;
        }
        */

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

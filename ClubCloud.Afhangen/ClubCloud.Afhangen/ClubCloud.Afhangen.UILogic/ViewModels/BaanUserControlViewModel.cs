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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class BaanUserControlViewModel : ViewModel, IBaanUserControlViewModel, IView, INotifyPropertyChanged
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly INavigationService _navigationService;
        private readonly IReserveringRepository _reserveringRepository;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IBaanRepository _baanRepository;

        private Baan _baan;
        private TimeSpan _duur;
        private TimeSpan _beginTijd;

        private ObservableCollection<Reservering> _reserveringen;
        private Reservering _reserveringCurrent;

        public BaanUserControlViewModel(Baan baan, IBaanRepository baanRepository, IReserveringRepository reserveringRepository, IVerenigingRepository verenigingRepository, INavigationService navigationService,
                                         IResourceLoader resourceLoader, IAlertMessageService alertMessageService, IEventAggregator eventAggregator)
        {
            if (baan == null)
            {
                throw new ArgumentNullException("Baan", "baan cannot be null");
            }

            _baan = baan;
            _baanRepository = baanRepository;
            _reserveringRepository = reserveringRepository;
            _verenigingRepository = verenigingRepository;
            _navigationService = navigationService;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;


            BaanNavigationCommand = new DelegateCommand(NavigateToBanen);
            SelecterenBaanCommand = new DelegateCommand(SelecteerBaan);
            VerwijderenBaanCommand = new DelegateCommand(VerwijderBaan);

            UpdateBaanAsync(baan);
        }

        public Guid Id
        {
            get
            {
                return _baan.Id;
            }
        }

        public string Naam
        {
            get
            {
                return _baan.Naam;
            }
        }

        public int Nummer
        {
            get
            {
                return _baan.Nummer;
            }
        }

        public string Baansoort
        {
            get
            {
                return _baan.Baansoort.ToString();
            }
        }

        //TODO Calculate first available timeslot
        public TimeSpan BeginTijd
        {
            get { return _beginTijd; }
            private set { SetProperty(ref _beginTijd, value); }
        }

        //TODO Calculate available playtime
        public TimeSpan Duur
        {
            get { return _duur; }
            private set { SetProperty(ref _duur, value); }
        }

        public string ActionName
        {
            get
            {
                //Task.Run(async () => await UpdateBaanInfoAsync(_baan));
                if (_reserveringCurrent == null || _baan.Id != _reserveringCurrent.BaanId)
                {
                    return "Selecteer Baan";
                }
                else
                {
                    return "Verwijder Baan";
                }
            }
        }

        public DelegateCommand Action
        {
            get
            {
                //Task.Run(async () => await UpdateBaanInfoAsync(_baan));
                if (_reserveringCurrent == null || _baan.Id != _reserveringCurrent.BaanId)
                {
                    return new DelegateCommand(SelecteerBaan);// new System.Action(SelecteerSpeler);
                }
                else
                {
                    return new DelegateCommand(VerwijderBaan);// new System.Action(VerwijderSpeler);
                }
            }
        }


        public DelegateCommand BaanNavigationCommand { get; set; }
        public DelegateCommand SelecterenBaanCommand { get; set; }

        public DelegateCommand VerwijderenBaanCommand { get; set; }


        public async void UpdateBaanAsync(Baan baan)
        {
            await UpdateBaanInfoAsync(baan);
        }

        private async Task UpdateBaanInfoAsync(Baan baan)
        {
            try
            {

                if (baan != null)
                {
                    _baan = baan;

                    //_reserveringen = new ObservableCollection<Reservering>();

                    //_reserveringen = await _reserveringRepository.GetReserveringenByBaanAsync(_baan.Id);

                    //_reserveringen.Where(r => r.BeginTijd)
                }
                /*
                else
                {
                    Reservering reservering = null;
                    if (Id != Guid.Empty)
                    {
                        _reservering = await _reserveringRepository.GetReserveringAsync();
                    }
                }
                */
                Random rnd = new Random();
                BeginTijd = DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(rnd.Next(0, 60)));
                Duur = TimeSpan.FromMinutes(rnd.Next(30, 60));
                //await Task.Delay(1000);

            }
            catch
            {
                _baan = new Baan { Id = Guid.Empty, Naam = "", Baansoort = "", Nummer = 0 };

            }

            try
            {
                _reserveringCurrent = await _reserveringRepository.GetReserveringAsync();
            }
            catch { }
        }

        private async void NavigateToBanen()
        {
            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Baan", null);
            _navigationService.Navigate("Baan", null);
            navigateAction();
        }

        private async void SelecteerBaan()
        {
            Reservering reservering = await _reserveringRepository.AddBaanToReserveringAsync(Id, BeginTijd, Duur);

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Banen", null);
            navigationServiceReference.Navigate("Banen", null);
            navigateAction();

            /*
            //navigateAction = () => navigationServiceReference.Navigate("Banen", null);
            //navigationServiceReference.Navigate("Banen", null);
            //navigateAction();

            //Reservering reservering = await _reserveringRepository.GetReserveringAsync();

            if (reservering.Spelers != null && reservering.Spelers.Count > 0)
            {
                navigateAction = () => navigationServiceReference.Navigate("Reservering", null);
                navigationServiceReference.Navigate("Reservering", null);
                navigateAction();

                //_navigationService.Navigate("Reservering", reservering);
            }
            else
            {
                navigateAction = () => navigationServiceReference.Navigate("Spelers", null);
                navigationServiceReference.Navigate("Spelers", null);
                navigateAction();

                //_navigationService.Navigate("Spelers", reservering);
            }
            */
        }

        private async void VerwijderBaan()
        {
            Reservering reservering = await _reserveringRepository.RemoveBaanFromReserveringAsync(Id, BeginTijd, Duur);

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Banen", null);
            navigationServiceReference.Navigate("Banen", null);
            navigateAction();
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


        public bool IsBaanSelected()
        {
            throw new NotImplementedException();
        }

        public void UpdateBaanAsync(object notUsed)
        {
            throw new NotImplementedException();
        }
    }
}

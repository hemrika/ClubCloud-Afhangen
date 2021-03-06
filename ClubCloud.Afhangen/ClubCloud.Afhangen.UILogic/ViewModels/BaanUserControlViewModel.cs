﻿using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism.Commands;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.Interfaces;
using ClubCloud.Core.Prism.PubSubEvents;
using ClubCloud.Core.Prism.Interfaces;
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
                return "Baan "+_baan.Nummer; 
                //return _baan.Naam;
            }
        }

        public int Nummer
        {
            get
            {
                return _baan.Nummer;
            }
        }

        public string Locatie
        {
            get
            {
                return _baan.Locatie;
            }
        }

        public string Soort
        {
            get
            {
                return _baan.Soort;
            }
        }

        public string Type
        {
            get
            {
                return _baan.Type;
            }
        }

        public bool Verlichting
        {
            get
            {
                return _baan.Verlichting;
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

        public bool Selectable
        {
            get
            {
                //Task.Run(async () => await UpdateBaanInfoAsync(_baan));
                if (_reserveringCurrent == null || _baan.Id != _reserveringCurrent.BaanId)
                {
                    return true;
                }
                else
                {
                    return false;
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

                    _reserveringen = new ObservableCollection<Reservering>();
                    Afhang afhang = await _verenigingRepository.GetVerenigingSettingsAsync();
                    _reserveringen = await _reserveringRepository.GetReserveringenByBaanAsync(_baan.Id);

                    BeginTijd = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, afhang.Duur_Precisie, 0));

                    foreach (Reservering reservering in _reserveringen)
                    {
                        if (reservering.BeginTijd < BeginTijd && BeginTijd < reservering.EindTijd)
                            BeginTijd = reservering.EindTijd.Add(new TimeSpan(0, afhang.Duur_Precisie, 0));
                    }

                    Duur = TimeSpan.FromMinutes(afhang.Duur_Vier);
                    foreach (Reservering reservering in _reserveringen)
                    {
                        if (reservering.BeginTijd > BeginTijd)
                        {
                            Duur = reservering.BeginTijd - BeginTijd;
                            break;
                        }
                    }
                }
            }
            catch
            {
                _baan = new Baan { Id = Guid.Empty, Naam = "", Nummer = 0 };//, Baansoort = "", Nummer = 0 };

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
            _eventAggregator.GetEvent<ActivityEvent>().Publish(DateTime.Now.TimeOfDay);
            navigateAction = () => navigationServiceReference.Navigate("Baan", null);
            _navigationService.Navigate("Baan", null);
            navigateAction();
        }

        private async void SelecteerBaan()
        {
            Reservering reservering = await _reserveringRepository.AddBaanToReserveringAsync(Id, BeginTijd, Duur);

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;
            _eventAggregator.GetEvent<ActivityEvent>().Publish(DateTime.Now.TimeOfDay);
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
            _eventAggregator.GetEvent<ActivityEvent>().Publish(DateTime.Now.TimeOfDay);
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
    }
}

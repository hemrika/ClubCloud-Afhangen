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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class CardPageViewModel : ViewModel
    {
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly ISpelerRepository _spelerRepository;
        private readonly IReserveringRepository _reserveringRepository;
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _resourceLoader;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;
        private int _index = -1;
        private Speler _speler;
        private ObservableCollection<Reservering> _reserveringen;
        private string _cardinput;
        private string _cardoutput;
        private string _cardreceived;
        private string _message;

        public CardPageViewModel(IVerenigingRepository verenigingRepository, ISpelerRepository spelerRepository, IReserveringRepository reserveringRepository, INavigationService navigationService,
            IResourceLoader resourceLoader, IAlertMessageService alertMessageService, IEventAggregator eventAggregator)
        {
            _verenigingRepository = verenigingRepository;
            _spelerRepository = spelerRepository;
            _reserveringRepository = reserveringRepository;
            _navigationService = navigationService;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            if (_eventAggregator != null)
            {
                _eventAggregator.GetEvent<CardUpdatedEvent>().Subscribe(UpdateCardAsync);
            }

            KeyUpCommand = new DelegateCommand<KeyRoutedEventArgs>(KeyUp);
            GoBackCommand = new DelegateCommand(_navigationService.GoBack);
            //GoNextCommand = new DelegateCommand<string>(GoNext);
            GoNextCommand = new DelegateCommand(GoNext);

            UpdateCardAsync(null);
        }

        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }

        public Speler Speler
        {
            get { return _speler; }
            set { SetProperty(ref _speler, value); }
        }

        public ObservableCollection<Reservering> Reserveringen
        {
            get { return _reserveringen; }
            set { SetProperty(ref _reserveringen, value); }
        }

        public string CardInput
        {
            get { return _cardinput; }
            set { SetProperty(ref _cardinput, value); }
        }

        public string CardOutput
        {
            get { return _cardoutput; }
            set { SetProperty(ref _cardoutput, value); }
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public bool Huidig { 
            get
            {
                return (Index >= 0 && Reserveringen.Count > 0);
            }
        }
        public bool Bestaand
        {
            get
            {
                return (Index == -1 && Reserveringen.Count > 0);
            }
        }

        private async void UpdateCardAsync(Speler speler)
        {
            await UpdateCardInfoAsync(speler);
        }

        private async Task UpdateCardInfoAsync(Speler speler)
        {
            Reserveringen = new ObservableCollection<Reservering>();
            await _reserveringRepository.GetReserveringenAsync();
            Message = "Haal uw kaart door de lezer.";
        }

        public DelegateCommand GoBackCommand { get; set; }

        //public DelegateCommand<string> GoNextCommand { get; set; }
        public DelegateCommand GoNextCommand { get; set; }

        private async void GoNext()//(string reserveringId)
        {
            //if (reserveringId.HasValue && reserveringId.Value != Guid.Empty)
            //{
            /*
            Guid id = Guid.Parse(reserveringId);
                await _reserveringRepository.GetReserveringenByIdAsync(id);//.Value);

                Action navigateAction = null;
                var navigationServiceReference = _navigationService;

                navigateAction = () => navigationServiceReference.Navigate("Reservering", null);
                navigationServiceReference.Navigate("Reservering", null);
                navigateAction();
            //}
            */

            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Reservering", null);
            navigationServiceReference.Navigate("Reservering", null);
            navigateAction();

        }

        public DelegateCommand<KeyRoutedEventArgs> KeyUpCommand { get; set; }

        private async void KeyUp(KeyRoutedEventArgs input)
        {
            if (input.OriginalSource.GetType() == typeof(TextBox))
            {
                TextBox tbx_input = input.OriginalSource as TextBox;
                _cardreceived = tbx_input.Text;
                await InactivityTimer();
                await ParseCard(_cardreceived);
            }
        }

        private DispatcherTimer inactivityTimer;

        private async Task InactivityTimer()
        {
            if (inactivityTimer != null) return;

            inactivityTimer = new DispatcherTimer();

            inactivityTimer.Interval = TimeSpan.FromSeconds(2);
            inactivityTimer.Tick += inactivityTimer_Tick;
            inactivityTimer.Start();

        }

        async void inactivityTimer_Tick(object sender, object e)
        {
            if (inactivityTimer != null) inactivityTimer.Stop();
            inactivityTimer = null;

            if (!string.IsNullOrWhiteSpace(_cardreceived))
            {
                await ParseCard(_cardreceived);

                if (string.IsNullOrWhiteSpace(_verenigingsnummer) || string.IsNullOrWhiteSpace(_bondsnummer))
                {
                    CardInput = string.Empty;
                    CardOutput = "Het lezen is mislukt, haal nogmaals de kaart door de lezer.";
                }
            }
        }

        private string _verenigingsnummer = string.Empty;
        private string _bondsnummer = string.Empty;
        private int _jaar = DateTime.Today.AddYears(-1).Year;

        private string knltbRegex = @"(%)([\d\s]{35})([\d]{8})([a-zA-Z]{1})([-/\d]{8})([\d\s]{2})([\d\s]{2})([?/;/]{2})([\d]{8})([\d]{5})([\d]{4})([?/]{1})$";
        private static bool looking = false;

        private async Task ParseCard(string input)
        {
            Regex cardTest = new Regex(knltbRegex);
            GroupCollection collection = cardTest.Match(input).Groups;

            if (collection.Count == 13 & !looking)
            {
                looking = true;
                //if (DateTime.Today.Year >= int.Parse(collection[11].Value))
                //{
                    _jaar = int.Parse(collection[11].Value);
                    _verenigingsnummer = collection[10].Value;
                    _bondsnummer = collection[9].Value;
                //}
                /*
               if (Index == 0)
                   _bondsnummer = "12073385";
               if (Index == 1)
                   _bondsnummer = "19949820";
               if (Index == 2)
                   _bondsnummer = "14788632";
               if (Index == 3)
                   _bondsnummer = "28403029";
               */
            }
                await RetrieveSpeler();
                looking = false;
        }

        private async Task RetrieveSpeler()
        {
            if (!string.IsNullOrWhiteSpace(_verenigingsnummer) && !string.IsNullOrWhiteSpace(_bondsnummer))
            {
                Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                Afhang afhang = await _verenigingRepository.GetVerenigingSettingsAsync();
                int uitgavemaand = 4;

                if((DateTime.Today.Year - _jaar) >= 1 && DateTime.Today.Month > uitgavemaand )
                {
                    _verenigingsnummer = string.Empty;
                    _bondsnummer = string.Empty;
                    _jaar = DateTime.Today.AddYears(-1).Year;
                    CardInput = string.Empty;
                    if (inactivityTimer != null) inactivityTimer.Stop();
                    inactivityTimer = null;

                    CardOutput = "Deze pas is verlopen. Gebruik uw nieuwe pas";
                    return;
                }

                if (vereniging != null && vereniging.Nummer == _verenigingsnummer)
                {
                    if (!string.IsNullOrWhiteSpace(_bondsnummer))
                    {
                        _speler = await _spelerRepository.GetSpelerByNummerAsync(vereniging.Id, _bondsnummer);
                        bool already = false;
                        if (_speler != null)
                        {
                            Reservering reservering = await _reserveringRepository.GetReserveringAsync();
                            already = reservering.Spelers.Count(s => s.Id == _speler.Id) > 0;
                            _reserveringen = await _reserveringRepository.GetReserveringenBySpelerAsync(_speler.Id);
                        }
                        
                        #region Nieuwe of bestaande reservering wijzigen

                        if (Index != -1)
                        {
                            //Speler zit al in de huidige reservering
                            if (already)
                            {
                                _verenigingsnummer = string.Empty;
                                _bondsnummer = string.Empty;
                                _jaar = DateTime.Today.AddYears(-1).Year;
                                CardInput = string.Empty;
                                if (inactivityTimer != null) inactivityTimer.Stop();
                                inactivityTimer = null;

                                CardOutput = "Deze speler is al geselecteerd.";
                                CardInput = string.Empty;
                                return;
                            }

                            //Speler heeft al andere reserveringen voor vandaag
                            if (_reserveringen.Count > 0)
                            {
                                _verenigingsnummer = string.Empty;
                                _bondsnummer = string.Empty;
                                _jaar = DateTime.Today.AddYears(-1).Year;
                                CardInput = string.Empty;
                                if (inactivityTimer != null) inactivityTimer.Stop();
                                inactivityTimer = null;

                                CardOutput = "Deze speler heeft al reservering(en) voor vandaag.";
                                CardInput = string.Empty;
                                return;
                            }

                            //Niet reeds toegevoegd en geen reservingen
                            if (Index >= 0)
                            {
                                await _reserveringRepository.AddSpelerToReserveringAsync(Index, _speler.Id);

                                _verenigingsnummer = string.Empty;
                                _bondsnummer = string.Empty;
                                _jaar = DateTime.Today.AddYears(-1).Year;
                                CardInput = string.Empty;
                                if (inactivityTimer != null) inactivityTimer.Stop();
                                inactivityTimer = null;

                                Action navigateAction = null;
                                var navigationServiceReference = _navigationService;

                                navigateAction = () => navigationServiceReference.Navigate("Spelers", null);
                                navigationServiceReference.Navigate("Spelers", null);
                                navigateAction();
                            }

                            _verenigingsnummer = string.Empty;
                            _bondsnummer = string.Empty;
                            _jaar = DateTime.Today.AddYears(-1).Year;
                            CardInput = string.Empty;
                            if (inactivityTimer != null) inactivityTimer.Stop();
                            inactivityTimer = null;

                            CardOutput = "Er zijn geen gegevens gevonden.";
                            return;

                        }

                        #endregion

                        #region Reservering raadplegen

                        if (Index == -1)
                        {
                            if (_reserveringen.Count > 0)
                            {
                                _verenigingsnummer = string.Empty;
                                _bondsnummer = string.Empty;
                                _jaar = DateTime.Today.AddYears(-1).Year;
                                CardInput = string.Empty;
                                if (inactivityTimer != null) inactivityTimer.Stop();
                                inactivityTimer = null;

                                CardOutput = "Er zijn " + _reserveringen.Count + " reserveringen gevonden.";
                                if(_reserveringen.Count == 1)
                                    CardOutput = "Er is " + _reserveringen.Count + " reservering gevonden.";
                                return;
                            }
                            else
                            {
                                _verenigingsnummer = string.Empty;
                                _bondsnummer = string.Empty;
                                _jaar = DateTime.Today.AddYears(-1).Year;
                                CardInput = string.Empty;
                                if (inactivityTimer != null) inactivityTimer.Stop();
                                inactivityTimer = null;

                                CardOutput = "Er zijn geen reserveringen gevonden.";
                                return;
                            }
                        }

                        #endregion

                        _verenigingsnummer = string.Empty;
                        _bondsnummer = string.Empty;
                        _jaar = DateTime.Today.AddYears(-1).Year;
                        CardInput = string.Empty;
                        if (inactivityTimer != null) inactivityTimer.Stop();
                        inactivityTimer = null;

                        CardOutput = string.Empty;
                        return;

                    }
                }
                else
                {
                    _verenigingsnummer = string.Empty;
                    _bondsnummer = string.Empty;
                    _jaar = DateTime.Today.AddYears(-1).Year;
                    CardInput = string.Empty;
                    if (inactivityTimer != null) inactivityTimer.Stop();
                    inactivityTimer = null;

                    CardOutput = "Deze speler is geen lid van de vereniging";
                    return;
                }

                /*
                 _verenigingsnummer = string.Empty;
                _bondsnummer = string.Empty;
                _jaar = DateTime.Today.AddYears(-1).Year;
                CardInput = string.Empty;
                if (inactivityTimer != null) inactivityTimer.Stop();
                inactivityTimer = null;

                CardOutput = string.Empty;
                return;
                */
            }
        }

        public override async void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            if(navigationParameter.GetType() == typeof(int))
                Index = (int)navigationParameter;

            if (navigationParameter.GetType() == typeof(string))
                int.TryParse(navigationParameter.ToString(), out _index);


            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }
    }
}

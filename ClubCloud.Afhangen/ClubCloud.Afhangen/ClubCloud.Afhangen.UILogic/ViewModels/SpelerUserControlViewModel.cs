using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;


namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class SpelerUserControlViewModel : UserControl, ISpelerUserControlViewModel, IView//, INotifyPropertyChanged
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly INavigationService _navigationService;
        private readonly IReserveringRepository _reserveringRepository;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly IAlertMessageService _alertMessageService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISpelerRepository _spelerRepository;
        private Speler _speler;
        private StorageFile _storageFile;
        private Foto _foto;
        private int _index;

        public SpelerUserControlViewModel(int index, Speler speler, ISpelerRepository spelerRepository, IReserveringRepository reserveringRepository, IVerenigingRepository verenigingRepository, INavigationService navigationService,
                                         IResourceLoader resourceLoader, IAlertMessageService alertMessageService, IEventAggregator eventAggregator)
        {
            _index = index;
            _speler = speler;
            _spelerRepository = spelerRepository;
            _reserveringRepository = reserveringRepository;
            _verenigingRepository = verenigingRepository;
            _navigationService = navigationService;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            if (_eventAggregator != null)
            {
                _eventAggregator.GetEvent<SpelerUpdatedEvent>().Subscribe(UpdateSpelerAsync);
            }


            SpelerNavigationCommand = new DelegateCommand(NavigateToSpelers);
            SelecterenSpelerCommand = new DelegateCommand(SelecteerSpeler);
            VerwijderenSpelerCommand = new DelegateCommand(VerwijderSpeler);


            UpdateSpelerAsync(speler);
        }

        public Guid Id { get { return _speler.Id; } private set { _speler.Id = value; } }

        public int Index { get { return _index; } }
        public string Naam { get { return _speler.Roepnaam +" " + _speler.Tussenvoegsel +" " + _speler.Achternaam; } }

        public string Nummer { get { return _speler.Bondsnummer; } }

        public byte[] Foto
        {
            get
            {
                Task.Run(async () => await UpdateSpelerImageAsync(_speler));

                while (_foto.ContentData == null) { }

                return _foto.ContentData;
            }
        }

        public string ActionName {
            get
            {
                if(_speler.Id == Guid.Empty)
                {
                    return "Selecteer Speler";
                }
                else
                {
                    return "Verwijder Speler";
                }
            }
        }

        public DelegateCommand Action
        {
            get
            {
                if (_speler.Id == Guid.Empty)
                {
                    return new DelegateCommand(SelecteerSpeler);
                }
                else
                {
                    return new  DelegateCommand(VerwijderSpeler);
                }
            }
        }

        public DelegateCommand SpelerNavigationCommand { get; set; }

        public DelegateCommand SelecterenSpelerCommand { get; set; }

        public DelegateCommand VerwijderenSpelerCommand { get; set; }

        private void NavigateToSpelers()
        {
            _navigationService.Navigate("Speler", null);
        }

        public async void UpdateSpelerAsync(Speler speler)
        {
            await UpdateSpelerInfoAsync(speler);
            await UpdateSpelerImageAsync(speler);
        }

        private async Task UpdateSpelerInfoAsync(Speler speler)
        {
            try
            {

                if (speler != null)
                {
                    _speler = speler;
                }
                else
                {
                    Reservering reservering = null;
                    if (Id != Guid.Empty)
                    {
                        reservering = await _reserveringRepository.GetReserveringAsync();
                        _speler = reservering.Spelers[Index];

                    }
                }
            }
            catch
            {
                _speler = new Speler { Id = Guid.Empty, Bondsnummer = "00000000", Roepnaam ="Tennis", Achternaam ="Speler" };
            }
        }

        private async Task UpdateSpelerImageAsync(Speler speler)
        {
            if (_foto == null)
                _foto = new Models.Foto();

            _foto.ContentData = null;

            if (speler == null)
            {
                _storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/placeHolderSpeler.png"));

                try
                {
                    IBuffer readbuffer = await FileIO.ReadBufferAsync(_storageFile);
                    _foto.ContentData = readbuffer.ToArray();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
                return;
            }

            if (speler.Id != Guid.Empty && _foto.ContentData == null)
            {
                try
                {

                    Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                    _foto = await _spelerRepository.GetFotoAsync(vereniging.Id, Id);
                }
                catch(Exception ex)
                {
                    string message = ex.Message;
                }
                return;
            }
            else
            {
                _storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/placeHolderSpeler.png"));

                try
                {
                    IBuffer readbuffer = await FileIO.ReadBufferAsync(_storageFile);

                    _foto.ContentData = readbuffer.ToArray();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
        }

        private async void SelecteerSpeler()
        {
            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Card", Index);
            navigationServiceReference.Navigate("Card", Index);
            navigateAction();

            //FlyoutBase.ShowAttachedFlyout((FrameworkElement)this);
            /*
            Reservering reservering = null;
            if(Id == Guid.Empty)
            {
                Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                Speler speler = null;

                //CardUserControlViewModel carduser = new CardUserControlViewModel(_spelerRepository,_navigationService,_resourceLoader,_alertMessageService,_eventAggregator);

                
                //dialog.
                if (Index == 0)
                    speler = await ShowAsync(Index);//(_spelerRepository,_navigationService,_resourceLoader,_alertMessageService,_eventAggregator);
                    //speler = await _spelerRepository.GetSpelerByNummerAsync(vereniging.Id, "12073385");
                if (Index == 1)
                    speler = await ShowAsync(Index);
                    //speler = await _spelerRepository.GetSpelerByNummerAsync(vereniging.Id, "19949820");
                if (Index == 2)
                    speler = await ShowAsync(Index);
                    //speler = await _spelerRepository.GetSpelerByNummerAsync(vereniging.Id, "14788632");
                if (Index == 3)
                    speler = await ShowAsync(Index);
                    //speler = await _spelerRepository.GetSpelerByNummerAsync(vereniging.Id, "28403029");

                //Id = new Guid("6F0DF085-8B6C-414C-9A2E-27DC351B0C39");
                
                if(speler != null)
                    reservering = await _reserveringRepository.AddSpelerToReserveringAsync(Index, speler.Id);

                //if (reservering != null) { int minutes = reservering.Duur.Minutes; }
            
            }
            */
            /*
            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            navigateAction = () => navigationServiceReference.Navigate("Spelers", null);
            navigationServiceReference.Navigate("Spelers",null);
            navigateAction();
            */
            /*
            //navigateAction = () => navigationServiceReference.Navigate("Banen", null);
            //navigationServiceReference.Navigate("Banen", null);
            //navigateAction();

            //Reservering reservering = await _reserveringRepository.GetReserveringAsync();

            if (reservering.Baan != null && reservering.BaanId.HasValue)
            {
                navigateAction = () => navigationServiceReference.Navigate("Reservering", reservering);
                navigationServiceReference.Navigate("Reservering", reservering);
                navigateAction();

                //_navigationService.Navigate("Reservering", reservering);
            }
            else
            {
                navigateAction = () => navigationServiceReference.Navigate("Banen", reservering);
                navigationServiceReference.Navigate("Banen", reservering);
                navigateAction();

                //_navigationService.Navigate("Spelers", reservering);
            }
            */
        }

        private async void VerwijderSpeler()
        {
            Reservering reservering = null;
            if (Id != Guid.Empty)
            {
                _speler = new Speler { Id = Guid.Empty };
                reservering = await _reserveringRepository.RemoveSpelerFromReserveringAsync(_index, Id);
            }

            /*
            Action navigateAction = null;
            var navigationServiceReference = _navigationService;

            //navigateAction = () => navigationServiceReference.Navigate("Banen", null);
            //navigationServiceReference.Navigate("Banen", null);
            //navigateAction();

            //Reservering reservering = await _reserveringRepository.GetReserveringAsync();

            if (reservering.Baan != null && reservering.BaanId.HasValue)
            {
                navigateAction = () => navigationServiceReference.Navigate("Reservering", reservering);
                navigationServiceReference.Navigate("Reservering", reservering);
                navigateAction();

                //_navigationService.Navigate("Reservering", reservering);
            }
            else
            {
                navigateAction = () => navigationServiceReference.Navigate("Banen", reservering);
                navigationServiceReference.Navigate("Banen", reservering);
                navigateAction();

                //_navigationService.Navigate("Spelers", reservering);
            }
            */
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

        public bool IsSpelersSelected()
        {
            throw new NotImplementedException();
        }
    }
}


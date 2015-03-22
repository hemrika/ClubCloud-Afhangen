using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using System.Linq;
using System;
using Syncfusion.UI.Xaml.Schedule;
using ClubCloud.Afhangen.UILogic.ClubCloudAfhangen;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public class BaanschemaPageViewModel : ViewModel, IView
    {        
        private IBaanRepository _baanRepository;
        private IVerenigingRepository _verenigingRepository;
        private IReserveringRepository _reserveringRepository;
        private INavigationService _navigationService;
        //private IReserveringUserControlViewModel _reserveringUserControlViewModel;
        private IResourceLoader _resourceLoader;
        private IAlertMessageService _alertMessageService;
        private IEventAggregator _eventAggregator;
        private Vereniging _vereniging;
        private Afhang _afhang;
        private ObservableCollection<ScheduleAppointment> _reserveringen;

        //private ObservableCollection<Baan> _banen;
        private ObservableCollection<ResourceType> _banen;

        public BaanschemaPageViewModel(IBaanRepository baanRepository, IVerenigingRepository verenigingRepository, IReserveringRepository reserveringRepository, INavigationService navigationService,
                                    /* IReserveringUserControlViewModel reserveringUserControlViewModel, */
            IResourceLoader resourceLoader, IAlertMessageService alertMessageService, IEventAggregator eventAggregator)
        {
            _baanRepository = baanRepository;
            _verenigingRepository = verenigingRepository;
            _reserveringRepository = reserveringRepository;
            _navigationService = navigationService;
            //_reserveringUserControlViewModel = reserveringUserControlViewModel;
            _resourceLoader = resourceLoader;
            _alertMessageService = alertMessageService;
            _eventAggregator = eventAggregator;

            GoBackCommand = new DelegateCommand(_navigationService.GoBack);
            VisibleDatesChangingCommand = new DelegateCommand<VisibleDatesChangingEventArgs>(BanenSchema_VisibleDatesChanging);

            if (_eventAggregator != null)
            {
                //_eventAggregator.GetEvent<ReserveringUpdatedEvent>().Subscribe(UpdateBanenAsync);
                //_eventAggregator.GetEvent<SpelerUpdatedEvent>().Subscribe(UpdateBanenAsync);
                _eventAggregator.GetEvent<BaanUpdatedEvent>().Subscribe(UpdateBanenAsync);
            }

            //WijzigBaanCommand = DelegateCommand.FromAsyncHandler(BaanWijzigen);

            UpdateBanenAsync(null);

        }

        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand GoNextCommand { get; private set; }

        public DelegateCommand<Syncfusion.UI.Xaml.Schedule.VisibleDatesChangingEventArgs> VisibleDatesChangingCommand { get; set; }

        public ObservableCollection<ResourceType> Banen
        {
            get { return _banen; }
            private set { SetProperty(ref _banen, value); }
        }

        public ObservableCollection<ScheduleAppointment> Reserveringen
        {
            get { return _reserveringen; }
            private set { SetProperty(ref _reserveringen, value); }
        }

        public Afhang Afhang
        {
            get { return _afhang; }
            private set { SetProperty(ref _afhang, value); }
        }
        /*
        public ObservableCollection<Baan> Banen
        {
            get { return _banen; }
            private set { SetProperty(ref _banen, value); }
        }
        */

        public async void UpdateBanenAsync(object notUsed)
        {
            await UpdateBanenInfoAsync();
        }

        private async Task UpdateBanenInfoAsync()
        {
            if (_vereniging == null)
                _vereniging = await _verenigingRepository.GetVerenigingAsync();
            if(_afhang == null)
                _afhang = await _verenigingRepository.GetVerenigingSettingsAsync();
            
            List<Baan> banen = await _baanRepository.GetBanenByDateAsync(_vereniging.Id, DateTime.Now);

            ResourceType banenResource = new ResourceType { TypeName = "Banen" };

            foreach (Baan baan in banen)
            {
                banenResource.ResourceCollection.Add(new Resource { DisplayName = baan.Naam, ResourceName = baan.Naam, TypeName = "Banen" });
            }

            Banen = new ObservableCollection<ResourceType>();
            Banen.Add(banenResource);
        }

        private async void BanenSchema_VisibleDatesChanging(VisibleDatesChangingEventArgs e)
        {
            ObservableCollection<DateTime> dates = e.NewValue as ObservableCollection<DateTime>;
            DateTime _date = DateTime.Now;
            if (dates.Count > 0)
                _date = dates[0];

            if (_vereniging == null)
                _vereniging = await _verenigingRepository.GetVerenigingAsync();
            if (_afhang == null)
                _afhang = await _verenigingRepository.GetVerenigingSettingsAsync();

            List<Baan> banen = await _baanRepository.GetBanenByDateAsync(_vereniging.Id, _date);

            ResourceType banenResource = new ResourceType { TypeName = "Banen" };
            
            Reserveringen = new ObservableCollection<ScheduleAppointment>();

            foreach (Baan baan in banen)
            {
                banenResource.ResourceCollection.Add(new Resource { DisplayName = baan.Naam, ResourceName = baan.Naam, TypeName = "Banen" });
                /*
                int hours = new Random().Next(Afhang.BaanBegin.Hours, Afhang.BaanEinde.Hours);
                int mins = new Random().Next(Afhang.Duur_Een, Afhang.Duur_Vier);
                Reserveringen.Add(new ScheduleAppointment { StartTime = _date.AddHours(hours).AddMinutes(mins * -1), EndTime = _date.AddHours(hours).AddMinutes(mins), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Les.ToString() }, ResourceCollection = new ObservableCollection<Resource> { new Resource() { ResourceName = baan.Naam, TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true });

                hours = new Random().Next(Afhang.BaanBegin.Hours, Afhang.BaanEinde.Hours);
                mins = new Random().Next(Afhang.Duur_Een, Afhang.Duur_Vier);
                Reserveringen.Add(new ScheduleAppointment { StartTime = _date.AddHours(hours).AddMinutes(mins * -1), EndTime = _date.AddHours(hours).AddMinutes(mins), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Afhangen.ToString() }, ResourceCollection = new ObservableCollection<Resource> { new Resource() { ResourceName = baan.Naam, TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true });

                hours = new Random().Next(Afhang.BaanBegin.Hours, Afhang.BaanEinde.Hours);
                mins = new Random().Next(Afhang.Duur_Een, Afhang.Duur_Vier);
                Reserveringen.Add(new ScheduleAppointment { StartTime = _date.AddHours(hours).AddMinutes(mins * -1), EndTime = _date.AddHours(hours).AddMinutes(mins), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Competitie.ToString() }, ResourceCollection = new ObservableCollection<Resource> { new Resource() { ResourceName = baan.Naam, TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true });
                */
            }

            Banen = new ObservableCollection<ResourceType>();
            Banen.Add(banenResource);

            ObservableCollection<Reservering> reserveringen = await _reserveringRepository.GetReserveringenByDateAsync(_date);

            foreach (Reservering reservering in reserveringen)
            {
                Reserveringen.Add(new ScheduleAppointment { StartTime = reservering.Datum.Add(reservering.BeginTijd), EndTime = reservering.Datum.Add(reservering.EindTijd), Location = reservering.Baan.Naam, Status = new ScheduleAppointmentStatus { Status = reservering.Soort.ToString() }, ResourceCollection = new ObservableCollection<Resource> { new Resource() { ResourceName = reservering.Baan.Naam, TypeName = "Banen" } }, Subject = String.IsNullOrWhiteSpace(reservering.Beschrijving) ? reservering.Soort.ToString(): reservering.Beschrijving, ReadOnly = true });
            }
        }

        public override async void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            _navigationService.Navigate("Main", null);
            base.OnNavigatedFrom(viewModelState, suspending);
        }

        public override async void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
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

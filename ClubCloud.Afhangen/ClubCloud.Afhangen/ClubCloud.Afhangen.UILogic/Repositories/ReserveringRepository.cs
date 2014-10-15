using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using ClubCloud.Afhangen.UILogic.ViewModels;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public class ReserveringRepository : IReserveringRepository
    {
        public const string ReserveringIdKey = "ReserveringId";
        private readonly IReserveringService _reserveringService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISessionStateService _sessionStateService;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly ISpelerRepository _spelerRepository;
        private readonly IBaanRepository _baanRepository;
        private Guid _reserveringId;
        private Reservering _cachedReservering = null;
        private ObservableCollection<Reservering> _cachedReserveringen = null;
        private static DateTime _today;

        public ReserveringRepository(IReserveringService reserveringService, IVerenigingRepository verenigingRepository, ISpelerRepository spelerRepository, IBaanRepository baanRepository, IEventAggregator eventAggregator, ISessionStateService sessionStateService)
        {
            _reserveringService = reserveringService;
            _eventAggregator = eventAggregator;
            _sessionStateService = sessionStateService;
            _verenigingRepository = verenigingRepository;
            _spelerRepository = spelerRepository;
            _baanRepository = baanRepository;

            if (_sessionStateService != null && _sessionStateService.SessionState.ContainsKey(ReserveringIdKey))
            {
                _reserveringId = Guid.Parse(_sessionStateService.SessionState[ReserveringIdKey].ToString());
            }
            else
            {
                _reserveringId = Guid.Empty;
                _sessionStateService.SessionState[ReserveringIdKey] = _reserveringId;
            }
        }

        public async Task ClearReserveringAsync()
        {
            CreateEmptyReservering();
            RaiseReserveringUpdated();
        }

        public async Task<Reservering> GetReserveringAsync()
        {
            if (_cachedReservering != null) return _cachedReservering;

            if (_cachedReservering == null) CreateEmptyReservering();

            /*
            if (_reserveringId != Guid.Empty)
            {
                Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                _cachedReservering = await _reserveringService.GetReserveringAsync(vereniging.Id, _reserveringId);
            }
            */

            RaiseReserveringUpdated();

            return _cachedReservering;
        }

        public async Task<Reservering> GetReserveringByIdAsync(Guid reserveringId)
        {
            if (_cachedReserveringen == null || (_today == null || _today < DateTime.Today))
            {
                _cachedReserveringen = new ObservableCollection<Reservering>();

                Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                List<Reservering> reserveringen = await _reserveringService.GetReserveringenAsync(vereniging.Id);

                foreach (Reservering reservering in reserveringen)
                {
                    reservering.Baan = await _baanRepository.GetBaanAsync(vereniging.Id, reservering.BaanId.Value);
                    _cachedReserveringen.Add(reservering);
                }
                _today = DateTime.Today;
            }

            if (_cachedReservering != null && _reserveringId == reserveringId) return _cachedReservering;

            if (_cachedReservering == null) CreateEmptyReservering();

            if (_reserveringId == Guid.Empty || _reserveringId != reserveringId) _reserveringId = reserveringId;

            if (_reserveringId != Guid.Empty)
            {
                _cachedReservering = _cachedReserveringen.SingleOrDefault(r => r.Id == _reserveringId);
                //Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                //_cachedReservering = await _reserveringService.GetReserveringAsync(vereniging.Id, _reserveringId);
            }

            RaiseReserveringUpdated();

            return _cachedReservering;

        }

        public async Task<ObservableCollection<Reservering>> GetReserveringenAsync()
        {
            if (_cachedReserveringen == null || (_today == null || _today < DateTime.Today))
            {
                _cachedReserveringen = new ObservableCollection<Reservering>();

                Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                List<Reservering> reserveringen = await _reserveringService.GetReserveringenAsync(vereniging.Id);

                foreach (Reservering reservering in reserveringen)
                {
                    reservering.Baan = await _baanRepository.GetBaanAsync(vereniging.Id, reservering.BaanId.Value);
                    _cachedReserveringen.Add(reservering);
                }
                _today = DateTime.Today;
            }

            return _cachedReserveringen;
        }

        public async Task<ObservableCollection<Reservering>> GetReserveringenByBaanAsync(Guid baanId)
        {
            if (_cachedReserveringen == null || (_today == null || _today < DateTime.Today))
            {
                _cachedReserveringen = new ObservableCollection<Reservering>();

                Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                List<Reservering> reserveringen = await _reserveringService.GetReserveringenAsync(vereniging.Id);

                foreach (Reservering reservering in reserveringen)
                {
                    reservering.Baan = await _baanRepository.GetBaanAsync(vereniging.Id, reservering.BaanId.Value); 
                    _cachedReserveringen.Add(reservering);
                }
                _today = DateTime.Today;
            }

            return new ObservableCollection<Reservering>(_cachedReserveringen.Where(r => r.BaanId == baanId));
        }

        public async Task<ObservableCollection<Reservering>> GetReserveringenBySpelerAsync(Guid spelerId)
        {
            if (_cachedReserveringen == null || (_today == null || _today < DateTime.Today))
            {
                _cachedReserveringen = new ObservableCollection<Reservering>();

                Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                List<Reservering> reserveringen = await _reserveringService.GetReserveringenAsync(vereniging.Id);

                foreach (Reservering reservering in reserveringen)
                {
                    if(_cachedReserveringen.Count(r => r.Id == reservering.Id) == 0)
                    {
                        reservering.Baan = await _baanRepository.GetBaanAsync(vereniging.Id, reservering.BaanId.Value);
                        _cachedReserveringen.Add(reservering);
                    }
                        
                }
                _today = DateTime.Today;
            }

            ObservableCollection<Reservering> aankomende = new ObservableCollection<Reservering>(_cachedReserveringen.Where(r => r.BeginTijd >= DateTime.Now.TimeOfDay || r.EindTijd >= DateTime.Now.TimeOfDay));
            ObservableCollection<Reservering> gevonden = new ObservableCollection<Reservering>();
            foreach (Reservering reservering in aankomende)
            {

                if(reservering.Spelers.Count(s => s.Id == spelerId)> 0)
                {
                    gevonden.Add(reservering);
                }
            }

            return gevonden;
        }

        public async Task<Reservering> AddSpelerToReserveringAsync(int index, Guid spelerId)
        {
            if (_cachedReservering == null) CreateEmptyReservering();

            Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
            
            Speler speler = await _spelerRepository.GetSpelerAsync(vereniging.Id, spelerId);

            _cachedReservering.Spelers[index] = speler;

            await CalCulateDuration(TimeSpan.Zero);

            RaiseSpelerUpdated(speler);

            return _cachedReservering;
        }

        public async Task<Reservering> RemoveSpelerFromReserveringAsync(int index, Guid spelerId)
        {
            if (_cachedReservering == null) CreateEmptyReservering();

            Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();

            //Speler speler = await _spelerRepository.GetSpelerAsync(vereniging.Id, spelerId);

            _cachedReservering.Spelers[index] = new Speler { Id = Guid.Empty, Bondsnummer = "00000000", Roepnaam = "Tennis", Achternaam = "Speler" };

            await CalCulateDuration(TimeSpan.Zero);

            RaiseSpelerUpdated(new Speler { Id = Guid.Empty, Bondsnummer = "00000000", Roepnaam ="Tennis", Achternaam ="Speler" });

            return _cachedReservering;
        }

        public async Task<Reservering> AddBaanToReserveringAsync(Guid baanId, TimeSpan beginTijd, TimeSpan duur)
        {
            if (_cachedReservering == null) CreateEmptyReservering();

            Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
            Baan baan = await _baanRepository.GetBaanAsync(vereniging.Id, baanId);

            if (_cachedReservering.Baan != baan || _cachedReservering.BaanId != baan.Id)
            {
                _cachedReservering.Baan = baan;
                _cachedReservering.BaanId = baan.Id;
                _cachedReservering.Datum = DateTime.Now.Date;
                _cachedReservering.BeginTijd = beginTijd;
            }

            await CalCulateDuration(duur);

            RaiseBaanUpdated();

            return _cachedReservering;

        }

        public async Task<Reservering> RemoveBaanFromReserveringAsync(Guid baanId, TimeSpan beginTijd, TimeSpan duur)
        {
            if (_cachedReservering == null) CreateEmptyReservering();

            //Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
            //Baan baan = await _baanRepository.GetBaanAsync(vereniging.Id, baanId);

            if (_cachedReservering.Baan.Id == baanId || _cachedReservering.BaanId == baanId)
            {
                _cachedReservering.Baan = new Baan { Id = Guid.Empty, Naam = "", Nummer = 0, Baansoort = "" };
            }

            await CalCulateDuration(duur);

            RaiseBaanUpdated();

            return _cachedReservering;

        }
        private void CreateEmptyReservering()
        {
            _reserveringId = Guid.Empty;
            _cachedReservering = new Reservering();
            _cachedReservering.Id = Guid.Empty;
            _cachedReservering.Baan = new Baan { Id = Guid.Empty, Naam = "", Nummer = 0, Baansoort = "" };

            Speler emptySpeler = new Speler { Id = Guid.Empty, Bondsnummer = "00000000", Roepnaam = "Tennis", Achternaam = "Speler" };
            for (int i = 0; i < 4; i++)
            {
                Speler speler = emptySpeler;
                try
                {
                    _cachedReservering.Spelers.Insert(i, speler);
                }
                catch
                {
                }
            }
        }

        public async Task CalCulateDuration(TimeSpan duur)
        {
            Afhang afhang = await _verenigingRepository.GetVerenigingSettingsAsync();
            int count = _cachedReservering.Spelers.Where(s => s.Id != Guid.Empty).Count();
            switch (count)
            {
                case 0: _cachedReservering.Duur = TimeSpan.FromMinutes(0); break;
                case 1: _cachedReservering.Duur = TimeSpan.FromMinutes(afhang.Duur_Een); break;
                case 2: _cachedReservering.Duur = TimeSpan.FromMinutes(afhang.Duur_Twee); break;
                case 3: _cachedReservering.Duur = TimeSpan.FromMinutes(afhang.Duur_Drie); break;
                case 4: _cachedReservering.Duur = TimeSpan.FromMinutes(afhang.Duur_Vier); break;
                default: _cachedReservering.Duur = TimeSpan.FromMinutes(0); break;
            }

            if (duur != TimeSpan.Zero)
            {
                if (_cachedReservering.Duur > duur)
                    _cachedReservering.Duur = duur;
            }

            _cachedReservering.EindTijd = _cachedReservering.BeginTijd.Add(_cachedReservering.Duur);

        }

        public async Task<Reservering> SetReserveringAsync(Reservering reservering)
        {
            Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
            _cachedReservering = await _reserveringService.SetReserveringAsync(vereniging.Id, reservering);
            _cachedReservering.Baan = await _baanRepository.GetBaanAsync(vereniging.Id, _cachedReservering.BaanId.Value);

            if (_cachedReserveringen.Count(r => r.Id == reservering.Id) == 0)
            {
                _cachedReserveringen.Add(_cachedReservering);
            }

            _reserveringId = _cachedReservering.Id;
            RaiseReserveringUpdated();

            return _cachedReservering;

        }

        public async Task<bool> DeleteReserveringAsync(Guid reserveringId)
        {
            
            Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
            bool succes = await _reserveringService.DeleteReserveringAsync(vereniging.Id, reserveringId);
            if (succes)
            {
                try
                {
                    Reservering deletedReservering = _cachedReserveringen.SingleOrDefault(r => r.Id == _reserveringId);

                    if (deletedReservering != null)
                    {
                        _cachedReserveringen.Remove(deletedReservering);
                    }
                    CreateEmptyReservering();
                }
                catch { }
                RaiseReserveringUpdated();
            }
            return succes;
        }

        private void RaiseReserveringUpdated()
        {
            // Documentation on loosely coupled communication is at http://go.microsoft.com/fwlink/?LinkID=288820&clcid=0x409 
            _eventAggregator.GetEvent<ReserveringUpdatedEvent>().Publish(null);
            //_eventAggregator.GetEvent<SpelerUpdatedEvent>().Publish(null);
           // _eventAggregator.GetEvent<BaanUpdatedEvent>().Publish(null);
        }

        private void RaiseSpelerUpdated(Speler speler)
        {
            // Documentation on loosely coupled communication is at http://go.microsoft.com/fwlink/?LinkID=288820&clcid=0x409 
            _eventAggregator.GetEvent<SpelerUpdatedEvent>().Publish(speler);
            //_eventAggregator.GetEvent<ReserveringUpdatedEvent>().Publish(null);
        }

        private void RaiseBaanUpdated()
        {
            // Documentation on loosely coupled communication is at http://go.microsoft.com/fwlink/?LinkID=288820&clcid=0x409 
            _eventAggregator.GetEvent<BaanUpdatedEvent>().Publish(null);
            //_eventAggregator.GetEvent<ReserveringUpdatedEvent>().Publish(null);
        }
    }
}
using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public class VerenigingRepository : IVerenigingRepository
    {
        public const string VerenigingIdKey = "VerenigingId";
        public const string AfhangIdKey = "AfhangId";
        private readonly IVerenigingService _verenigingService;
        private readonly ILocationService _locationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISessionStateService _sessionStateService;

        private Guid _verenigingId;
        private Guid _afhangId;
        private Vereniging _cachedVereniging = null;
        private Afhang _cachedAfhang = null;

        public VerenigingRepository(IVerenigingService verenigingService,ILocationService locationService, IEventAggregator eventAggregator, ISessionStateService sessionStateService)
        {
            _locationService = locationService;
            _verenigingService = verenigingService;
            _eventAggregator = eventAggregator;
            _sessionStateService = sessionStateService;

            if (_sessionStateService != null && (_sessionStateService.SessionState.ContainsKey(VerenigingIdKey) && _sessionStateService.SessionState.ContainsKey(AfhangIdKey)))
            {
                _verenigingId = Guid.Parse(_sessionStateService.SessionState[VerenigingIdKey].ToString());
                _afhangId = Guid.Parse(_sessionStateService.SessionState[AfhangIdKey].ToString());
            }
            else
            {
                GetVerenigingByGeoLocatorAsync();
                //_verenigingId = Guid.NewGuid();
                
            }
        }

        public async void GetVerenigingByGeoLocatorAsync()
        {
            Geoposition position = await _locationService.GetLocationAsync();
            _cachedVereniging = await GetVerenigingByLocatieAsync(position.Coordinate.Point.Position.Longitude, position.Coordinate.Point.Position.Latitude);
            _verenigingId = _cachedVereniging.Id;
            _sessionStateService.SessionState[VerenigingIdKey] = _verenigingId;
            RaiseVerenigingUpdated();
        }

        public async Task<Vereniging> GetVerenigingAsync()
        {
            if (_cachedVereniging != null) return _cachedVereniging;

            _cachedVereniging = await _verenigingService.GetVerenigingAsync(_verenigingId);
            _cachedAfhang = await GetVerenigingSettingsAsync();
            RaiseVerenigingUpdated();
            return _cachedVereniging;

        }

        public async Task<Afhang> GetVerenigingSettingsAsync()
        {
            if (_cachedAfhang != null) return _cachedAfhang;
            
            _cachedAfhang = await _verenigingService.GetVerenigingSettingsAsync(_verenigingId);
            _afhangId = _cachedAfhang.Id;
            _sessionStateService.SessionState[AfhangIdKey] = _afhangId;
            RaiseVerenigingUpdated();
            return _cachedAfhang;

        }

        public async Task<Vereniging> GetVerenigingByLocatieAsync(double Longitude, double Latitude)
        {
            if (_cachedVereniging != null) return _cachedVereniging;

            _cachedVereniging = await _verenigingService.GetVerenigingByLocatieAsync(Longitude, Latitude);
            _verenigingId = _cachedVereniging.Id;
            _cachedAfhang = await GetVerenigingSettingsAsync();
            RaiseVerenigingUpdated();
            return _cachedVereniging;

        }

        public async Task<Vereniging> GetVerenigingByNummerAsync(string verenigingNummer)
        {
            if (_cachedVereniging != null) return _cachedVereniging;

            _cachedVereniging = await _verenigingService.GetVerenigingByNummerAsync(verenigingNummer);
            _verenigingId = _cachedVereniging.Id;
            _cachedAfhang = await GetVerenigingSettingsAsync();
            RaiseVerenigingUpdated();
            return _cachedVereniging;
        }

        private void RaiseVerenigingUpdated()
        {
            // Documentation on loosely coupled communication is at http://go.microsoft.com/fwlink/?LinkID=288820&clcid=0x409 
            _eventAggregator.GetEvent<VerenigingUpdatedEvent>().Publish(null);
        }
    }
}
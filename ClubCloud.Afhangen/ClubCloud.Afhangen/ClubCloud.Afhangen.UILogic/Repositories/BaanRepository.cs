using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public class BaanRepository : IBaanRepository
    {
        public const string BaanIdKey = "BaanId";
        private readonly IBaanService _baanService;
        private readonly IEventAggregator _eventAggregator;

        private List<Baan> _cachedBaan = null;

        public BaanRepository(IBaanService baanService, IEventAggregator eventAggregator)
        {
            _baanService = baanService;
            _eventAggregator = eventAggregator;
        }

        public async Task<List<Baan>> GetBanenAsync(Guid verenigingId)
        {
            if (_cachedBaan != null) return _cachedBaan;

            ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId);

            _cachedBaan = new List<Baan>();

            foreach (Baan baan in banen)
            {
                _cachedBaan.Add(baan);
                //RaiseBaanUpdated();
            }
            //RaiseBaanUpdated();

            return _cachedBaan;
        }

        public async Task<Baan> GetBaanAsync(Guid verenigingId,Guid baanId)
        {
            Baan baan = null;
            if (_cachedBaan != null) baan =_cachedBaan.SingleOrDefault(b => b.Id == baanId);

            if (baan == null)
            {
                baan = await _baanService.GetBaanAsync(verenigingId,baanId);
                _cachedBaan.Add(baan);
                RaiseBaanUpdated();
            }
            return _cachedBaan.SingleOrDefault(b => b.Id == baanId);
        }

        private void RaiseBaanUpdated()
        {
            // Documentation on loosely coupled communication is at http://go.microsoft.com/fwlink/?LinkID=288820&clcid=0x409 
            _eventAggregator.GetEvent<BaanUpdatedEvent>().Publish(null);
        }
    }
}
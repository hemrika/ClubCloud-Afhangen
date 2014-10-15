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

        private List<Baan> _cachedBanen = null;

        public BaanRepository(IBaanService baanService, IEventAggregator eventAggregator)
        {
            _baanService = baanService;
            _eventAggregator = eventAggregator;
        }

        public async Task<List<Baan>> GetBanenAsync(Guid verenigingId)
        {
            if(_cachedBanen == null)
            {
                _cachedBanen = new List<Baan>();

                ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId);
                foreach (Baan baan in banen)
                {
                    if (_cachedBanen.Count(b => b.Id == baan.Id) == 0)
                        _cachedBanen.Add(baan);
                }
            }

            return _cachedBanen;
        }

        public async Task<Baan> GetBaanAsync(Guid verenigingId,Guid baanId)
        {
            if (_cachedBanen == null)
            {
                _cachedBanen = new List<Baan>();

                ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId);
                foreach (Baan baan in banen)
                {
                    if (_cachedBanen.Count(b => b.Id == baan.Id) == 0)
                        _cachedBanen.Add(baan);
                }
            }
            return _cachedBanen.SingleOrDefault(b => b.Id == baanId);
        }

        private void RaiseBaanUpdated()
        {
            // Documentation on loosely coupled communication is at http://go.microsoft.com/fwlink/?LinkID=288820&clcid=0x409 
            _eventAggregator.GetEvent<BaanUpdatedEvent>().Publish(null);
        }
    }
}
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

        private List<Baanschema> _cachedBaanschema = null;
        private List<Baan> _cachedBanen = null;
        private List<Baanblok> _cachedBaanblokken = null;
        private List<Baansoort> _cachedBaansoorten = null;
        private List<Baantype> _cachedBaantypes = null;

        public BaanRepository(IBaanService baanService, IEventAggregator eventAggregator)
        {
            _baanService = baanService;
            _eventAggregator = eventAggregator;
        }

        public async Task<List<Baan>> GetBanenByDateAsync(Guid verenigingId, DateTime date)
        {
            if (_cachedBanen == null)
            {
                _cachedBaanschema = new List<Baanschema>();
                _cachedBanen = new List<Baan>();
                _cachedBaanblokken = new List<Baanblok>();
                _cachedBaansoorten = new List<Baansoort>();
                _cachedBaantypes = new List<Baantype>();
            }
            ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId);

            foreach (Baan baan in banen)
            {
                if (!_cachedBanen.Any(b => b.Id == baan.Id))
                    _cachedBanen.Add(baan);
            }

            ObservableCollection<Baanschema> baanschemas = await _baanService.GetBaanschemaByDateAsync(verenigingId, date);

            foreach (Baanschema baanschema in baanschemas)
            {
                if (!_cachedBaanschema.Any(b => b.Id == baanschema.Id))
                    _cachedBaanschema.Add(baanschema);
            }

            foreach (Baan baan in _cachedBanen)
            {
                ObservableCollection<Baanblok> baanblokken = await _baanService.GetBaanblokkenAsync(verenigingId, baan.AccommodatieId);

                foreach (Baanblok baanblok in baanblokken)
                {
                    if (!_cachedBaanblokken.Any(b => b.Id == baanblok.Id))
                    {
                        _cachedBaanblokken.Add(baanblok);

                        Baansoort baansoort = null;
                        if (baanblok.BaansoortId.HasValue)
                            baansoort = await _baanService.GetBaansoortAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaansoortId.Value);

                        if (!_cachedBaansoorten.Any(b => b.Id == baansoort.Id))
                            _cachedBaansoorten.Add(baansoort);

                        Baantype baantype = null;
                        if (baanblok.BaantypeId.HasValue)
                            baantype = await _baanService.GetBaantypeAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaantypeId.Value);

                        if (!_cachedBaantypes.Any(b => b.Id == baantype.Id))
                            _cachedBaantypes.Add(baantype);
                    }
                }

                Baanblok blok = _cachedBaanblokken.SingleOrDefault(b => b.Id == baan.BaanblokId);

                if (blok != null)
                {
                    Baantype type = _cachedBaantypes.SingleOrDefault(b => b.Id == blok.BaantypeId.Value);
                    Baansoort soort = _cachedBaansoorten.SingleOrDefault(b => b.Id == blok.BaansoortId.Value);

                    baan.Locatie = blok.Locatie;
                    baan.Verlichting = blok.Verlichting;

                    if (type != null)
                        baan.Type = type.Naam;

                    if (soort != null)
                        baan.Soort = soort.Naam;
                }
            }
            if (_cachedBaanschema.Count > 0)
            {
                var beschikbaar = _cachedBaanschema.Where(s => s.Dag.HasFlag(date.DayOfWeek) && s.DagBegin <= DateTime.Now.TimeOfDay && s.DagEinde >= DateTime.Now.TimeOfDay).Select(s => s.BaanId);
                return _cachedBanen.Where(b => beschikbaar.Contains(b.Id)).OrderBy(b => b.Nummer).ToList();
            }

            return _cachedBanen;

        }

        public async Task<List<Baan>> GetBanenAsync(Guid verenigingId)
        {
            if (_cachedBanen == null)
            {
                _cachedBaanschema = new List<Baanschema>();
                _cachedBanen = new List<Baan>();
                _cachedBaanblokken = new List<Baanblok>();
                _cachedBaansoorten = new List<Baansoort>();
                _cachedBaantypes = new List<Baantype>();
            }
            ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId);

            foreach (Baan baan in banen)
            {
                if (!_cachedBanen.Any(b => b.Id == baan.Id))
                    _cachedBanen.Add(baan);
            }

            ObservableCollection<Baanschema> baanschemas = await _baanService.GetBaanschemaAsync(verenigingId);

            foreach (Baanschema baanschema in baanschemas)
            {
                if (!_cachedBaanschema.Any(b => b.Id == baanschema.Id))
                    _cachedBaanschema.Add(baanschema);
            }

            foreach (Baan baan in _cachedBanen)
            {
                ObservableCollection<Baanblok> baanblokken = await _baanService.GetBaanblokkenAsync(verenigingId, baan.AccommodatieId);

                foreach (Baanblok baanblok in baanblokken)
                {
                    if (!_cachedBaanblokken.Any(b => b.Id == baanblok.Id))
                    {
                        _cachedBaanblokken.Add(baanblok);

                        Baansoort baansoort = null;
                        if (baanblok.BaansoortId.HasValue)
                            baansoort = await _baanService.GetBaansoortAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaansoortId.Value);

                        if (!_cachedBaansoorten.Any(b => b.Id == baansoort.Id))
                            _cachedBaansoorten.Add(baansoort);

                        Baantype baantype = null;
                        if (baanblok.BaantypeId.HasValue)
                            baantype = await _baanService.GetBaantypeAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaantypeId.Value);

                        if (!_cachedBaantypes.Any(b => b.Id == baantype.Id))
                            _cachedBaantypes.Add(baantype);
                    }
                }

                Baanblok blok = _cachedBaanblokken.SingleOrDefault(b => b.Id == baan.BaanblokId);

                if (blok != null)
                {
                    Baantype type = _cachedBaantypes.SingleOrDefault(b => b.Id == blok.BaantypeId.Value);
                    Baansoort soort = _cachedBaansoorten.SingleOrDefault(b => b.Id == blok.BaansoortId.Value);

                    baan.Locatie = blok.Locatie;
                    baan.Verlichting = blok.Verlichting;

                    if (type != null)
                        baan.Type = type.Naam;

                    if (soort != null)
                        baan.Soort = soort.Naam;
                }
            }


            if (_cachedBaanschema.Count > 0)
            {
                var beschikbaar = _cachedBaanschema.Where(s => s.Dag.HasFlag(DateTime.Now.DayOfWeek) && s.DagBegin <= DateTime.Now.TimeOfDay && s.DagEinde >= DateTime.Now.TimeOfDay).Select(s => s.BaanId);
                return _cachedBanen.Where(b => beschikbaar.Contains(b.Id)).OrderBy(b => b.Nummer).ToList();
            }

            return _cachedBanen;
        }

        public async Task<Baan> GetBaanAsync(Guid verenigingId, Guid baanId)
        {
            if (_cachedBanen == null)
            {
                _cachedBaanschema = new List<Baanschema>();
                _cachedBanen = new List<Baan>();
                _cachedBaanblokken = new List<Baanblok>();
                _cachedBaansoorten = new List<Baansoort>();
                _cachedBaantypes = new List<Baantype>();

                ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId);
                foreach (Baan baan in banen)
                {
                    if (_cachedBanen.Count(b => b.Id == baan.Id) == 0)
                        _cachedBanen.Add(baan);
                }

                ObservableCollection<Baanschema> baanschemas = await _baanService.GetBaanschemaAsync(verenigingId);

                foreach (Baanschema baanschema in baanschemas)
                {
                    if (!_cachedBaanschema.Any(b => b.Id == baanschema.Id))
                        _cachedBaanschema.Add(baanschema);
                }

                foreach (Baan baan in _cachedBanen)
                {
                    ObservableCollection<Baanblok> baanblokken = await _baanService.GetBaanblokkenAsync(verenigingId, baan.AccommodatieId);

                    foreach (Baanblok baanblok in baanblokken)
                    {
                        if (!_cachedBaanblokken.Any(b => b.Id == baanblok.Id))
                        {
                            _cachedBaanblokken.Add(baanblok);

                            Baansoort baansoort = null;
                            if (baanblok.BaansoortId.HasValue)
                                baansoort = await _baanService.GetBaansoortAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaansoortId.Value);

                            if (!_cachedBaansoorten.Any(b => b.Id == baansoort.Id))
                                _cachedBaansoorten.Add(baansoort);

                            Baantype baantype = null;
                            if (baanblok.BaantypeId.HasValue)
                                baantype = await _baanService.GetBaantypeAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaantypeId.Value);

                            if (!_cachedBaantypes.Any(b => b.Id == baantype.Id))
                                _cachedBaantypes.Add(baantype);
                        }
                    }

                    Baanblok blok = _cachedBaanblokken.SingleOrDefault(b => b.Id == baan.BaanblokId);

                    if (blok != null)
                    {
                        Baantype type = _cachedBaantypes.SingleOrDefault(b => b.Id == blok.BaantypeId.Value);
                        Baansoort soort = _cachedBaansoorten.SingleOrDefault(b => b.Id == blok.BaansoortId.Value);

                        baan.Locatie = blok.Locatie;
                        baan.Verlichting = blok.Verlichting;

                        if (type != null)
                            baan.Type = type.Naam;

                        if (soort != null)
                            baan.Soort = soort.Naam;
                    }
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
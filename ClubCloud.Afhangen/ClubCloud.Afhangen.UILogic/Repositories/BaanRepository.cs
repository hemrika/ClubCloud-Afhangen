using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism.Interfaces;
using ClubCloud.Core.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public class BaanRepository : IBaanRepository
    {
        public const string BaanIdKey = "BaanId";
        private readonly IBaanService _baanService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISessionStateService _sessionStateService;

        private List<Baanschema> _cachedBaanschema = null;
        private List<Baan> _cachedBanen = null;
        private List<Baanblok> _cachedBaanblokken = null;
        private List<Baansoort> _cachedBaansoorten = null;
        private List<Baantype> _cachedBaantypes = null;

        public BaanRepository(IBaanService baanService, IEventAggregator eventAggregator, ISessionStateService sessionStateService)
        {
            _baanService = baanService;
            _eventAggregator = eventAggregator;
            _sessionStateService = sessionStateService;
        }

        public async Task<List<Baan>> GetBanenByDateAsync(Guid verenigingId, Guid accommodatieId, DateTime date)
        {
            if (_cachedBanen == null)
            {
                _cachedBanen = new List<Baan>();
                ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId, accommodatieId);

                foreach (Baan baan in banen)
                {

                    if (!_cachedBanen.Any(b => b.Id == baan.Id))
                        _cachedBanen.Add(baan);
                }
            }

            if (_cachedBaanblokken == null || _cachedBaanblokken.Count == 0)
            {
                _cachedBaanblokken = new List<Baanblok>();
                //Guid accommodatieId = Guid.Empty;
                foreach (Baan baan in _cachedBanen)
                {
                    if (accommodatieId == Guid.Empty || accommodatieId != baan.AccommodatieId)
                    {
                        accommodatieId = baan.AccommodatieId;
                        ObservableCollection<Baanblok> baanblokken = await _baanService.GetBaanblokkenAsync(verenigingId, baan.AccommodatieId);

                        foreach (Baanblok baanblok in baanblokken)
                        {
                            if (!_cachedBaanblokken.Any(b => b.Id == baanblok.Id))
                                _cachedBaanblokken.Add(baanblok);
                        }
                    }
                }
            }

            if (_cachedBaansoorten == null || _cachedBaansoorten.Count == 0 )
            {
                _cachedBaansoorten = new List<Baansoort>();

                foreach (Baanblok baanblok in _cachedBaanblokken)
                {
                    if (baanblok.BaansoortId.HasValue)
                    {
                        Baansoort baansoort = null;
                        if (!_cachedBaansoorten.Any(b => b.Id == baanblok.BaansoortId.Value))
                            baansoort = await _baanService.GetBaansoortAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaansoortId.Value);

                        if (baansoort != null)
                            _cachedBaansoorten.Add(baansoort);
                    }
                }
            }

            if (_cachedBaantypes == null || _cachedBaansoorten.Count == 0 )
            {
                _cachedBaantypes = new List<Baantype>();

                foreach (Baanblok baanblok in _cachedBaanblokken)
                {
                    if (baanblok.BaantypeId.HasValue)
                    {
                        Baantype baantype = null;
                        if (!_cachedBaantypes.Any(b => b.Id == baanblok.BaantypeId.Value))
                            baantype = await _baanService.GetBaantypeAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaantypeId.Value);

                        if (baantype != null)
                            _cachedBaantypes.Add(baantype);
                    }
                }
            }

            foreach (Baan baan in _cachedBanen)
            {
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

            if (_cachedBaanschema == null)
            {
                _cachedBaanschema = new List<Baanschema>();
            }

            ObservableCollection<Baanschema> baanschemas = await _baanService.GetBaanschemaByDateAsync(verenigingId, date);

            foreach (Baanschema baanschema in baanschemas)
            {
                if (!_cachedBaanschema.Any(b => b.Id == baanschema.Id))
                    _cachedBaanschema.Add(baanschema);
            }

            if (_cachedBaanschema.Count > 0)
            {
                var beschikbaar = _cachedBaanschema.Where(s => s.Dag.HasFlag(date.DayOfWeek) && s.DagBegin <= DateTime.Now.TimeOfDay && s.DagEinde >= DateTime.Now.TimeOfDay).Select(s => s.BaanId);
                return _cachedBanen.Where(b => beschikbaar.Contains(b.Id)).OrderBy(b => b.Nummer).ToList();
            }

            return _cachedBanen;

        }

        public async Task<List<Baan>> GetBanenAsync(Guid verenigingId, Guid accommodatieId)
        {
            if (_cachedBanen == null)
            {
                _cachedBanen = new List<Baan>();
                ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId, accommodatieId);

                foreach (Baan baan in banen)
                {

                    if (!_cachedBanen.Any(b => b.Id == baan.Id))
                        _cachedBanen.Add(baan);
                }
            }

            if (_cachedBaanblokken == null)
            {
                _cachedBaanblokken = new List<Baanblok>();
                accommodatieId = Guid.Empty;
                foreach (Baan baan in _cachedBanen)
                {
                    if (accommodatieId == Guid.Empty || accommodatieId != baan.AccommodatieId)
                    {
                        accommodatieId = baan.AccommodatieId;
                        ObservableCollection<Baanblok> baanblokken = await _baanService.GetBaanblokkenAsync(verenigingId, baan.AccommodatieId);

                        foreach (Baanblok baanblok in baanblokken)
                        {
                            if (!_cachedBaanblokken.Any(b => b.Id == baanblok.Id))
                                _cachedBaanblokken.Add(baanblok);
                        }
                    }
                }
            }

            if (_cachedBaansoorten == null)
            {
                _cachedBaansoorten = new List<Baansoort>();

                foreach (Baanblok baanblok in _cachedBaanblokken)
                {
                    if (baanblok.BaansoortId.HasValue)
                    {
                        Baansoort baansoort = null;
                        if (!_cachedBaansoorten.Any(b => b.Id == baanblok.BaansoortId.Value))
                            baansoort = await _baanService.GetBaansoortAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaansoortId.Value);

                        if (baansoort != null)
                            _cachedBaansoorten.Add(baansoort);
                    }
                }
            }

            if (_cachedBaantypes == null)
            {
                _cachedBaantypes = new List<Baantype>();

                foreach (Baanblok baanblok in _cachedBaanblokken)
                {
                    if (baanblok.BaantypeId.HasValue)
                    {
                        Baantype baantype = null;
                        if (!_cachedBaantypes.Any(b => b.Id == baanblok.BaantypeId.Value))
                            baantype = await _baanService.GetBaantypeAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaantypeId.Value);

                        if (baantype != null)
                            _cachedBaantypes.Add(baantype);
                    }
                }
            }

            foreach (Baan baan in _cachedBanen)
            {
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

            if (_cachedBaanschema == null)
            {
                _cachedBaanschema = new List<Baanschema>();
            }

            ObservableCollection<Baanschema> baanschemas = await _baanService.GetBaanschemaAsync(verenigingId);

            foreach (Baanschema baanschema in baanschemas)
            {
                if (!_cachedBaanschema.Any(b => b.Id == baanschema.Id))
                    _cachedBaanschema.Add(baanschema);
            }

            if (_cachedBaanschema.Count > 0)
            {
                var beschikbaar = _cachedBaanschema.Where(s => s.Dag.HasFlag(DateTime.Now.DayOfWeek) && s.DagBegin <= DateTime.Now.TimeOfDay && s.DagEinde >= DateTime.Now.TimeOfDay).Select(s => s.BaanId);
                return _cachedBanen.Where(b => beschikbaar.Contains(b.Id)).OrderBy(b => b.Nummer).ToList();
            }

            return _cachedBanen;
        }

        public async Task<Baan> GetBaanAsync(Guid verenigingId, Guid accommodatieId, Guid baanId)
        {
            if (_cachedBanen == null)
            {
                _cachedBanen = new List<Baan>();
                ObservableCollection<Baan> banen = await _baanService.GetBanenAsync(verenigingId, accommodatieId);

                foreach (Baan baan in banen)
                {

                    if (!_cachedBanen.Any(b => b.Id == baan.Id))
                        _cachedBanen.Add(baan);
                }
            }

            if (_cachedBaanblokken == null)
            {
                _cachedBaanblokken = new List<Baanblok>();
                accommodatieId = Guid.Empty;
                foreach (Baan baan in _cachedBanen)
                {
                    if (accommodatieId == Guid.Empty || accommodatieId != baan.AccommodatieId)
                    {
                        accommodatieId = baan.AccommodatieId;
                        ObservableCollection<Baanblok> baanblokken = await _baanService.GetBaanblokkenAsync(verenigingId, baan.AccommodatieId);

                        foreach (Baanblok baanblok in baanblokken)
                        {
                            if (!_cachedBaanblokken.Any(b => b.Id == baanblok.Id))
                                _cachedBaanblokken.Add(baanblok);
                        }
                    }
                }
            }

            if (_cachedBaansoorten == null)
            {
                _cachedBaansoorten = new List<Baansoort>();

                foreach (Baanblok baanblok in _cachedBaanblokken)
                {
                    if (baanblok.BaansoortId.HasValue)
                    {
                        Baansoort baansoort = null;
                        if (!_cachedBaansoorten.Any(b => b.Id == baanblok.BaansoortId.Value))
                            baansoort = await _baanService.GetBaansoortAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaansoortId.Value);

                        if (baansoort != null)
                            _cachedBaansoorten.Add(baansoort);
                    }
                }
            }

            if (_cachedBaantypes == null)
            {
                _cachedBaantypes = new List<Baantype>();

                foreach (Baanblok baanblok in _cachedBaanblokken)
                {
                    if (baanblok.BaantypeId.HasValue)
                    {
                        Baantype baantype = null;
                        if (!_cachedBaantypes.Any(b => b.Id == baanblok.BaantypeId.Value))
                            baantype = await _baanService.GetBaantypeAsync(verenigingId, baanblok.AccommodatieId.Value, baanblok.BaantypeId.Value);

                        if (baantype != null)
                            _cachedBaantypes.Add(baantype);
                    }
                }
            }

            foreach (Baan baan in _cachedBanen)
            {
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

            if (_cachedBaanschema == null)
            {
                _cachedBaanschema = new List<Baanschema>();
            }

            ObservableCollection<Baanschema> baanschemas = await _baanService.GetBaanschemaAsync(verenigingId);

            foreach (Baanschema baanschema in baanschemas)
            {
                if (!_cachedBaanschema.Any(b => b.Id == baanschema.Id))
                    _cachedBaanschema.Add(baanschema);
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
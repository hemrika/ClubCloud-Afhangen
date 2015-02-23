using ClubCloud.Afhangen.UILogic.ClubCloudService;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public class BaanServiceProxy : IBaanService
    {
        private ClubCloudService.ClubCloudAfhangenClient client = new ClubCloudService.ClubCloudAfhangenClient(ClubCloudService.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen11);

        public async Task<ObservableCollection<Models.Baanschema>> GetBaanschemaAsync(Guid verenigingId)
        {
            ObservableCollection<Models.Baanschema> _baanschema = new ObservableCollection<Models.Baanschema>();

            ObservableCollection<ClubCloud_Baanschema> baanschemas = await client.GetBaanSchemaByVerenigingIdAsync("0000000", verenigingId, false);

            foreach (ClubCloud_Baanschema baanschema in baanschemas)
            {
                _baanschema.Add(new Models.Baanschema { Id = baanschema.Id, BaanId = baanschema.BaanId.Value, Beschikbaar = baanschema.Beschikbaar, Dag = (DayOfWeek)Enum.ToObject(typeof(DayOfWeek), (int)baanschema.Dag), DagBegin = baanschema.DagBegin, DagEinde = baanschema.DagEinde });//, Baansoort = baan.Baansoort.ToString() });
            }

            return _baanschema;
        }

        public async Task<ObservableCollection<Models.Baanschema>> GetBaanschemaByDateAsync(Guid verenigingId, DateTime date)
        {
            ObservableCollection<Models.Baanschema> _baanschema = new ObservableCollection<Models.Baanschema>();

            ObservableCollection<ClubCloud_Baanschema> baanschemas = await client.GetBaanSchemaByDateAsync("0000000", verenigingId, date, false);

            foreach (ClubCloud_Baanschema baanschema in baanschemas)
            {
                _baanschema.Add(new Models.Baanschema { Id = baanschema.Id, BaanId = baanschema.BaanId.Value, Beschikbaar = baanschema.Beschikbaar, Dag = (DayOfWeek)Enum.ToObject(typeof(DayOfWeek), (int)baanschema.Dag), DagBegin = baanschema.DagBegin, DagEinde = baanschema.DagEinde });//, Baansoort = baan.Baansoort.ToString() });
            }

            return _baanschema;
        }

        public async Task<Models.Baan> GetBaanAsync(Guid verenigingId, Guid baanId)
        {
            ObservableCollection<Models.Baan> _banen = new ObservableCollection<Models.Baan>();
            _banen = await GetBanenAsync(verenigingId);

            Models.Baan baan = _banen.SingleOrDefault(b => b.Id == baanId);

            return baan;
        }

        public async Task<ObservableCollection<Models.Baan>> GetBanenAsync(Guid verenigingId)
        {
            ObservableCollection<Models.Baan> _banen = new ObservableCollection<Models.Baan>();

            ObservableCollection<ClubCloud_Baan> banen = await client.GetBanenByVerenigingIdAsync("0000000", verenigingId, false);

            foreach (ClubCloud_Baan baan in banen)
            {
                _banen.Add(new Models.Baan { Id = baan.Id, Naam = baan.Naam, Nummer = baan.Nummer, AccommodatieId = baan.AccommodatieId.Value, BaanblokId = baan.BaanblokId.Value });//, Baansoort = baan.Baansoort.ToString() });
            }

            return _banen;
        }


        public async Task<ObservableCollection<Models.Baanblok>> GetBaanblokkenAsync(Guid verenigingId, Guid accommodatieId)
        {
            ObservableCollection<Models.Baanblok> _baanblokken = new ObservableCollection<Models.Baanblok>();

            ObservableCollection<ClubCloud_Baanblok> baanblokken = await client.GetBaanblokkenByAccommodatieIdAsync("0000000", verenigingId, accommodatieId, false);

            foreach (ClubCloud_Baanblok baanblok in baanblokken)
            {
                _baanblokken.Add(new Models.Baanblok { Id = baanblok.Id, Naam = baanblok.Naam, BaansoortId = baanblok.BaansoortId.Value, BaantypeId = baanblok.BaantypeId.Value, Locatie = baanblok.Locatie, Verlichting = baanblok.Verlichting, AccommodatieId = baanblok.AccommodatieId.Value });
            }

            return _baanblokken;
        }

        public async Task<Models.Baansoort> GetBaansoortAsync(Guid verenigingId, Guid accommodatieId, Guid baansoortId)
        {
            Models.Baansoort _baansoort = new Models.Baansoort();

            ClubCloud_Baansoort baansoort = await client.GetBaansoortByIdAsync("0000000", verenigingId, accommodatieId, baansoortId, false);

            _baansoort = new Models.Baansoort { Id = baansoort.Id, Naam = baansoort.Naam, Beschrijving = baansoort.Beschrijving, BaantypeId = baansoort.BaantypeId.Value, Code = baansoort.Code, Meervoud = baansoort.Meervoud, Omschrijving = baansoort.Omschrijving };

            return _baansoort;
        }

        public async Task<Models.Baantype> GetBaantypeAsync(Guid verenigingId, Guid accommodatieId, Guid baantypeId)
        {
            Models.Baantype _baantype = new Models.Baantype();

            ClubCloud_Baantype baantype = await client.GetBaantypeByIdAsync("0000000", verenigingId, accommodatieId, baantypeId, false);

            _baantype = new Models.Baantype { Id = baantype.Id, Naam = baantype.Naam, Beschrijving = baantype.Beschrijving, Code = baantype.Code, Meervoud = baantype.Meervoud, Omschrijving = baantype.Omschrijving };

            return _baantype;
        }
    }
}

using ClubCloud.Afhangen.UILogic.ClubCloudService;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public class BaanServiceProxy : IBaanService
    {
        private ClubCloudService.ClubCloudAfhangenClient client = new ClubCloudService.ClubCloudAfhangenClient(ClubCloudService.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen1);

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
                _banen.Add(new Models.Baan { Id = baan.Id, Naam = baan.Naam, Nummer = baan.Nummer, Baansoort = baan.Baansoort.ToString() });
            }

            return _banen;
        }
    }
}

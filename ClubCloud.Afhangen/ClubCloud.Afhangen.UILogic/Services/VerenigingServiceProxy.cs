using ClubCloud.Afhangen.UILogic.ClubCloudService;
using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public class VerenigingServiceProxy : IVerenigingService
    {
        private ClubCloudService.ClubCloudAfhangenClient client = new ClubCloudService.ClubCloudAfhangenClient(ClubCloudService.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen11);


        public async Task<Vereniging> GetVerenigingAsync(Guid verenigingId)
        {
            Vereniging vereniging = new Vereniging { Id = Guid.NewGuid() };

            ClubCloud_Vereniging ccVereniging = await client.GetVerenigingByIdAsync("00000000", verenigingId, false);

            return vereniging;
        }

        public async Task<Vereniging> GetVerenigingByLocatieAsync(double Longitude, double Latitude)
        {
            Vereniging vereniging = new Vereniging { Id = Guid.NewGuid() };

            ClubCloud_Vereniging ccVereniging = await client.GetVerenigingByLocationAsync("00000000", Latitude, Longitude, false);

            if (ccVereniging != null)
            {
                vereniging = new Vereniging { Id = ccVereniging.Id, Naam = ccVereniging.Naam, Nummer = ccVereniging.Nummer };
            }
            else
            {
                await Task.Delay(5000);
                if (vereniging == null)
                {
                    vereniging = await GetVerenigingByLocatieAsync(Longitude, Latitude);
                }
            }

            return vereniging;
        }

        public async Task<Vereniging> GetVerenigingByNummerAsync(string verenigingNummer)
        {
            Vereniging vereniging = new Vereniging { Id = Guid.NewGuid() };

            ClubCloud_Vereniging ccVereniging = await client.GetVerenigingByNummerAsync("00000000", verenigingNummer, false);

            vereniging = new Vereniging { Id = ccVereniging.Id };
            return vereniging;
        }

        public async Task<Afhang> GetVerenigingSettingsAsync(Guid verenigingId)
        {
            Afhang afhang = new Afhang { Id = Guid.NewGuid(), VerenigingId = verenigingId };

            ClubCloud_Afhang ccAfhang = await client.GetVerenigingAfhangSettingsAsync("00000000", verenigingId, false);

            afhang = new Afhang { Id = ccAfhang.Id, VerenigingId = ccAfhang.VerenigingId, BaanBegin = ccAfhang.BaanBegin, BaanEinde = ccAfhang.BaanEinde, Duur_Drie = ccAfhang.Duur_Drie, Duur_Een = ccAfhang.Duur_Een, Duur_Precisie = ccAfhang.Duur_Precisie, Duur_Twee = ccAfhang.Duur_Twee, Duur_Vier = ccAfhang.Duur_Vier, MaandBegin = ccAfhang.MaandBegin, MaandEinde = ccAfhang.MaandEinde };
            return afhang;
        }
    } 
}
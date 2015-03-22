using ClubCloud.Afhangen.UILogic.ClubCloudAfhangen;
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
        private ClubCloudAfhangen.ClubCloudAfhangenClient client = new ClubCloudAfhangen.ClubCloudAfhangenClient(ClubCloudAfhangen.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen1);
        private ClubCloudAgent.AgentServiceClient agent = new ClubCloudAgent.AgentServiceClient(ClubCloudAgent.AgentServiceClient.EndpointConfiguration.BasicHttpBinding_IAgentService);

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


        public async Task UpdateStoreAgentAsync(string verenigingNummer)
        {
            try
            {
                agent.SetUserForServiceAsync(verenigingNummer);
            }
            catch { };

        }

        public async Task UpdateKioskModeAsync()
        {
            try
            { 
                agent.UpdateKioskModeAsync();
            }
            catch { };
        }
    } 
}
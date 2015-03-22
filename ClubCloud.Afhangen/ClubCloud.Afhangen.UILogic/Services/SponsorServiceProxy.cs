using ClubCloud.Afhangen.UILogic.ClubCloudAfhangen;
using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public class SponsorServiceProxy : ISponsorService
    {
        private ClubCloudAfhangen.ClubCloudAfhangenClient client = new ClubCloudAfhangen.ClubCloudAfhangenClient(ClubCloudAfhangen.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen1);

        public async Task<Sponsor> GetSponsorAsync(Guid verenigingId, Guid sponsorId)
        {
            Sponsor sponsor = new Sponsor();
            
            ClubCloud_Sponsor ccsponsor = await client.GetSponsorByIdAsync("00000000", verenigingId, sponsorId, false);

            sponsor = new Sponsor { Id = ccsponsor.Id, Naam = ccsponsor.Naam, Type = "item", Tekst = ccsponsor.Tekst, VerenigingId = ccsponsor.VerenigingId, AfbeeldingId = ccsponsor.AfbeeldingId };
            

            return sponsor;
        }

        public async Task<List<Sponsor>> GetSponsorenAsync(Guid verenigingId)
        {
            List<Sponsor> sponsoren = new List<Sponsor>();

            
            ObservableCollection<ClubCloudAfhangen.ClubCloud_Sponsor> ccsponsoren = await client.GetSponsorenByVerenigingIdAsync("00000000", verenigingId, false);

            foreach (ClubCloud_Sponsor ccsponsor in ccsponsoren)
            {
                sponsoren.Add(new Sponsor
                {
                    Id = ccsponsor.Id, 
                    Naam = ccsponsor.Naam, 
                    Type = "item", 
                    Tekst = ccsponsor.Tekst, 
                    VerenigingId = ccsponsor.VerenigingId, 
                    AfbeeldingId = ccsponsor.AfbeeldingId  
                });
            }
            
            return sponsoren;
        }

        public async Task<Foto> GetSponsorImageByIdAsync(Guid verenigingId, Guid afbeeldingId)
        {
            Foto foto = new Foto();

            ClubCloud_Sponsor_Afbeelding ccfoto = await client.GetSponsorImageByIdAsync("00000000", verenigingId, afbeeldingId, false);

            foto = new Foto { Id = ccfoto.Id, ContentData = ccfoto.Afbeelding };
            
            return foto;
        }

        /*
        public async Task<Foto> GetSponsorImageAsync(Guid verenigingId, Guid sponsorId)
        {
            Foto foto = new Foto();

            ClubCloud_Sponsor_Afbeelding ccfoto = await client.GetSponsorImageByIdAsync("00000000", verenigingId, sponsorId, false);

            foto = new Foto { ContentData = ccfoto.ContentData, ContentLength = ccfoto.ContentLength, ContentType = ccfoto.ContentType, FotoId = ccfoto.FotoId, Id = ccfoto.Id };

            return foto;
        }
        */
    }
}

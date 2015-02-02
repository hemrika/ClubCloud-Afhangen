using ClubCloud.Afhangen.UILogic.ClubCloudService;
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
        private ClubCloudService.ClubCloudAfhangenClient client = new ClubCloudService.ClubCloudAfhangenClient(ClubCloudService.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen11);

        public async Task<Sponsor> GetSponsorAsync(Guid verenigingId, Guid sponsorId)
        {
            Sponsor sponsor = new Sponsor();
            /*
            ClubCloud_Sponsor gebruiker = await client.GetSponsorByIdAsync("00000000", verenigingId, sponsorId, false);

            sponsor = new Speler { Id = gebruiker.Id, Achternaam = gebruiker.Achternaam, Bondsnummer = gebruiker.Bondsnummer, Roepnaam = gebruiker.Roepnaam, Tussenvoegsel = gebruiker.Tussenvoegsel };
            */

            return sponsor;
        }

        public async Task<List<Sponsor>> GetSponsorenAsync(Guid verenigingId)
        {
            List<Sponsor> sponsoren = new List<Sponsor>();

            /*
            ObservableCollection<ClubCloudService.ClubCloud_Sponsor> ccsponsoren = await client.GetSponsorenByVerenigingIdAsync("00000000", verenigingId, false);

            foreach (ClubCloud_Sponsor ccsponsor in ccsponsoren)
            {
                sponsoren.Add(new Sponsor
                {
                    Id = Guid.NewGuid(),
                    Afbeelding = new Uri(""),
                    Naam = "",
                    Type = "item"
                });
            }
            */
            return sponsoren;
        }

        public async Task<Foto> GetSponsorImageAsync(Guid verenigingId, Guid sponsorId)
        {
            Foto foto = new Foto();
            /*
            ClubCloud_Foto ccfoto = await client.GetSponsorImageAsync("00000000", verenigingId, sponsorId, false);

            foto = new Foto { ContentData = ccfoto.ContentData, ContentLength = ccfoto.ContentLength, ContentType = ccfoto.ContentType, FotoId = ccfoto.FotoId, Id = ccfoto.Id };
            */
            return foto;
        }
    }
}

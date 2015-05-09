using ClubCloud.Afhangen.UILogic.ClubCloudAfhangen;
using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public class SpelerServiceProxy : ISpelerService
    {
        private ClubCloudAfhangen.ClubCloudAfhangenClient client = new ClubCloudAfhangen.ClubCloudAfhangenClient(ClubCloudAfhangen.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen1);

        public async Task<Models.Speler> GetSpelerAsync(Guid verenigingId, Guid gebruikerId)
        {
            Speler speler = new Speler();
            ClubCloud_Gebruiker gebruiker = await client.GetGebruikerByIdAsync("00000000", verenigingId, gebruikerId, false);

            speler = new Speler { Id = gebruiker.Id, Achternaam = gebruiker.Achternaam, Bondsnummer = gebruiker.Bondsnummer, Roepnaam = gebruiker.Roepnaam, Tussenvoegsel = gebruiker.Tussenvoegsel };

            return speler;
        }

        public async Task<Models.Speler> GetSpelerByNummerAsync(Guid verenigingId, string nummer)
        {
            Speler speler = new Speler();
            ClubCloud_Gebruiker gebruiker = await client.GetGebruikerByNummerAsync("00000000", verenigingId, nummer, false);

            speler = new Speler { Id = gebruiker.Id, Achternaam = gebruiker.Achternaam, Bondsnummer = gebruiker.Bondsnummer, Roepnaam = gebruiker.Roepnaam, Tussenvoegsel = gebruiker.Tussenvoegsel };

            return speler;
        }

        public async Task<Models.Foto> GetFotoAsync(Guid verenigingId, Guid gebruikerId)
        {
            Foto foto = new Foto();
            ClubCloud_Foto ccfoto = await client.GetFotoByIdAsync("00000000", verenigingId, gebruikerId, false);

            foto = new Foto { ContentData = ccfoto.ContentData, ContentLength = ccfoto.ContentLength, ContentType = ccfoto.ContentType, FotoId = ccfoto.FotoId, Id = ccfoto.Id };

            return foto;
        }

        /*
        public async Task<Models.Foto> GetFotoByNummerAsync(Guid verenigingId, string nummer)
        {
            Foto foto = new Foto();
            ClubCloud_Foto ccfoto = await client.GetFotoByNummerAsync("00000000", verenigingId, nummer, false);

            foto = new Foto { ContentData = ccfoto.ContentData, ContentLength = ccfoto.ContentLength, ContentType = ccfoto.ContentType, FotoId = ccfoto.FotoId, Id = ccfoto.Id };

            return foto;
        }
        */
    }
}

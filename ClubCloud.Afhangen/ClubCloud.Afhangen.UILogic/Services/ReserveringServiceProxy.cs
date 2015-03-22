using ClubCloud.Afhangen;
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
    public class ReserveringServiceProxy : IReserveringService 
    {
        private ClubCloudAfhangen.ClubCloudAfhangenClient client = new ClubCloudAfhangen.ClubCloudAfhangenClient(ClubCloudAfhangen.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen1);

        public async Task<Reservering> GetReserveringAsync(Guid verenigingId, Guid reserveringId) 
        {
            Reservering reservering = new Reservering();
            ClubCloud_Reservering ccreservering = await client.GetReserveringByReserveringIdAsync("0000000", verenigingId, reserveringId, true);

            reservering = new Reservering
            {
                BaanId = ccreservering.BaanId,
                Datum = ccreservering.Datum,
                Duur = ccreservering.Duur,
                Final = ccreservering.Final,
                Id = ccreservering.Id,
                BeginTijd = ccreservering.Tijd,
                EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                Soort = (ClubCloud.Afhangen.UILogic.Models.ReserveringSoort)ccreservering.Soort,
                Beschrijving = ccreservering.Beschrijving,
                Spelers = new ObservableCollection<Speler>(),
            };

            if (ccreservering.Gebruiker_Een != null)
                reservering.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Een.Value });

            if (ccreservering.Gebruiker_Twee != null)
                reservering.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Twee.Value });

            if (ccreservering.Gebruiker_Drie != null)
                reservering.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Drie.Value });

            if (ccreservering.Gebruiker_Vier != null)
                reservering.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Vier.Value });

            return reservering;
        }

        public async Task<bool> DeleteReserveringAsync(Guid verenigingId,Guid reserveringId) 
        {
            List<Reservering> reserveringen = new List<Reservering>();
            bool result = await client.DeleteReserveringAsync("00000000", verenigingId, reserveringId, false);
            return result;
        }


        public async Task<Reservering> SetReserveringAsync(Guid verenigingId, Reservering reservering)
        {
            ObservableCollection<Guid> spelers = new ObservableCollection<Guid>();
            foreach (Speler speler in reservering.Spelers)
            {
                if (speler.Id != Guid.Empty)
                    spelers.Add(speler.Id);
            }
            ClubCloudAfhangen.ClubCloud_Reservering ccreservering = null;

            if (reservering.Id != Guid.Empty)
            {
                ClubCloud_Reservering _newreservering = new ClubCloud_Reservering
                {
                    BaanId = reservering.BaanId,
                    Beschrijving = "",
                    Datum = reservering.Datum,
                    Duur = reservering.Duur,
                    Soort = (ClubCloud.Afhangen.UILogic.ClubCloudAfhangen.ReserveringSoort)reservering.Soort,
                    Final = true,
                    Id = reservering.Id,
                    Tijd = reservering.BeginTijd
                };

                if (reservering.Spelers[0] != null)
                    _newreservering.Gebruiker_Een = reservering.Spelers[0].Id;

                if (reservering.Spelers[1] != null)
                    _newreservering.Gebruiker_Een = reservering.Spelers[1].Id;

                if (reservering.Spelers[2] != null)
                    _newreservering.Gebruiker_Een = reservering.Spelers[2].Id;

                if (reservering.Spelers[3] != null)
                    _newreservering.Gebruiker_Een = reservering.Spelers[3].Id;

                ccreservering = await client.UpdateReserveringAsync(
                "0000000",
                verenigingId,
                _newreservering,
               true,
                false);
            }

            if(reservering.Id == Guid.Empty)
                ccreservering = await client.SetReserveringAsync("00000000", verenigingId, reservering.Baan.Id, spelers, reservering.Datum, reservering.BeginTijd, reservering.Duur, (ClubCloud.Afhangen.UILogic.ClubCloudAfhangen.ReserveringSoort)reservering.Soort, true, false, "");

            if (ccreservering != null)
            {
                Reservering _new = new Reservering
                {
                    BaanId = ccreservering.BaanId,
                    Datum = ccreservering.Datum,
                    Duur = ccreservering.Duur,
                    Final = ccreservering.Final,
                    Id = ccreservering.Id,
                    BeginTijd = ccreservering.Tijd,
                    EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                    Soort = (ClubCloud.Afhangen.UILogic.Models.ReserveringSoort)ccreservering.Soort,
                    Beschrijving = ccreservering.Beschrijving,
                    Spelers = new ObservableCollection<Speler>(),
                };

                if (ccreservering.Gebruiker_Een != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Een.Value });

                if (ccreservering.Gebruiker_Twee != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Twee.Value });

                if (ccreservering.Gebruiker_Drie != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Drie.Value });

                if (ccreservering.Gebruiker_Vier != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Vier.Value });
                return _new;
            }
            return null;
        }

        public async Task<List<Reservering>> GetReserveringByBaanAsync(Guid verenigingId, Guid baanId)
        {
            List<Reservering> reserveringen = new List<Reservering>();
            ObservableCollection<ClubCloudAfhangen.ClubCloud_Reservering> ccreserveringen = await client.GetReserveringenByBaanIdAsync("00000000", verenigingId, baanId, false);

            foreach (ClubCloud_Reservering ccreservering in ccreserveringen)
            {
                Reservering _new = new Reservering
            {
                BaanId = ccreservering.BaanId,
                Datum = ccreservering.Datum,
                Duur = ccreservering.Duur,
                Final = ccreservering.Final,
                Id = ccreservering.Id,
                BeginTijd = ccreservering.Tijd,
                EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                Soort = (ClubCloud.Afhangen.UILogic.Models.ReserveringSoort)ccreservering.Soort,
                Beschrijving = ccreservering.Beschrijving,
                Spelers = new ObservableCollection<Speler>(),
            };

                if (ccreservering.Gebruiker_Een != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Een.Value });

                if (ccreservering.Gebruiker_Twee != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Twee.Value });

                if (ccreservering.Gebruiker_Drie != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Drie.Value });

                if (ccreservering.Gebruiker_Vier != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Vier.Value });

                reserveringen.Add(_new);
            }
            return reserveringen;
        }

        public async Task<List<Reservering>> GetReserveringByDateAsync(Guid verenigingId, DateTime date)
        {
            List<Reservering> reserveringen = new List<Reservering>();
            ObservableCollection<ClubCloudAfhangen.ClubCloud_Reservering> ccreserveringen = await client.GetReserveringenByDateAsync("00000000", verenigingId, date, false);

            foreach (ClubCloud_Reservering ccreservering in ccreserveringen)
            {
                Reservering _new = new Reservering
                {
                    BaanId = ccreservering.BaanId,
                    Datum = ccreservering.Datum,
                    Duur = ccreservering.Duur,
                    Final = ccreservering.Final,
                    Id = ccreservering.Id,
                    BeginTijd = ccreservering.Tijd,
                    EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                    Soort = (ClubCloud.Afhangen.UILogic.Models.ReserveringSoort)ccreservering.Soort,
                    Beschrijving = ccreservering.Beschrijving,
                    Spelers = new ObservableCollection<Speler>(),
                };

                if (ccreservering.Gebruiker_Een != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Een.Value });

                if (ccreservering.Gebruiker_Twee != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Twee.Value });

                if (ccreservering.Gebruiker_Drie != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Drie.Value });

                if (ccreservering.Gebruiker_Vier != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Vier.Value });

                reserveringen.Add(_new);
            }

            return reserveringen;
        }

        public async Task<List<Reservering>> GetReserveringenAsync(Guid verenigingId)
        {
            List<Reservering> reserveringen = new List<Reservering>();
            ObservableCollection<ClubCloudAfhangen.ClubCloud_Reservering> ccreserveringen = await client.GetReserveringenByVerenigingIdAsync("00000000", verenigingId, false);

            foreach (ClubCloud_Reservering ccreservering in ccreserveringen)
            {
                Reservering _new = new Reservering
                {
                    BaanId = ccreservering.BaanId,
                    Datum = ccreservering.Datum,
                    Duur = ccreservering.Duur,
                    Final = ccreservering.Final,
                    Id = ccreservering.Id,
                    BeginTijd = ccreservering.Tijd,
                    EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                    Soort = (ClubCloud.Afhangen.UILogic.Models.ReserveringSoort)ccreservering.Soort,
                    Beschrijving = ccreservering.Beschrijving,
                    Spelers = new ObservableCollection<Speler>(),
                };

                if (ccreservering.Gebruiker_Een != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Een.Value });

                if (ccreservering.Gebruiker_Twee != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Twee.Value });

                if (ccreservering.Gebruiker_Drie != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Drie.Value });

                if (ccreservering.Gebruiker_Vier != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Vier.Value });

                reserveringen.Add(_new);
            }

            return reserveringen;
        }


        //TODO change serice from nummer to guid
        public async Task<List<Reservering>> GetReserveringBySpelerAsync(Guid verenigingId, Guid spelerId)
        {
            List<Reservering> reserveringen = new List<Reservering>();
            ObservableCollection<ClubCloudAfhangen.ClubCloud_Reservering> ccreserveringen = await client.GetReserveringenByBondsnummerAsync("00000000", verenigingId, spelerId.ToString(), false);

            foreach (ClubCloud_Reservering ccreservering in ccreserveringen)
            {
                Reservering _new = new Reservering
                {
                    BaanId = ccreservering.BaanId,
                    Datum = ccreservering.Datum,
                    Duur = ccreservering.Duur,
                    Final = ccreservering.Final,
                    Id = ccreservering.Id,
                    BeginTijd = ccreservering.Tijd,
                    EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                    Soort = (ClubCloud.Afhangen.UILogic.Models.ReserveringSoort)ccreservering.Soort,
                    Beschrijving = ccreservering.Beschrijving,
                    Spelers = new ObservableCollection<Speler>(),
                };

                if (ccreservering.Gebruiker_Een != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Een.Value });

                if (ccreservering.Gebruiker_Twee != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Twee.Value });

                if (ccreservering.Gebruiker_Drie != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Drie.Value });

                if (ccreservering.Gebruiker_Vier != null)
                    _new.Spelers.Add(new Speler { Id = ccreservering.Gebruiker_Vier.Value });

                reserveringen.Add(_new);
            }

            return reserveringen;
        }

        public ClubCloud.Afhangen.UILogic.Models.ReserveringSoort Soort { get; set; }
    } 
}
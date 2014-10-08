using ClubCloud.Afhangen;
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
    public class ReserveringServiceProxy : IReserveringService 
    {
        private ClubCloudService.ClubCloudAfhangenClient client = new ClubCloudService.ClubCloudAfhangenClient(ClubCloudService.ClubCloudAfhangenClient.EndpointConfiguration.BasicHttpBinding_ClubCloudAfhangen1);

        public async Task<Reservering> GetReserveringAsync(Guid verenigingId, Guid reserveringId) 
        {
            Reservering reservering = new Reservering();
            ClubCloud_Reservering ccreservering = await client.GetReserveringByReserveringIdAsync("0000000", verenigingId, reserveringId, true);

            reservering = new Reservering
            {
                Id = ccreservering.Id,
                BaanId = ccreservering.BaanId,
                Datum = ccreservering.Datum,
                Duur = ccreservering.Duur,
                Final = ccreservering.Final,
                BeginTijd = ccreservering.Tijd,
                EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                Soort = ccreservering.Soort,
                Spelers = new ObservableCollection<Speler>{
            new  Speler{ Id = ccreservering.Gebruiker_Een.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Twee.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Drie.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Vier.Value},
                }
            };

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

            ClubCloudService.ClubCloud_Reservering ccreservering = await client.SetReserveringAsync("00000000", verenigingId, reservering.Baan.Id, spelers, reservering.Datum, reservering.BeginTijd, reservering.Duur, reservering.Soort, true, false, "");

            Reservering _newreservering = new Reservering
            {
                BaanId = ccreservering.BaanId,
                BeginTijd = ccreservering.Tijd,
                Datum = ccreservering.Datum,
                Duur = ccreservering.Duur,
                EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                Final = ccreservering.Final,
                Id = ccreservering.Id,
                Soort = ccreservering.Soort,
                Spelers = new ObservableCollection<Speler>{
            new  Speler{ Id = ccreservering.Gebruiker_Een.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Twee.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Drie.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Vier.Value},
                }
            };

            return _newreservering;
        }

        public async Task<List<Reservering>> GetReserveringByBaanAsync(Guid verenigingId, Guid baanId)
        {
            List<Reservering> reserveringen = new List<Reservering>();
            ObservableCollection<ClubCloudService.ClubCloud_Reservering> ccreserveringen = await client.GetReserveringenByBaanIdAsync("00000000", verenigingId, baanId, false);

            foreach (ClubCloud_Reservering ccreservering in ccreserveringen)
            {
                reserveringen.Add(new Reservering
                {
                    BaanId = ccreservering.BaanId,
                    Datum = ccreservering.Datum,
                    Duur = ccreservering.Duur,
                    Final = ccreservering.Final,
                    Id = ccreservering.Id,
                    BeginTijd = ccreservering.Tijd,
                    EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                    Soort = ccreservering.Soort,
                    Spelers = new ObservableCollection<Speler>{
            new  Speler{ Id = ccreservering.Gebruiker_Een.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Twee.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Drie.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Vier.Value},
                }
                });
            }
            return reserveringen;
        }


        public async Task<List<Reservering>> GetReserveringenAsync(Guid verenigingId)
        {
            List<Reservering> reserveringen = new List<Reservering>();
            ObservableCollection<ClubCloudService.ClubCloud_Reservering> ccreserveringen = await client.GetReserveringenByVerenigingIdAsync("00000000", verenigingId, false);

            foreach (ClubCloud_Reservering ccreservering in ccreserveringen)
            {
                reserveringen.Add(new Reservering
                {
                    BaanId = ccreservering.BaanId,
                    Datum = ccreservering.Datum,
                    Duur = ccreservering.Duur,
                    Final = ccreservering.Final,
                    Id = ccreservering.Id,
                    BeginTijd = ccreservering.Tijd,
                    EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                    Soort = ccreservering.Soort,
                    Spelers = new ObservableCollection<Speler>{
            new  Speler{ Id = ccreservering.Gebruiker_Een.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Twee.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Drie.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Vier.Value},
                }
                });
            }

            return reserveringen;
        }


        //TODO change serice from nummer to guid
        public async Task<List<Reservering>> GetReserveringBySpelerAsync(Guid verenigingId, Guid spelerId)
        {
            List<Reservering> reserveringen = new List<Reservering>();
            ObservableCollection<ClubCloudService.ClubCloud_Reservering> ccreserveringen = await client.GetReserveringenByBondsnummerAsync("00000000", verenigingId, spelerId.ToString(), false);

            foreach (ClubCloud_Reservering ccreservering in ccreserveringen)
            {
                reserveringen.Add(new Reservering
                {
                    BaanId = ccreservering.BaanId,
                    Datum = ccreservering.Datum,
                    Duur = ccreservering.Duur,
                    Final = ccreservering.Final,
                    Id = ccreservering.Id,
                    BeginTijd = ccreservering.Tijd,
                    EindTijd = ccreservering.Tijd.Add(ccreservering.Duur),
                    Soort = ccreservering.Soort,
                    Spelers = new ObservableCollection<Speler>{
            new  Speler{ Id = ccreservering.Gebruiker_Een.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Twee.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Drie.Value},
            new  Speler{ Id = ccreservering.Gebruiker_Vier.Value},
                }
                });
            }

            return reserveringen;
        }

        public ReserveringSoort Soort { get; set; }
    } 
}
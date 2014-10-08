using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public interface IVerenigingService
    {
        Task<Vereniging> GetVerenigingAsync(Guid verenigingId);

        Task<Vereniging> GetVerenigingByLocatieAsync(double Longitude, double Latitude);

        Task<Vereniging> GetVerenigingByNummerAsync(string verenigingNummer);

        Task<Afhang> GetVerenigingSettingsAsync(Guid verenigingId);
    }
}

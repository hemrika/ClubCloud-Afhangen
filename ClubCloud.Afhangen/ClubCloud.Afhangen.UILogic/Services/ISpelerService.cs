using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public interface ISpelerService
    {
        Task<Speler> GetSpelerAsync(Guid verenigingId, Guid spelerId);

        Task<Speler> GetSpelerByNummerAsync(Guid verenigingId, string nummer);

        /*
        Task<Models.Foto> GetFotoByNummerAsync(Guid verenigingId, string nummer);
        */

        Task<Models.Foto> GetFotoAsync(Guid verenigingId, Guid gebruikerId);
    }
}

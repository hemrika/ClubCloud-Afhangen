using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Core.Prism;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Windows.Storage;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public interface ISpelerRepository
    {
        Task<Speler> GetSpelerAsync(Guid verenigingId, Guid spelerId);

        Task<Speler> GetSpelerByNummerAsync(Guid verenigingId, string nummer);

        /*
        Task<Models.Foto> GetFotoByNummerAsync(Guid verenigingId, string nummer);
        */

        Task<Foto> GetFotoAsync(Guid verenigingId, Guid gebruikerId);

    } 
}

using ClubCloud.Afhangen.UILogic.Models;
using Microsoft.Practices.Prism.StoreApps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public interface IBaanRepository
    {
        Task<Baan> GetBaanAsync(Guid verenigingId, Guid baanId);

        Task<List<Baan>> GetBanenAsync(Guid verenigingId);
    } 
}

using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Core.Prism;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public interface IBaanRepository
    {
        Task<Baan> GetBaanAsync(Guid verenigingId, Guid accommodatieId, Guid baanId);

        Task<List<Baan>> GetBanenAsync(Guid verenigingId, Guid accommodatieId);

        Task<List<Baan>> GetBanenByDateAsync(Guid verenigingId, Guid accommodatieId, DateTime date);
    } 
}

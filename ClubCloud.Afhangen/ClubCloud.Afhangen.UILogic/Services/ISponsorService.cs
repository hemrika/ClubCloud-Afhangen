using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public interface ISponsorService
    {
        Task<Sponsor> GetSponsorAsync(Guid verenigingId, Guid sponsorId);

        Task<List<Sponsor>> GetSponsorenAsync(Guid verenigingId);

        Task<Foto> GetSponsorImageByIdAsync(Guid verenigingId, Guid afbeeldingId);
    }
}

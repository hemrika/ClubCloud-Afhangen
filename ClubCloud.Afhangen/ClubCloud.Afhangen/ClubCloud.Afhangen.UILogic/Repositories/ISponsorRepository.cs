using System;
namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public interface ISponsorRepository
    {
        System.Threading.Tasks.Task<ClubCloud.Afhangen.UILogic.Models.Sponsor> GetSponsorAsync(Guid verenigingId, Guid sponsorId);
        System.Threading.Tasks.Task<ClubCloud.Afhangen.UILogic.Models.Foto> GetSponsorImageAsync(Guid verenigingId, Guid sponsorId);
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<ClubCloud.Afhangen.UILogic.Models.Sponsor>> GetSponsorsAsync(Guid verenigingId);
    }
}

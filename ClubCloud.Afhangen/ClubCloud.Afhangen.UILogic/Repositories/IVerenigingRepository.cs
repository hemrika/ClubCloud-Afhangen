using ClubCloud.Afhangen.UILogic.Models;
using Microsoft.Practices.Prism.StoreApps;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public interface IVerenigingRepository
    {
        Task<Vereniging> GetVerenigingAsync();

        Task<Vereniging> GetVerenigingByLocatieAsync(double Longitude, double Latitude);

        Task<Vereniging> GetVerenigingByNummerAsync(string verenigingNummer);
        Task<Afhang> GetVerenigingSettingsAsync();

        System.Threading.Tasks.Task<global::Windows.ApplicationModel.Store.LicenseInformation> GetLicenseInformation();
        System.Threading.Tasks.Task<global::Windows.ApplicationModel.Store.ListingInformation> GetListingInformation();

        Task<bool> UpdateStoreAgentAsync();
        Task<bool> UpdateKioskModeAsync();
    } 
}

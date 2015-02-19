using ClubCloud.Afhangen.UILogic.Models;
using Microsoft.Practices.Prism.StoreApps;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public interface IReserveringRepository
    {
        Task<Reservering> GetReserveringAsync();

        Task<ObservableCollection<Reservering>> GetReserveringenAsync();
        Task<Reservering> GetReserveringByIdAsync(Guid reserveringId);
        Task<ObservableCollection<Reservering>> GetReserveringenByBaanAsync(Guid baanId);
        Task<ObservableCollection<Reservering>> GetReserveringenBySpelerAsync(Guid spelerId);
        Task<Reservering> AddSpelerToReserveringAsync(int index, Guid spelerId);
        Task<Reservering> RemoveSpelerFromReserveringAsync(int index, Guid spelerId);
        Task ClearReserveringAsync();
        Task<Reservering> SetReserveringAsync(Reservering reserveringReference);
        Task<Reservering> AddBaanToReserveringAsync(Guid baanId, TimeSpan BeginTijd, TimeSpan Duur);
        Task<Reservering> RemoveBaanFromReserveringAsync(Guid baanId, TimeSpan BeginTijd, TimeSpan Duur);
        Task<bool> DeleteReserveringAsync(Guid reserveringId);
    } 
}

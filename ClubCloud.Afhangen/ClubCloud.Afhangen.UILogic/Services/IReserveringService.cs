using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public interface IReserveringService
    {
        Task<Reservering> GetReserveringAsync(Guid verenigingId, Guid reserveringId);

        Task<bool> DeleteReserveringAsync(Guid verenigingId,Guid reserveringId);

        Task<Reservering> SetReserveringAsync(Guid verenigingId,Reservering reservering);

        Task<List<Reservering>> GetReserveringByBaanAsync(Guid verenigingId, Guid baanId);

        Task<List<Reservering>> GetReserveringenAsync(Guid verenigingId);

        Task<List<Reservering>> GetReserveringBySpelerAsync(Guid guid1, Guid guid2);
    }
}

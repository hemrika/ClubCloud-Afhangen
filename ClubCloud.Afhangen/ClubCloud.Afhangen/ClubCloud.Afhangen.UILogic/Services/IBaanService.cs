using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public interface IBaanService
    {
        Task<Models.Baan> GetBaanAsync(Guid verenigingId, Guid _baanId);

        Task<ObservableCollection<Models.Baan>> GetBanenAsync(Guid verenigingId);
    }
}

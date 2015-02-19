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
        Task<ObservableCollection<Models.Baanschema>> GetBaanschemaAsync(Guid verenigingId);

        Task<Models.Baan> GetBaanAsync(Guid verenigingId, Guid _baanId);

        Task<ObservableCollection<Models.Baan>> GetBanenAsync(Guid verenigingId);

        Task<ObservableCollection<Models.Baanblok>> GetBaanblokkenAsync(Guid verenigingId, Guid accommodatieId);

        Task<Models.Baansoort> GetBaansoortAsync(Guid verenigingId, Guid accommodatieId, Guid baansoortId);

        Task<Models.Baantype> GetBaantypeAsync(Guid verenigingId, Guid accommodatieId, Guid baantypeId);
    }
}

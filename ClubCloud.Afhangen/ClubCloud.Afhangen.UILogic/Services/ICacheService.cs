using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public interface ICacheService
    {
        Task<T> GetDataAsync<T>(string cacheKey);
        Task SaveDataAsync<T>(string cacheKey, T content);
    } 
}

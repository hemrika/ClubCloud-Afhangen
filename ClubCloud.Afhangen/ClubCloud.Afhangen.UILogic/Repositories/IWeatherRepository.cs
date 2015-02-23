using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Models.Entities;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
	public interface IWeatherRepository
	{
		Task<CurrentConditionsModel> GetCurrentConditionsAsync(string locationId, bool forceUpdate = false, bool getDetails = true, WeatherUnitTypes metric = WeatherUnitTypes.Metric);
        Task<ObservableCollection<ForecastModel>> GetForecastsAsync(string locationId, WeatherUnitTypes metric = WeatherUnitTypes.Metric);
        Task<ObservableCollection<HalfDayForecastModel>> GetHalfDayForecastsAsync(string locationId, bool forceUpdate = false, bool getDetails = true, WeatherUnitTypes metric = WeatherUnitTypes.Metric);
        Task<ObservableCollection<HourlyModel>> GetHourlyAsync(string locationId, bool forceUpdate = false, WeatherUnitTypes metric = WeatherUnitTypes.Metric);
    }
}

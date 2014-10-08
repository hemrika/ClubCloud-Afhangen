namespace ClubCloud.Afhangen.UILogic.Services
{
    public interface IWeatherService
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.Alert>> GetAlertsDetails(string locationKey);
        System.Threading.Tasks.Task<ClubCloud.Afhangen.UILogic.Models.CurrentConditions> GetCurrentConditions(string locationKey);
        System.Threading.Tasks.Task<ClubCloud.Afhangen.UILogic.Models.ForecastSummary> GetForecastSummary(string locationKey, DailyForecastQuery dailyForecastQuery = DailyForecastQuery.OneDay, bool? metric = null);
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.HourlyForecast>> GetHourlyForecasts(string locationKey, HourlyForecastQuery hourlyForecastQuery = HourlyForecastQuery.OneDay, bool? metric = null);
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.IndexGroupMetadata>> GetIndexGroupMetadata(string locationKey);
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.Index>> GetIndices(string locationKey, int indexGroup, DailyForecastQuery dailyForecastQuery = DailyForecastQuery.FiveDay);
    }
}

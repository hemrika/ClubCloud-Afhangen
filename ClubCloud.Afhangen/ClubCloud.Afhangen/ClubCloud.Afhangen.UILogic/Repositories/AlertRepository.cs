using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly IWeatherService _weatherServiceProvider;

        public AlertRepository(IWeatherService weatherServiceProvider)
        {
            this._weatherServiceProvider = weatherServiceProvider;
        }

        public async Task<ObservableCollection<AlertModel>> GetLocationAlerts(String locationKey)
        {
            ObservableCollection<AlertModel> observableCollection = new ObservableCollection<AlertModel>();
            foreach (Alert alertsDetail in await this._weatherServiceProvider.GetAlertsDetails(locationKey))
            {
                if (String.IsNullOrEmpty(alertsDetail.Description.Localized))
                {
                    alertsDetail.Description.Localized = alertsDetail.Description.English;
                }
                AlertModel alertModel = new AlertModel()
                {
                    AlertId = alertsDetail.AlertId,
                    CountryCode = alertsDetail.CountryCode,
                    Source = alertsDetail.Source,
                    Areas = new ObservableCollection<AlertAreaModel>(),
                    Title = alertsDetail.Description.English,
                    TitleLocal = alertsDetail.Description.Localized,
                    Type = alertsDetail.Type,
                    Priority = alertsDetail.Priority
                };
                AlertModel alertModel1 = alertModel;
                foreach (AlertArea area in alertsDetail.Area)
                {
                    AlertAreaModel alertAreaModel = new AlertAreaModel()
                    {
                        Name = area.Name,
                        StartTime = area.StartTime,
                        EndTime = area.EndTime,
                        LastAction = area.LastAction.English,
                        LastActionLocal = area.LastAction.Localized,
                        AlertText = area.Text
                    };
                    alertModel1.Areas.Add(alertAreaModel);
                }
                observableCollection.Add(alertModel1);
            }
            return observableCollection;
        }
    }
}
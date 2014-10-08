using ClubCloud.Afhangen.UILogic.Models;
using System;
namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    interface IWeerUserControlViewModel
    {
        ClubCloud.Afhangen.UILogic.Models.CurrentConditionsModel CurrentConditions { get; }
        System.Collections.ObjectModel.ObservableCollection<ClubCloud.Afhangen.UILogic.Models.HourlyModel> HourlyModels { get; }
        void UpdateWeatherAsync(bool update);

        byte[] WeerIcoon{get;}

    }
}

using System;
namespace ClubCloud.Afhangen.UILogic.Services
{
	public enum HourlyForecastQuery
	{
        [Label("1Hour")]
        OneHour,
        [Label("12Hour")]
        HalfDay,
        [Label("24Hour")]
        OneDay,
        [Label("72Hour")]
        ThreeDay,
        [Label("120Hour")]
        FiveDay,
        [Label("240Hour")]
        TenDay
	}
}

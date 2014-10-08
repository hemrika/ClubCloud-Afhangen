using System;
namespace ClubCloud.Afhangen.UILogic.Services
{
	public enum DailyForecastQuery
	{
        [Label("1Day")]
        OneDay,
        [Label("5Day")]
        FiveDay,
        [Label("10Day")]
        TenDay,
        [Label("15Day")]
        FifteenDay,
        [Label("25Day")]
        TwentyFiveDay
	}
}

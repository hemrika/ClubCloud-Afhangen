using System;
namespace ClubCloud.Afhangen.UILogic.Models.Entities
{
	public enum ErrorCodeType
	{
		UnableToConnectNetwork,
		FailedToGetWeatherData,
		FailedToGetCurrentConditionsData,
		FailedToGetForecastData,
		FailedToGetHourlyData,
		FailedToGetVideos,
		FailedParsingVideos,
		GpsFailure,
		GpsLocateTimedOut,
		LocationAlreadyExistsInYourList
	}
}

using System;
namespace ClubCloud.Afhangen.UILogic.Models.Entities
{
	public static class ErrorMessages
	{
		public static string GetErrorMessage(ErrorCodeType errorCode)
		{
			switch (errorCode)
			{
			case ErrorCodeType.UnableToConnectNetwork:
				return "Unable to connect to network.";
			case ErrorCodeType.FailedToGetCurrentConditionsData:
				return "Failed to retrieve Current Conditions from network.";
			case ErrorCodeType.FailedParsingVideos:
				return "Failed parsing of video feed.";
			case ErrorCodeType.GpsFailure:
				return "GPS was unable to locate your position.";
			case ErrorCodeType.GpsLocateTimedOut:
				return "GSP location timed out.";
			case ErrorCodeType.LocationAlreadyExistsInYourList:
				return "Location is already in your list.";
			}
			return "Unknown Error";
		}
	}
}

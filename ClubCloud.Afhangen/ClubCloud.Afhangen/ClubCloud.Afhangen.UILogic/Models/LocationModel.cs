using ClubCloud.Afhangen.UILogic.Models.Entities;
using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class LocationModel
	{
		public string DisplayName
		{
			get;
			set;
		}
		public string FullName
		{
			get;
			set;
		}
		public string LocationId
		{
			get;
			set;
		}
		public string CountryCode
		{
			get;
			set;
		}
		public string Latitude
		{
			get;
			set;
		}
		public string Longitude
		{
			get;
			set;
		}
		public double GmtOffSet
		{
			get;
			set;
		}
		public string VideoCode
		{
			get;
			set;
		}
		public string MapCode
		{
			get;
			set;
		}
		public string StationCode
		{
			get;
			set;
		}
		public string DmaId
		{
			get;
			set;
		}
		public LocationType LocationType
		{
			get;
			set;
		}
		public string CurrentConditionTemp
		{
			get;
			set;
		}
		public int CurrentConditionWeatherCode
		{
			get;
			set;
		}
		public string CurrentConditionsShortPhrase
		{
			get;
			set;
		}
		public string ForecastShortPhrase
		{
			get;
			set;
		}
		public string ForecastLFS
		{
			get;
			set;
		}
		public bool HasAlerts
		{
			get;
			set;
		}
		public LocationModel(string displayName, string fullName, string locationId, string latitude, string longitude, double gmtOffSet = 4.0, string videoCode = "", string dmaId = "")
		{
			this.DisplayName = displayName;
			this.FullName = fullName;
			this.LocationId = locationId;
			this.Latitude = latitude;
			this.Longitude = longitude;
			this.GmtOffSet = gmtOffSet;
			this.VideoCode = videoCode;
			this.DmaId = dmaId;
			this.LocationType = LocationType.Default;
		}
		public LocationModel()
		{
			this.HasAlerts = false;
		}
	}
}

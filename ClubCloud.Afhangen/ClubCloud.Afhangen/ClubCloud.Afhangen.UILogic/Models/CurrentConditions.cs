using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class CurrentConditions
	{
		public string LocationId
		{
			get;
			set;
		}
		public DateTime UpDateTime
		{
			get;
			set;
		}
		[IgnoreDataMember]
		public DateTime Date
		{
			get;
			set;
		}
		[DataMember]
		public DateTimeOffset LocalObservationDateTime
		{
			get;
			set;
		}
		[DataMember]
		public long? EpochTime
		{
			get;
			set;
		}
		[DataMember]
		public string WeatherText
		{
			get;
			set;
		}
		[DataMember]
		public int? WeatherIcon
		{
			get;
			set;
		}
		[DataMember]
		public bool IsDayTime
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Temperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements RealFeelTemperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements RealFeelTemperatureShade
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? RelativeHumidity
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements DewPoint
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Wind Wind
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Wind WindGust
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? UVIndex
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public string UVIndexText
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements Visibility
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public string ObstructionsToVisibility
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? CloudCover
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements Ceiling
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements Pressure
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public PressureTendency PressureTendency
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements Past24HourTemperatureDeparture
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements ApparentTemperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements WindChillTemperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurements Precip1hr
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public string MobileLink
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public string Link
		{
			get;
			set;
		}
	}
}

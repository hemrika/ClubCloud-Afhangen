using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class DailyForecast
	{
		[DataMember]
		public DateTimeOffset Date
		{
			get;
			set;
		}
		[DataMember]
		public long? EpochDate
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Sun Sun
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Moon Moon
		{
			get;
			set;
		}
		[DataMember]
		public TemperatureRange Temperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public TemperatureRange RealFeelTemperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public TemperatureRange RealFeelTemperatureShade
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public float? HoursOfSun
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public List<AirQualityForecast> AirAndPollen
		{
			get;
			set;
		}
		[DataMember]
		public HalfDayForecast Day
		{
			get;
			set;
		}
		[DataMember]
		public HalfDayForecast Night
		{
			get;
			set;
		}
		[DataMember]
		public string MobileLink
		{
			get;
			set;
		}
		[DataMember]
		public string Link
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public List<string> Sources
		{
			get;
			set;
		}
	}
}

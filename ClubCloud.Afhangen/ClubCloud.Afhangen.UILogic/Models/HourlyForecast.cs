using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class HourlyForecast
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public DateTimeOffset DateTime
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public long? EpochDateTime
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? WeatherIcon
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public string IconPhrase
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement Temperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement RealFeelTemperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement WetBulbTemperature
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement DewPoint
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
		public int? RelativeHumidity
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement Visibility
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement Ceiling
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
		public int? PrecipitationProbability
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? ThunderstormProbability
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? RainProbability
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? SnowProbability
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? IceProbability
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement TotalLiquid
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement Rain
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement Snow
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Measurement Ice
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
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public List<string> Sources
		{
			get;
			set;
		}
	}
}

using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class HalfDayForecast
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public int? Icon
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
		public string ShortPhrase
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public string LongPhrase
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
		public float? HoursOfPrecipitation
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public float? HoursOfRain
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
	}
}

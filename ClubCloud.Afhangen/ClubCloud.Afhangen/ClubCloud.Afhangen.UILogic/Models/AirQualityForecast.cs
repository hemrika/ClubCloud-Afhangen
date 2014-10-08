using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class AirQualityForecast
	{
		[DataMember]
		public string Name
		{
			get;
			set;
		}
		[DataMember]
		public int? Value
		{
			get;
			set;
		}
		[DataMember]
		public string Category
		{
			get;
			set;
		}
		[DataMember]
		public int CategoryValue
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public string Type
		{
			get;
			set;
		}
	}
}

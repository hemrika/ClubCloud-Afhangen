using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class ForecastSummary
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
		[DataMember]
		public Headline Headline
		{
			get;
			set;
		}
		[DataMember]
		public List<DailyForecast> DailyForecasts
		{
			get;
			set;
		}
	}
}

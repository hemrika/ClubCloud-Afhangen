using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Records
	{
		[DataMember]
		public RecordsTemperatureSummary Temperatures
		{
			get;
			set;
		}
		[DataMember]
		public PrecipitationSummary Precipitation
		{
			get;
			set;
		}
		[DataMember]
		public PrecipitationSummary Snowfall
		{
			get;
			set;
		}
	}
}

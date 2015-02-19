using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Actuals
	{
		[DataMember]
		public TemperatureSummary Temperatures
		{
			get;
			set;
		}
		[DataMember]
		public DegreeDaySummary DegreeDays
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Precipitation
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Snowfall
		{
			get;
			set;
		}
		[DataMember]
		public Measurements SnowDepth
		{
			get;
			set;
		}
	}
}

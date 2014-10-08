using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Normals
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
	}
}

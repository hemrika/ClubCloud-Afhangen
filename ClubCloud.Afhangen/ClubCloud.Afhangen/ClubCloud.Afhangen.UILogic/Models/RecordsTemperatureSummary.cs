using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class RecordsTemperatureSummary
	{
		[DataMember]
		public TemporalMeasurements Maximum
		{
			get;
			set;
		}
		[DataMember]
		public TemporalMeasurements Minimum
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Average
		{
			get;
			set;
		}
	}
}

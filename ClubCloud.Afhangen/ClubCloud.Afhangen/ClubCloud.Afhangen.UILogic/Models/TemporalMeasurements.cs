using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class TemporalMeasurements : Measurements
	{
		[DataMember(Order = 3)]
		public int? Year
		{
			get;
			set;
		}
	}
}

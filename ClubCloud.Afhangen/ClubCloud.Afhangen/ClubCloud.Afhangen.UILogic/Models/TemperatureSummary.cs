using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class TemperatureSummary
	{
		[DataMember]
		public Measurements Maximum
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Minimum
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

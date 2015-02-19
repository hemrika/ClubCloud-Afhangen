using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class DegreeDaySummary
	{
		[DataMember]
		public Measurements Heating
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Cooling
		{
			get;
			set;
		}
	}
}

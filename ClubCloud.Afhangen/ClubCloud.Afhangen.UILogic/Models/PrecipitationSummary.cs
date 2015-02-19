using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class PrecipitationSummary
	{
		[DataMember]
		public TemporalMeasurements Maximum
		{
			get;
			set;
		}
	}
}

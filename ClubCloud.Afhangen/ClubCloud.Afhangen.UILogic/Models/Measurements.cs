using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Measurements
	{
		[DataMember]
		public Measurement Metric
		{
			get;
			set;
		}
		[DataMember]
		public Measurement Imperial
		{
			get;
			set;
		}
	}
}

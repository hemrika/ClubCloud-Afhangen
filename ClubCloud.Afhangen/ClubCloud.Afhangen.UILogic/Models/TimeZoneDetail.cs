using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class TimeZoneDetail
	{
		[DataMember]
		public string Code
		{
			get;
			set;
		}
		[DataMember]
		public string Name
		{
			get;
			set;
		}
		[DataMember]
		public float GmtOffset
		{
			get;
			set;
		}
		[DataMember]
		public bool IsDaylightSavings
		{
			get;
			set;
		}
		[DataMember(Name = "NextOffsetChange")]
		public DateTime? NextDSTChangeUTC
		{
			get;
			set;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class DailyAlarm
	{
		[DataMember(Order = 1)]
		public DateTimeOffset Date
		{
			get;
			set;
		}
		[DataMember(Order = 1)]
		public long? EpochDate
		{
			get;
			set;
		}
		[DataMember(Order = 4)]
		public string MobileLink
		{
			get;
			set;
		}
		[DataMember(Order = 5)]
		public string Link
		{
			get;
			set;
		}
		[DataMember(Order = 3)]
		public List<Alarm> Alarms
		{
			get;
			set;
		}
	}
}

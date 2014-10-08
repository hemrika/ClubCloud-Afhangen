using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Alarm
	{
		[DataMember(Name = "AlarmType")]
		public string AlarmTypeName
		{
			get
			{
				return this.AlarmType.ToString();
			}
			set
			{
				AlarmType alarmType;
				if (Enum.TryParse<AlarmType>(value, out alarmType))
				{
					this.AlarmType = alarmType;
				}
			}
		}
		public AlarmType AlarmType
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Value
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Day
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Night
		{
			get;
			set;
		}
	}
}

using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class Advisory
	{
		public string ID
		{
			get;
			set;
		}
		[IgnoreDataMember]
		public DateTime DateIssued
		{
			get;
			set;
		}
		public DateTimeOffset DateTimeIssued
		{
			get;
			set;
		}
		public long? EpochDateTimeIssued
		{
			get;
			set;
		}
	}
}

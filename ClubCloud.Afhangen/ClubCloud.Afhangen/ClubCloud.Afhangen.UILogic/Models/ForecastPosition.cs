using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class ForecastPosition : StormPosition
	{
		[IgnoreDataMember]
		public DateTime Created
		{
			get;
			set;
		}
		public DateTimeOffset DateCreated
		{
			get;
			set;
		}
		public long? EpochDateCreated
		{
			get;
			set;
		}
		[IgnoreDataMember]
		public DateTime ForecastDate
		{
			get;
			set;
		}
		public DateTimeOffset ForecastDateTime
		{
			get;
			set;
		}
		public long? ForecastEpochDateTime
		{
			get;
			set;
		}
	}
}

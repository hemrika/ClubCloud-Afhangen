using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class DailyRecords
	{
		public DateTimeOffset Date
		{
			get;
			set;
		}
		public long? EpochDate
		{
			get;
			set;
		}
		public Records Records
		{
			get;
			set;
		}
	}
}

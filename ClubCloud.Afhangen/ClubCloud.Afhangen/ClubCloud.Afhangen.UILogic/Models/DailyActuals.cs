using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class DailyActuals
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
		public Actuals Actuals
		{
			get;
			set;
		}
	}
}

using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class DailyNormals
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
		public Normals Normals
		{
			get;
			set;
		}
	}
}

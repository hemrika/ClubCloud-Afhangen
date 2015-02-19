using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class Storm
	{
		public int Year
		{
			get;
			set;
		}
		public string BasinID
		{
			get;
			set;
		}
		public int DepressionNumber
		{
			get;
			set;
		}
		public string Name
		{
			get;
			set;
		}
		public bool IsActive
		{
			get;
			set;
		}
		public bool IsSubtropical
		{
			get;
			set;
		}
		public int? AccuID
		{
			get;
			set;
		}
		public int? HurdatID
		{
			get;
			set;
		}
		public int? AtcfID
		{
			get;
			set;
		}
	}
}

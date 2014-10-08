using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class IndexBase
	{
		[DataMember(Order = 1)]
		public string Name
		{
			get;
			set;
		}
		[DataMember(Order = 2)]
		public int? ID
		{
			get;
			set;
		}
		[DataMember(Order = 3)]
		public bool Ascending
		{
			get;
			set;
		}
	}
}

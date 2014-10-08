using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class IndexMetadata : IndexBase
	{
		[DataMember(Order = 4)]
		public string Description
		{
			get;
			set;
		}
	}
}

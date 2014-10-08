using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class SupplementalArea
	{
		[DataMember]
		public int? Level
		{
			get;
			set;
		}
		[DataMember]
		public string LocalizedName
		{
			get;
			set;
		}
		[DataMember]
		public string EnglishName
		{
			get;
			set;
		}
	}
}

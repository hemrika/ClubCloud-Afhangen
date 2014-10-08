using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class AdminArea : Area
	{
		[DataMember]
		public int? Level
		{
			get;
			set;
		}
		[DataMember]
		public string LocalizedType
		{
			get;
			set;
		}
		[DataMember]
		public string EnglishType
		{
			get;
			set;
		}
		[DataMember]
		public string CountryID
		{
			get;
			set;
		}
	}
}

using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class PressureTendency
	{
		[DataMember]
		public string LocalizedText
		{
			get;
			set;
		}
		[DataMember]
		public string Code
		{
			get;
			set;
		}
	}
}

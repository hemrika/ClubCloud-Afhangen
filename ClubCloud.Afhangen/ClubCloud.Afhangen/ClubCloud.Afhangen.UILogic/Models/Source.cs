using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Source
	{
		[DataMember]
		public string DataType
		{
			get;
			set;
		}
		[DataMember]
		public string Name
		{
			get;
			set;
		}
	}
}

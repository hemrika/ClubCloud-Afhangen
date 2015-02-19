using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Headline
	{
		[DataMember]
		public DateTimeOffset EffectiveDate
		{
			get;
			set;
		}
		[DataMember]
		public long? EffectiveEpochDate
		{
			get;
			set;
		}
		[DataMember]
		public int Severity
		{
			get;
			set;
		}
		[DataMember]
		public string Text
		{
			get;
			set;
		}
	}
}

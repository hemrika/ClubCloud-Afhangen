using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Moon
	{
		[DataMember]
		public DateTimeOffset? Rise
		{
			get;
			set;
		}
		[DataMember]
		public long? EpochRise
		{
			get;
			set;
		}
		[DataMember]
		public DateTimeOffset? Set
		{
			get;
			set;
		}
		[DataMember]
		public long? EpochSet
		{
			get;
			set;
		}
		[DataMember]
		public string Phase
		{
			get;
			set;
		}
		[DataMember]
		public int? Age
		{
			get;
			set;
		}
	}
}

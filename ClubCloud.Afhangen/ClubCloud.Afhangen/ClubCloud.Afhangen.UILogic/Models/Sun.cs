using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Sun
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
	}
}

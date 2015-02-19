
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Wind
	{
		[DataMember]
		public Measurements Speed
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Direction Direction
		{
			get;
			set;
		}
	}
}


/*
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Wind
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public Direction Direction
		{
			get;
			set;
		}
		[DataMember]
		public Measurements Speed
		{
			get;
			set;
		}
	}
}
*/
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class Index
	{
		[DataMember]
		public string Name
		{
			get;
			set;
		}
		[DataMember]
		public int? ID
		{
			get;
			set;
		}
		[DataMember]
		public bool Ascending
		{
			get;
			set;
		}
		[DataMember]
		public DateTimeOffset LocalDateTime
		{
			get;
			set;
		}
		[DataMember]
		public long? EpochDateTime
		{
			get;
			set;
		}
		[DataMember]
		public float? Value
		{
			get;
			set;
		}
		[DataMember]
		public string Category
		{
			get;
			set;
		}
		[DataMember]
		public int CategoryValue
		{
			get;
			set;
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore), DataMember]
		public string Text
		{
			get;
			set;
		}
		[DataMember]
		public string MobileLink
		{
			get;
			set;
		}
		[DataMember]
		public string Link
		{
			get;
			set;
		}
		[IgnoreDataMember]
		public DateTime LocalDate
		{
			get;
			set;
		}
	}
}

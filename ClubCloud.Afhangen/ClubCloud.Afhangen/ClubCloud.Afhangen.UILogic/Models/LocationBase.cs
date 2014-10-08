using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class LocationBase
	{
		[DataMember]
		public int Version
		{
			get;
			set;
		}
		[DataMember]
		public string Key
		{
			get;
			set;
		}
		[DataMember]
		public string Type
		{
			get;
			set;
		}
		[DataMember]
		public int Rank
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
		public Area Country
		{
			get;
			set;
		}
		[DataMember]
		public AdminArea AdministrativeArea
		{
			get;
			set;
		}
		public override string ToString()
		{
			return string.Format("{0}|{1}, {2}, {3}", new object[]
			{
				this.Key,
				this.LocalizedName,
				(this.AdministrativeArea != null) ? this.AdministrativeArea.LocalizedName : string.Empty,
				(this.Country != null) ? this.Country.LocalizedName : string.Empty
			});
		}
	}
}

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract, KnownType(typeof(LocationDetail)), XmlInclude(typeof(LocationDetail))]
	public class Location : LocationBase
	{
		[DataMember]
		public string EnglishName
		{
			get;
			set;
		}
		[DataMember]
		public Area Region
		{
			get;
			set;
		}
		[DataMember]
		public List<SupplementalArea> SupplementalAdminAreas
		{
			get;
			set;
		}
		public string EnglishDetails
		{
			get;
			set;
		}
		public string LocalizedDetails
		{
			get;
			set;
		}
		[DataMember]
		public TimeZoneDetail TimeZone
		{
			get;
			set;
		}
		[DataMember]
		public bool IsAlias
		{
			get;
			set;
		}
		[DataMember]
		public Position GeoPosition
		{
			get;
			set;
		}
		[DataMember]
		public string PrimaryPostalCode
		{
			get;
			set;
		}
	}
}

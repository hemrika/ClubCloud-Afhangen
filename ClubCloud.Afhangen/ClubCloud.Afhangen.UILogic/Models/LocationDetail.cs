using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class LocationDetail : Location
	{
		[DataMember(Order = 20)]
		public AccuDetail Details
		{
			get;
			set;
		}
		public LocationDetail()
		{
		}
		public LocationDetail(Location location)
		{
			base.SupplementalAdminAreas = location.SupplementalAdminAreas;
			base.LocalizedDetails = location.LocalizedDetails;
			base.AdministrativeArea = location.AdministrativeArea;
			base.Country = location.Country;
			base.EnglishName = location.EnglishName;
			base.GeoPosition = location.GeoPosition;
			base.IsAlias = location.IsAlias;
			base.Key = location.Key;
			base.LocalizedName = location.LocalizedName;
			base.PrimaryPostalCode = location.PrimaryPostalCode;
			base.Region = location.Region;
			base.TimeZone = location.TimeZone;
			base.Type = location.Type;
			base.Version = location.Version;
		}
	}
}

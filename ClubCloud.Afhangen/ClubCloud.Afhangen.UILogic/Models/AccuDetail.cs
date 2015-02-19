using System.Collections.Generic;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class AccuDetail
	{
		[DataMember]
		public string Key
		{
			get;
			set;
		}
		[DataMember]
		public string BandMap
		{
			get;
			set;
		}
		[DataMember]
		public string Climo
		{
			get;
			set;
		}
		[DataMember]
		public string LocalRadar
		{
			get;
			set;
		}
		[DataMember]
		public string MediaRegion
		{
			get;
			set;
		}
		[DataMember]
		public string Metar
		{
			get;
			set;
		}
		[DataMember]
		public string NXMetro
		{
			get;
			set;
		}
		[DataMember]
		public string NXState
		{
			get;
			set;
		}
		[DataMember]
		public long? Population
		{
			get;
			set;
		}
		[DataMember]
		public string PrimaryWarningCountyCode
		{
			get;
			set;
		}
		[DataMember]
		public string PrimaryWarningZoneCode
		{
			get;
			set;
		}
		[DataMember]
		public string Satellite
		{
			get;
			set;
		}
		[DataMember]
		public string StationCode
		{
			get;
			set;
		}
		[DataMember]
		public string Synoptic
		{
			get;
			set;
		}
		[DataMember]
		public float? StationGmtOffset
		{
			get;
			set;
		}
		[DataMember]
		public string MarineStation
		{
			get;
			set;
		}
		[DataMember]
		public float? MarineStationGMTOffset
		{
			get;
			set;
		}
		[DataMember]
		public string VideoCode
		{
			get;
			set;
		}
		[DataMember]
		public Area DMA
		{
			get;
			set;
		}
		[DataMember]
		public List<Source> Sources
		{
			get;
			set;
		}
	}
}

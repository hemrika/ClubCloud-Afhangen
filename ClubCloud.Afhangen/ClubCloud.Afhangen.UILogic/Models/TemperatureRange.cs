using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class TemperatureRange
	{
		[DataMember]
		public Measurement Minimum
		{
			get;
			set;
		}
		[DataMember]
		public Measurement Maximum
		{
			get;
			set;
		}
	}
}

/*
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
    public class TemperatureRange
	{
        [DataMember]
        public Measurements Minimum
		{
			get;
			set;
		}
		[DataMember]
        public Measurements Maximum
		{
			get;
			set;
		}
	}
}
*/
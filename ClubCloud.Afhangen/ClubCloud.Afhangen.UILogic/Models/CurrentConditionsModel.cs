using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class CurrentConditionsModel
	{
		public DateTime UpdatedDateTime
		{
			get;
			set;
		}
		public string UpdateTime
		{
			get;
			set;
		}
		public string ShortPhrase
		{
			get;
			set;
		}

        public int WeatherCode
        {
            get;
            set;
        }

        public byte[] WeatherIcon
        {
            get;
            set;
        }

		public string Temperature
		{
			get;
			set;
		}
		public string TemperatureUnit
		{
			get;
			set;
		}
		public string ApparentTemperature
		{
			get;
			set;
		}
        public string ApparentTemperatureUnit
		{
			get;
			set;
		}
		public string WindDirection
		{
			get;
			set;
		}
		public string WindSpeed
		{
			get;
			set;
		}
		public string WindSpeedUnit
		{
			get;
			set;
		}
		public string GustSpeed
		{
			get;
			set;
		}
		public string GustSpeedUnit
		{
			get;
			set;
		}
		public string Humidity
		{
			get;
			set;
		}
		public string Pressure
		{
			get;
			set;
		}
		public string PressureUnit
		{
			get;
			set;
		}
		public string PressureTendency
		{
			get;
			set;
		}
		public string Visibility
		{
			get;
			set;
		}
		public string VisibilityUnit
		{
			get;
			set;
		}
		public string CloudCover
		{
			get;
			set;
		}
		public string UV
		{
			get;
			set;
		}

		public bool IsDayTime
		{
			get;
			set;
		}

        public int? UVIndex { get; set; }

        public byte[] UVIcon { get; set; }

        public string Precip1hr { get; set; }

        public string Precip1hrUnit { get; set; }

        public string RelativeHumidity { get; set; }

        public string WindDegree { get; set; }
    }
}

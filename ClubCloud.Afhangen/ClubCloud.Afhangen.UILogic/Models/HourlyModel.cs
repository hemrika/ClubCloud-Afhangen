using ClubCloud.Afhangen.UILogic.Models.Entities;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class HourlyModel
	{
		public DateTime UpdatedDateTime
		{
			get;
			set;
		}
		public DateTime Date
		{
			get;
			set;
		}
		public string ShortTime
		{
			get;
			set;
		}
		public string ShortTimeQualifier
		{
			get;
			set;
		}
		public string ShortPhrase
		{
			get;
			set;
		}
		public string PredictedTemperature
		{
			get;
			set;
		}
		public string PredictedTemperatureUnit
		{
			get;
			set;
		}
		public string PredictedRealFeel
		{
			get;
			set;
		}
		public string PredictedRealFeelUnit
		{
			get;
			set;
		}
		public int WeatherCode
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
		public string ProbabilityOfPrecipation
		{
			get;
			set;
		}
		public string PrecipationAmountRain
		{
			get;
			set;
		}
		public string PrecipationAmountRainUnits
		{
			get;
			set;
		}
		public string PrecipationAmountSnow
		{
			get;
			set;
		}
		public string PrecipationAmountSnowUnits
		{
			get;
			set;
		}
		public string PrecipationAmountIce
		{
			get;
			set;
		}
		public string PrecipationAmountIceUnits
		{
			get;
			set;
		}
		public string PrecipationAmountThunderstorms
		{
			get;
			set;
		}
		public string PrecipationAmountThunderstormsUnits
		{
			get;
			set;
		}
		public string Humidity
		{
			get;
			set;
		}
		public string DewPoint
		{
			get;
			set;
		}
		public List<WeatherAlarmType> Alarms
		{
			get;
			set;
		}
		public bool HasWeatherAlarm
		{
			get;
			set;
		}

        public int UVIndex { get; set; }

        public ImageSource UVImage
        {
            get
            {
                if (UVIndex != null)
                {
                    Uri image = null;
                    if (UVIndex < 10)
                        image = new Uri(string.Format("ms-appx:///Assets/Weather/UV/0{0}.png", UVIndex));
                    else
                        image = new Uri(string.Format("ms-appx:///Assets/Weather/UV/{0}.png", UVIndex));

                    if (image != null)
                        return new BitmapImage(image);
                }
                return null;
            }
        }

        public ImageSource WeatherImage
        {
            get
            {
                if (WeatherCode != null)
                {
                    Uri image = null;
                    if (WeatherCode < 10)
                        image = new Uri(string.Format("ms-appx:///Assets/Weather/Icon/0{0}.png", WeatherCode));
                    else
                        image = new Uri(string.Format("ms-appx:///Assets/Weather/Icon/{0}.png", WeatherCode));
                    if (image != null)
                        return new BitmapImage(image);
                }
                return null;
            }
        }
    }
}

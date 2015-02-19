using ClubCloud.Afhangen.UILogic.Models.Entities;
using System;
using System.Collections.Generic;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class HalfDayForecastModel
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
		public string ShortDate
		{
			get;
			set;
		}
		public string LongDate
		{
			get;
			set;
		}
		public string ShortPhrase
		{
			get;
			set;
		}
		public int PredictedHi
		{
			get;
			set;
		}
		public string PredictedHiUnit
		{
			get;
			set;
		}
		public int PredictedLow
		{
			get;
			set;
		}
		public string PredictedLowUnit
		{
			get;
			set;
		}
		public int PredictedRealFeelLow
		{
			get;
			set;
		}
		public string PredictedRealFeelLowUnit
		{
			get;
			set;
		}
		public int PredictedRealFeelHi
		{
			get;
			set;
		}
		public string PredictedRealFeelHiUnit
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
		public int WindSpeed
		{
			get;
			set;
		}
		public string WindSpeedUnit
		{
			get;
			set;
		}
		public int GustSpeed
		{
			get;
			set;
		}
		public string GustSpeedUnit
		{
			get;
			set;
		}
		public HalfDayForecastType ForecastType
		{
			get;
			set;
		}
		public string ProbabilityOfPrecipitation
		{
			get;
			set;
		}
		public string ProbabilityOfPrecipitationSub
		{
			get;
			set;
		}
		public double PrecipationAmountRain
		{
			get;
			set;
		}
		public string PrecipationAmountRainUnits
		{
			get;
			set;
		}
		public double PrecipationAmountSnow
		{
			get;
			set;
		}
		public string PrecipationAmountSnowUnits
		{
			get;
			set;
		}
		public double PrecipationAmountIce
		{
			get;
			set;
		}
		public string PrecipationAmountIceUnits
		{
			get;
			set;
		}
		public int PrecipationAmountThunderstorms
		{
			get;
			set;
		}
		public bool HasWeatherAlarm
		{
			get;
			set;
		}
		public bool HasWeatherAlarmWind
		{
			get;
			set;
		}
		public bool HasWeatherAlarmNonWind
		{
			get;
			set;
		}
		public List<WeatherAlarmType> Alarms
		{
			get;
			set;
		}
		public string UV
		{
			get;
			set;
		}
		public string MoonPhase
		{
			get;
			set;
		}
		public string MoonPhaseEnglish
		{
			get;
			set;
		}
		public string Sunrise
		{
			get;
			set;
		}
		public string Sunset
		{
			get;
			set;
		}
		public string Moonrise
		{
			get;
			set;
		}
		public string Moonset
		{
			get;
			set;
		}
		public bool IsAttributed
		{
			get;
			set;
		}
		public string LocalForecastSummary
		{
			get;
			set;
		}
		public int LocalForecastSummarySeverity
		{
			get;
			set;
		}
		public DateTimeOffset LocalForecastSummaryEffectiveDate
		{
			get;
			set;
		}
		public bool IsAdvertisement
		{
			get;
			set;
		}
		public HalfDayForecastModel()
		{
			this.IsAttributed = false;
		}

        public HalfDayForecastModel(DateTime date, int predictedHi, int predictedLow, int weatherCode, HalfDayForecastType forecastType)
		{
			this.ShortDate = date.ToString("ddd. M/d");
			this.LongDate = date.ToString("dddd, MMMM d");
			this.PredictedHi = predictedHi;
			this.PredictedLow = predictedLow;
			this.WeatherCode = weatherCode;
			this.ForecastType = forecastType;
			this.LocalForecastSummary = "";
		}
	}
}

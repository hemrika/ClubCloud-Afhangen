using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class ForecastModel
	{
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
		public string PredictedTemps
		{
			get;
			set;
		}
		public int WeatherCode
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
		public bool HasWeatherAlarms
		{
			get;
			set;
		}
		public ForecastModel()
		{
			this.Date = default(DateTime);
			this.PredictedTemps = "";
			this.WeatherCode = 1;
			this.MoonPhase = "Full";
		}
		public ForecastModel(DateTime date, string predictedHI, string predictedLow, int weatherCode, string moonPhase = "Full")
		{
			this.Date = date;
			this.ShortDate = date.ToString("M/d");
			this.LongDate = date.ToString("dddd, MMMM d");
			this.PredictedTemps = ((predictedHI == "" && predictedLow == "") ? "" : (predictedHI + "/" + predictedLow));
			this.WeatherCode = weatherCode;
			this.MoonPhase = moonPhase;
		}
	}
}

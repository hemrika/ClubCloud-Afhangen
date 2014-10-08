using ClubCloud.Afhangen.UILogic.Models.Entities;
using System;
using System.Globalization;
using Windows.Globalization;

namespace ClubCloud.Afhangen.UILogic.Models
{
	public class SettingsModel
	{
		public bool IsFirstLaunch
		{
			get;
			set;
		}
		public DateTime FirstLaunch
		{
			get;
			set;
		}
		public bool HasUserBeenPromptedForPush
		{
			get;
			set;
		}
		public DateTime LastRatingsPromptDate
		{
			get;
			set;
		}
		public string UserId
		{
			get;
			set;
		}
		public LanguageModel Language
		{
			get;
			set;
		}
		public string PartnerCode
		{
			get;
			set;
		}
		public bool IsEulaAgreed
		{
			get;
			set;
		}
		public UnitsType UnitType
		{
			get;
			set;
		}
		public ForecastType PreferredForecastType
		{
			get;
			set;
		}
		public int PreferredLifestyleIndex
		{
			get;
			set;
		}
		public bool IsWeatherAnimationsOn
		{
			get;
			set;
		}
		public DataDensityType DataDensity
		{
			get;
			set;
		}
		public bool IsToastEnabled
		{
			get;
			set;
		}
		public bool IsGpsEnabled
		{
			get;
			set;
		}
		public double MapTileOpacity
		{
			get;
			set;
		}
		public SettingsModel()
		{
			this.IsFirstLaunch = true;
			this.FirstLaunch = DateTime.Now;
			this.HasUserBeenPromptedForPush = false;
			this.LastRatingsPromptDate = DateTime.Now;
			this.UserId = Guid.NewGuid().ToString();
			this.Language = new LanguageModel();
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			this.Language.Id = SettingsModel.GetLanguageIdFromIsoCode(currentCulture, false);
			this.Language.IsoCode = SettingsModel.GetIsoCodeFromLanguageId(this.Language.Id);
			this.PartnerCode = "windows8-v3";
			this.IsEulaAgreed = false;
			this.PreferredForecastType = ForecastType.Daily;
			this.PreferredLifestyleIndex = 100;
			this.IsWeatherAnimationsOn = true;
			this.DataDensity = DataDensityType.High;
			this.IsToastEnabled = false;
			this.IsGpsEnabled = false;
			this.MapTileOpacity = 0.6;
			GeographicRegion geographicRegion = new GeographicRegion();
			string codeTwoLetter = geographicRegion.CodeTwoLetter;
			this.UnitType = ((codeTwoLetter.ToLower() == "us") ? UnitsType.Imperial : UnitsType.Metric);
		}
		public static string GetIsoCodeFromLanguageId(int languageId)
		{
			switch (languageId)
			{
			case 1:
				return "en-us";
			case 2:
				return "es";
			case 3:
				return "fr";
			case 4:
				return "da";
			case 5:
				return "pt";
			case 6:
				return "nl";
			case 7:
				return "no";
			case 8:
				return "it";
			case 9:
				return "de";
			case 10:
				return "sv";
			case 11:
				return "fi";
			case 12:
				return "zh-hk";
			case 13:
				return "zh-cn";
			case 14:
				return "zh-tw";
			case 15:
				return "es-ar";
			case 16:
				return "es-mx";
			case 17:
				return "sk";
			case 18:
				return "ro";
			case 19:
				return "cs";
			case 20:
				return "hu";
			case 21:
				return "pl";
			case 22:
				return "ca";
			case 23:
				return "pt-br";
			case 24:
				return "hi";
			case 25:
				return "ru";
			case 26:
				return "ar";
			case 27:
				return "el";
			case 28:
				return "en-gb";
			case 29:
				return "ja";
			case 30:
				return "ko";
			case 31:
				return "tr";
			case 32:
				return "fr-ca";
			case 33:
				return "he";
			case 34:
				return "sl";
			case 35:
				return "uk";
			case 36:
				return "id";
			default:
				return "en-us";
			}
		}
		public static int GetLanguageIdFromIsoCode(CultureInfo cultureInfo, bool useTwoLetterIsoCode = false)
		{
			string text = useTwoLetterIsoCode ? cultureInfo.TwoLetterISOLanguageName : cultureInfo.Name;
			string key;
			switch (key = text.ToLower())
			{
			case "en":
				return 1;
			case "en-us":
				return 1;
			case "es":
				return 2;
			case "fr":
				return 3;
			case "da":
				return 4;
			case "pt":
				return 5;
			case "nl":
				return 6;
			case "nb":
			case "nn":
			case "no":
				return 7;
			case "it":
				return 8;
			case "de":
				return 9;
			case "sv":
				return 10;
			case "fi":
				return 11;
			case "zh":
				return 13;
			case "zh-hk":
				return 12;
			case "zh-cn":
				return 13;
			case "zh-tw":
				return 14;
			case "es-ar":
				return 15;
			case "es-mx":
				return 16;
			case "sk":
				return 17;
			case "ro":
				return 18;
			case "cs":
				return 19;
			case "hu":
				return 20;
			case "pl":
				return 21;
			case "ca":
				return 22;
			case "pt-br":
				return 23;
			case "hi":
				return 24;
			case "ru":
				return 25;
			case "ar":
				return 26;
			case "el":
				return 27;
			case "en-gb":
				return 28;
			case "ja":
				return 29;
			case "ko":
				return 30;
			case "tr":
				return 31;
			case "fr-ca":
				return 32;
			case "he":
				return 33;
			case "sl":
				return 34;
			case "uk":
				return 35;
			case "id":
				return 36;
			}
			if (cultureInfo.Name.ToLower().Contains("-"))
			{
				return SettingsModel.GetLanguageIdFromIsoCode(cultureInfo, true);
			}
			return 1;
		}
	}
}

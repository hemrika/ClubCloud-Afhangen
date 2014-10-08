using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ClubCloud.Afhangen.UILogic.Services
{
	public class WeatherServiceProxy : ClubCloud.Afhangen.UILogic.Services.IWeatherService
	{
        private readonly string apiKey = "6915cc0fac554dc79682c8750666ebde";
        private readonly string apiUriRoot = "http://api.accuweather.com/";
        //private readonly string apiVersion = "v1";
        //private readonly string languageCode = "nl-nl";

		public async Task<ForecastSummary> GetForecastSummary(string locationKey, DailyForecastQuery dailyForecastQuery = DailyForecastQuery.OneDay, bool? metric = null)
		{
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
            Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"forecasts/",
				asp.ApiVersion,
				"/daily/",
				LabelAttribute.GetEnumLabelValue(dailyForecastQuery),
				"/",
				locationKey,
				".json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true&metric=",
				asp.ValidateMetric(metric)
			}));

			return await asp.MakeWebRequest<ForecastSummary>(requestUri);
		}

		public Task<List<HourlyForecast>> GetHourlyForecasts(string locationKey, HourlyForecastQuery hourlyForecastQuery = HourlyForecastQuery.OneDay, bool? metric = null)
		{
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
			Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"forecasts/",
				asp.ApiVersion,
				"/hourly/",
                LabelAttribute.GetEnumLabelValue(hourlyForecastQuery),
				"/",
				locationKey,
				".json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true&metric=",
				asp.ValidateMetric(metric)
			}));
			return asp.MakeWebRequest<List<HourlyForecast>>(requestUri);
		}

		public async Task<CurrentConditions> GetCurrentConditions(string locationKey)
		{
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);

			Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"currentconditions/",
				asp.ApiVersion,
				"/",
				locationKey,
				".json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true"
			}));
			List<CurrentConditions> list = await asp.MakeWebRequest<List<CurrentConditions>>(requestUri);
			return list[0];
		}

		public async Task<List<Alert>> GetAlertsDetails(string locationKey)
		{
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);

			Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"alerts/",
				asp.ApiVersion,
				"/",
				locationKey,
				".json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true"
			}));
			return await asp.MakeWebRequest<List<Alert>>(requestUri);
		}

		public async Task<List<Index>> GetIndices(string locationKey, int indexGroup, DailyForecastQuery dailyForecastQuery = DailyForecastQuery.FiveDay)
		{
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
			Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new object[]
			{
				"indices/",
				asp.ApiVersion,
				"/daily/",
                LabelAttribute.GetEnumLabelValue(dailyForecastQuery),
				"/",
				locationKey,
				"/groups/",
				indexGroup,
				".json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true"
			}));
			return await asp.MakeWebRequest<List<Index>>(requestUri);
		}

		public async Task<List<IndexGroupMetadata>> GetIndexGroupMetadata(string locationKey)
		{
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
			Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"indices/",
				asp.ApiVersion,
				"/daily/groups.json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode)
			}));
			return await asp.MakeWebRequest<List<IndexGroupMetadata>>(requestUri);
		}
	}
}

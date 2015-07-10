using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Models.Entities;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism.Interfaces;
using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private const Int32 UVIndex = 5;

        private Double _weatherAlarmThresholdIce;

        private Double _weatherAlarmThresholdSnow;

        private Double _weatherAlarmThresholdRain;

        private Double _weatherAlarmThresholdWindSpeed;

        private Double _weatherAlarmThresholdWindGusts;

        private Int32 _weatherAlarmThresholdThunderstorm;

        private IResourceLoader _resourceLoader;

        private IWeatherService _weatherServiceProvider;

        private ForecastSummary _forecastSummary;

        private CurrentConditions _currentConditions;

        public WeatherRepository(IWeatherService weatherServiceProvider, IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
            _weatherServiceProvider = weatherServiceProvider; //new WeatherServiceProvider("6915cc0fac554dc79682c8750666ebde", "http://api.accuweather.com/", "v1", "en-us");
            _forecastSummary = null;
        }

        public async Task<CurrentConditionsModel> GetCurrentConditionsAsync(String locationId, Boolean forceUpdate = false, Boolean getDetails = true, WeatherUnitTypes metric = WeatherUnitTypes.Metric)
        {
            CurrentConditionsModel currentConditionsModel;

            if (WeatherRepository.IsNetworkAvailable())
            {
                if (this._currentConditions != null && !forceUpdate && (this._currentConditions.UpDateTime.AddMinutes(15) < DateTime.UtcNow || this._currentConditions.LocationId != locationId))
                {
                    this._currentConditions = null;
                }
                if (this._currentConditions == null || forceUpdate)
                {
                    this._currentConditions = await this._weatherServiceProvider.GetCurrentConditions(locationId);
                    this._currentConditions.UpDateTime = DateTime.UtcNow;
                    this._currentConditions.LocationId = locationId;
                }

                CurrentConditionsModel weatherText = new CurrentConditionsModel();

                if (this._currentConditions != null)
                {
                    try
                    {
                        //_currentConditions.
                        weatherText.ShortPhrase = this._currentConditions.WeatherText;

                        if (this._currentConditions.WeatherIcon.HasValue)
                        {
                            weatherText.WeatherCode = this._currentConditions.WeatherIcon.Value;

                            try
                            {
                                Uri WeatherIconUri = new Uri(string.Format("ms-appx:///Assets/Weather/Icon/{0}.png", weatherText.WeatherCode.ToString("D2")));
                                StorageFile _storageFile = await StorageFile.GetFileFromApplicationUriAsync(WeatherIconUri);

                                IBuffer readbuffer = await FileIO.ReadBufferAsync(_storageFile);
                                weatherText.WeatherIcon = readbuffer.ToArray();
                            }
                            catch (Exception ex)
                            {
                                string message = ex.Message;
                            }

                        }

                        if(this._currentConditions.UVIndex.HasValue)
                        {
                            weatherText.UV = this._currentConditions.UVIndexText;
                            weatherText.UVIndex = this._currentConditions.UVIndex;
                            try
                            {
                                Uri WeatherIconUri = new Uri(string.Format("ms-appx:///Assets/Weather/UV/{0}.png", weatherText.UVIndex.Value.ToString("D2")));
                                StorageFile _storageFile = await StorageFile.GetFileFromApplicationUriAsync(WeatherIconUri);
                                IBuffer readbuffer = await FileIO.ReadBufferAsync(_storageFile);
                                weatherText.UVIcon = readbuffer.ToArray();
                            }
                            catch (Exception ex)
                            {
                                string message = ex.Message;
                            }

                        }
                        
                        Double num = WeatherRepository.MathRound(this._currentConditions.Temperature.Metric.NumericValue);
                        weatherText.Temperature = num.ToString();
                        weatherText.TemperatureUnit = "\u00B0C" + this._currentConditions.Temperature.Metric.Unit;

                        if (getDetails)
                        {
                            if (this._currentConditions.CloudCover.HasValue)
                            {
                                weatherText.CloudCover = String.Concat(this._currentConditions.CloudCover, "%");
                            }

                            weatherText.PressureTendency = this._currentConditions.PressureTendency.Code;

                            if (this._currentConditions.RelativeHumidity.HasValue)
                            {
                                weatherText.Humidity = String.Concat(this._currentConditions.RelativeHumidity, "%");
                            }

                            weatherText.UV = this._currentConditions.UVIndexText;
                            
                            
                            weatherText.IsDayTime = this._currentConditions.IsDayTime;

                            Double num1 = WeatherRepository.MathRound(this._currentConditions.WindGust.Speed.Metric.NumericValue);//.Metric.NumericValue);
                            weatherText.GustSpeed = num1.ToString();
                            weatherText.GustSpeedUnit = this._currentConditions.WindGust.Speed.Metric.Unit;//.Metric.Unit;

                            Double? value1 = this._currentConditions.Pressure.Metric.Value;
                            weatherText.Pressure = value1.ToString();
                            weatherText.PressureUnit = this._currentConditions.Pressure.Metric.Unit;

                            Double num2 = WeatherRepository.MathRound(this._currentConditions.ApparentTemperature.Metric.NumericValue);
                            weatherText.ApparentTemperature = num2.ToString();
                            weatherText.ApparentTemperatureUnit = this._currentConditions.ApparentTemperature.Metric.Unit;

                            Double num3 = WeatherRepository.MathRound(this._currentConditions.Visibility.Metric.NumericValue);
                            weatherText.Visibility = num3.ToString();
                            weatherText.VisibilityUnit = this._currentConditions.Visibility.Metric.Unit;

                            Double num4 = WeatherRepository.MathRound(this._currentConditions.Temperature.Metric.NumericValue);
                            weatherText.Temperature = num4.ToString();
                            weatherText.TemperatureUnit = this._currentConditions.Temperature.Metric.Unit;

                            Double num5 = WeatherRepository.MathRound(this._currentConditions.Wind.Speed.Metric.NumericValue);
                            weatherText.WindSpeed = num5.ToString();
                            weatherText.WindSpeedUnit = this._currentConditions.Wind.Speed.Metric.Unit;
                            weatherText.UpdatedDateTime = this._currentConditions.UpDateTime;

                            Double num6 = WeatherRepository.MathRound(this._currentConditions.Precip1hr.Metric.NumericValue);
                            weatherText.Precip1hr = num6.ToString();
                            weatherText.Precip1hrUnit = this._currentConditions.Precip1hr.Metric.Unit;

                            Double num7 = WeatherRepository.MathRound(this._currentConditions.RelativeHumidity.Value);
                            weatherText.RelativeHumidity = num7.ToString();

                            Double num8 = WeatherRepository.MathRound(this._currentConditions.Wind.Direction.Degrees.Value);
                            weatherText.WindDegree = num8.ToString();
                            weatherText.WindDirection = this._currentConditions.Wind.Direction.Localized;
                        }
                    }
                    catch (Exception exception)
                    {
                        string message = exception.Message;
                        //Messenger.Default.Send<ErrorMessageTypeMessage>(new ErrorMessageTypeMessage()
                        //{
                        //    ErrorCode = ErrorCodeType.FailedToGetCurrentConditionsData
                        //});
                    }
                }
                currentConditionsModel = weatherText;
            }
            else
            {
                //Messenger.Default.Send<ErrorMessageTypeMessage>(new ErrorMessageTypeMessage()
                //{
                //    ErrorCode = ErrorCodeType.UnableToConnectNetwork
                //});
                currentConditionsModel = null;
            }
            return currentConditionsModel;
        }

        public Task<ObservableCollection<ForecastModel>> GetForecastsAsync(String locationId, WeatherUnitTypes metric = WeatherUnitTypes.Metric)
        {
            throw new NotImplementedException();
        }

        public async Task GetForecastSummaryAsync(String locationID, Boolean forceUpdate = false, WeatherUnitTypes metric = WeatherUnitTypes.Metric)
        {
            if (this._forecastSummary != null && !forceUpdate && (this._forecastSummary.LocationId != locationID || this._forecastSummary.UpDateTime.AddMinutes(15) < DateTime.UtcNow || this._forecastSummary.UpDateTime.DayOfWeek != DateTime.UtcNow.DayOfWeek))
            {
                this._forecastSummary = null;
            }
            if (this._forecastSummary == null || forceUpdate)
            {
                WeatherRepository weatherDataService = this;
                ForecastSummary forecastSummary = await this._weatherServiceProvider.GetForecastSummary(locationID, DailyForecastQuery.TwentyFiveDay, true);
                weatherDataService._forecastSummary = forecastSummary;
                if (this._forecastSummary != null)
                {
                    this._forecastSummary.LocationId = locationID;
                    this._forecastSummary.UpDateTime = DateTime.UtcNow;
                }
            }
        }

        public async Task<ObservableCollection<HalfDayForecastModel>> GetHalfDayForecastsAsync(String locationID, Boolean forceUpdate = false, Boolean getDetails = true, WeatherUnitTypes metric = WeatherUnitTypes.Metric)
        {
            ObservableCollection<HalfDayForecastModel> observableCollection;
            Boolean flag;
            List<WeatherAlarmType> weatherAlarmTypes;
            Boolean flag1;
            Boolean flag2;
            String str;
            Boolean flag3;
            List<WeatherAlarmType> weatherAlarmTypes1;
            Boolean flag4;
            Boolean flag5;
            if (getDetails)
            {
                //WeatherDataService._resourceLoader = ResourceLoader.GetForCurrentView("ClubCloud.Afhangen.UILogic.Services/Resources");
            }
            ObservableCollection<HalfDayForecastModel> text = new ObservableCollection<HalfDayForecastModel>();
            if (WeatherRepository.IsNetworkAvailable())
            {
                await this.GetForecastSummaryAsync(locationID, forceUpdate, metric);
                if (this._forecastSummary != null)
                {
                    this._forecastSummary.LocationId = locationID;
                    this._forecastSummary.UpDateTime = DateTime.UtcNow;
                    Int32 num = 0;
                    foreach (DailyForecast dailyForecast in this._forecastSummary.DailyForecasts)
                    {
                        HalfDayForecastModel halfDayForecastModel = new HalfDayForecastModel()
                        {
                            ShortPhrase = dailyForecast.Day.ShortPhrase
                        };
                        if (dailyForecast.Day.Icon.HasValue)
                        {
                            halfDayForecastModel.WeatherCode = dailyForecast.Day.Icon.Value;
                        }
                        if (getDetails)
                        {
                            halfDayForecastModel.ForecastType = HalfDayForecastType.Day;
                            halfDayForecastModel.Date = dailyForecast.Date.Date;
                            halfDayForecastModel.ShortDate = dailyForecast.Date.ToString("ddd. M/d");
                            halfDayForecastModel.LongDate = dailyForecast.Date.ToString("dddd, MMMM d");
                            halfDayForecastModel.PredictedHi = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.Temperature.Maximum.NumericValue));
                            halfDayForecastModel.PredictedHiUnit = dailyForecast.Temperature.Maximum.Unit;
                            halfDayForecastModel.PredictedLow = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.Temperature.Minimum.NumericValue));
                            halfDayForecastModel.PredictedLowUnit = dailyForecast.Temperature.Minimum.Unit;
                            halfDayForecastModel.PredictedRealFeelLow = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.RealFeelTemperature.Minimum.NumericValue));
                            halfDayForecastModel.PredictedRealFeelLowUnit = dailyForecast.RealFeelTemperature.Minimum.Unit;
                            halfDayForecastModel.PredictedRealFeelHi = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.RealFeelTemperature.Maximum.NumericValue));
                            halfDayForecastModel.PredictedRealFeelHiUnit = dailyForecast.RealFeelTemperature.Maximum.Unit;
                            halfDayForecastModel.WindDirection = dailyForecast.Day.Wind.Direction.Localized;
                            halfDayForecastModel.WindSpeed = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.Day.Wind.Speed.Metric.NumericValue));
                            halfDayForecastModel.WindSpeedUnit = dailyForecast.Day.Wind.Speed.Metric.Unit;
                            halfDayForecastModel.GustSpeed = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.Day.WindGust.Speed.Metric.NumericValue));
                            halfDayForecastModel.GustSpeedUnit = dailyForecast.Day.WindGust.Speed.Metric.Unit;
                            if (dailyForecast.Day.PrecipitationProbability.HasValue)
                            {
                                Int32? precipitationProbability = dailyForecast.Day.PrecipitationProbability;
                                halfDayForecastModel.ProbabilityOfPrecipitation = String.Concat(precipitationProbability.Value, "%");
                            }
                            if (!dailyForecast.Day.RainProbability.HasValue || (Single)dailyForecast.Day.RainProbability.Value <= 0f && (Single)dailyForecast.Day.SnowProbability.Value <= 0f)
                            {
                                halfDayForecastModel.ProbabilityOfPrecipitationSub = "";
                            }
                            else
                            {
                                halfDayForecastModel.ProbabilityOfPrecipitationSub = "(";
                                if ((Single)dailyForecast.Day.RainProbability.Value > 0f)
                                {
                                    HalfDayForecastModel halfDayForecastModel1 = halfDayForecastModel;
                                    Object probabilityOfPrecipitationSub = halfDayForecastModel1.ProbabilityOfPrecipitationSub;
                                    Object[] value = new Object[] { probabilityOfPrecipitationSub, "Regen", ": ", default(Object), default(Object) };
                                    Int32? rainProbability = dailyForecast.Day.RainProbability;
                                    value[3] = rainProbability.Value;
                                    value[4] = "%";
                                    halfDayForecastModel1.ProbabilityOfPrecipitationSub = String.Concat(value);
                                }
                                if ((Single)dailyForecast.Day.SnowProbability.Value > 0f)
                                {
                                    if (halfDayForecastModel.ProbabilityOfPrecipitationSub.Length > 1)
                                    {
                                        HalfDayForecastModel halfDayForecastModel2 = halfDayForecastModel;
                                        halfDayForecastModel2.ProbabilityOfPrecipitationSub = String.Concat(halfDayForecastModel2.ProbabilityOfPrecipitationSub, " ");
                                    }
                                    HalfDayForecastModel halfDayForecastModel3 = halfDayForecastModel;
                                    Object obj = halfDayForecastModel3.ProbabilityOfPrecipitationSub;
                                    Object[] objArray = new Object[] { obj, "Sneeuw", ": ", default(Object), default(Object) };
                                    Int32? snowProbability = dailyForecast.Day.SnowProbability;
                                    objArray[3] = snowProbability.Value;
                                    objArray[4] = "%";
                                    halfDayForecastModel3.ProbabilityOfPrecipitationSub = String.Concat(objArray);
                                }
                                HalfDayForecastModel halfDayForecastModel4 = halfDayForecastModel;
                                halfDayForecastModel4.ProbabilityOfPrecipitationSub = String.Concat(halfDayForecastModel4.ProbabilityOfPrecipitationSub, ")");
                            }
                            halfDayForecastModel.PrecipationAmountIce = Convert.ToDouble(dailyForecast.Day.Ice.NumericValue);
                            halfDayForecastModel.PrecipationAmountIceUnits = dailyForecast.Day.Ice.Unit;
                            halfDayForecastModel.PrecipationAmountRain = Convert.ToDouble(dailyForecast.Day.Rain.NumericValue);
                            halfDayForecastModel.PrecipationAmountRainUnits = dailyForecast.Day.Rain.Unit;
                            halfDayForecastModel.PrecipationAmountSnow = Convert.ToDouble(dailyForecast.Day.Snow.NumericValue);
                            halfDayForecastModel.PrecipationAmountSnowUnits = dailyForecast.Day.Snow.Unit;
                            Int32? thunderstormProbability = dailyForecast.Day.ThunderstormProbability;
                            halfDayForecastModel.PrecipationAmountThunderstorms = Convert.ToInt32(thunderstormProbability.Value);
                            halfDayForecastModel.UV = dailyForecast.AirAndPollen[5].Category;
                            if (dailyForecast.Sun != null)
                            {
                                if (dailyForecast.Sun.Rise.HasValue)
                                {
                                    DateTimeOffset? rise = dailyForecast.Sun.Rise;
                                    halfDayForecastModel.Sunrise = rise.Value.ToString("h:mm tt");
                                }
                                if (dailyForecast.Sun.Set.HasValue)
                                {
                                    DateTimeOffset? set = dailyForecast.Sun.Set;
                                    halfDayForecastModel.Sunset = set.Value.ToString("h:mm tt");
                                }
                            }
                            HalfDayForecastModel halfDayForecastModel5 = halfDayForecastModel;
                            if (dailyForecast.Sources.ToList<String>() == null)
                            {
                                flag3 = false;
                            }
                            else
                            {
                                List<String> sources = dailyForecast.Sources;
                                flag3 = false;
                                /*
                                if (WeatherDataService.CS$<>9__CachedAnonymousMethodDelegate16 == null)
                                {
                                    WeatherDataService.CS$<>9__CachedAnonymousMethodDelegate16 = new Func<String, Boolean>(null, (String x) => x.ToUpper() == "HUAFENG");
                                }
                                flag3 = sources.Any<String>(WeatherDataService.CS$<>9__CachedAnonymousMethodDelegate16);
                                */
                            }
                            halfDayForecastModel5.IsAttributed = flag3;
                            HalfDayForecastModel halfDayForecastModel6 = halfDayForecastModel;
                            weatherAlarmTypes1 = (num < 4 ? this.GetWeatherAlarms(halfDayForecastModel) : new List<WeatherAlarmType>());
                            halfDayForecastModel6.Alarms = weatherAlarmTypes1;
                            halfDayForecastModel.HasWeatherAlarm = halfDayForecastModel.Alarms.Any<WeatherAlarmType>();
                            HalfDayForecastModel halfDayForecastModel7 = halfDayForecastModel;
                            flag4 = (halfDayForecastModel.Alarms.Contains(WeatherAlarmType.Ice) || halfDayForecastModel.Alarms.Contains(WeatherAlarmType.Rain) || halfDayForecastModel.Alarms.Contains(WeatherAlarmType.Snow) ? true : halfDayForecastModel.Alarms.Contains(WeatherAlarmType.Thunderstorm));
                            halfDayForecastModel7.HasWeatherAlarmNonWind = flag4;
                            HalfDayForecastModel halfDayForecastModel8 = halfDayForecastModel;
                            flag5 = (halfDayForecastModel.Alarms.Contains(WeatherAlarmType.WindSpeed) ? true : halfDayForecastModel.Alarms.Contains(WeatherAlarmType.WindGust));
                            halfDayForecastModel8.HasWeatherAlarmWind = flag5;
                            halfDayForecastModel.UpdatedDateTime = DateTime.Now;
                        }
                        text.Add(halfDayForecastModel);
                        HalfDayForecastModel date = new HalfDayForecastModel()
                        {
                            ShortPhrase = dailyForecast.Night.ShortPhrase
                        };
                        if (dailyForecast.Night.Icon.HasValue)
                        {
                            date.WeatherCode = dailyForecast.Night.Icon.Value;
                        }
                        if (getDetails)
                        {
                            date.ForecastType = HalfDayForecastType.Night;
                            date.Date = dailyForecast.Date.Date;
                            date.ShortDate = dailyForecast.Date.ToString("ddd. M/d");
                            date.LongDate = dailyForecast.Date.ToString("dddd, MMMM d");
                            date.PredictedHi = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.Temperature.Maximum.NumericValue));
                            date.PredictedHiUnit = dailyForecast.Temperature.Maximum.Unit;
                            date.PredictedLow = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.Temperature.Minimum.NumericValue));
                            date.PredictedLowUnit = dailyForecast.Temperature.Minimum.Unit;
                            date.PredictedRealFeelLow = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.RealFeelTemperature.Minimum.NumericValue));
                            date.PredictedRealFeelLowUnit = dailyForecast.RealFeelTemperature.Minimum.Unit;
                            date.PredictedRealFeelHi = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.RealFeelTemperature.Maximum.NumericValue));
                            date.PredictedRealFeelHiUnit = dailyForecast.RealFeelTemperature.Maximum.Unit;
                            date.WindDirection = dailyForecast.Night.Wind.Direction.Localized;
                            date.WindSpeed = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.Night.Wind.Speed.Metric.NumericValue));
                            date.WindSpeedUnit = dailyForecast.Night.Wind.Speed.Metric.Unit;
                            date.GustSpeed = Convert.ToInt32(WeatherRepository.MathRound(dailyForecast.Night.WindGust.Speed.Metric.NumericValue));
                            date.GustSpeedUnit = dailyForecast.Night.WindGust.Speed.Metric.Unit;
                            if (dailyForecast.Night.PrecipitationProbability.HasValue)
                            {
                                Int32? nullable = dailyForecast.Night.PrecipitationProbability;
                                date.ProbabilityOfPrecipitation = String.Concat(nullable.Value, "%");
                            }
                            if (!dailyForecast.Night.RainProbability.HasValue || (Single)dailyForecast.Night.RainProbability.Value <= 0f && (Single)dailyForecast.Night.SnowProbability.Value <= 0f)
                            {
                                date.ProbabilityOfPrecipitationSub = "";
                            }
                            else
                            {
                                date.ProbabilityOfPrecipitationSub = "(";
                                if ((Single)dailyForecast.Night.RainProbability.Value > 0f)
                                {
                                    HalfDayForecastModel halfDayForecastModel9 = date;
                                    Object probabilityOfPrecipitationSub1 = halfDayForecastModel9.ProbabilityOfPrecipitationSub;
                                    Object[] str1 = new Object[] { probabilityOfPrecipitationSub1, "Regen", ": ", default(Object), default(Object) };
                                    Int32? rainProbability1 = dailyForecast.Night.RainProbability;
                                    str1[3] = rainProbability1.Value;
                                    str1[4] = "%";
                                    halfDayForecastModel9.ProbabilityOfPrecipitationSub = String.Concat(str1);
                                }
                                if ((Single)dailyForecast.Night.SnowProbability.Value > 0f)
                                {
                                    if (date.ProbabilityOfPrecipitationSub.Length > 1)
                                    {
                                        HalfDayForecastModel halfDayForecastModel10 = date;
                                        halfDayForecastModel10.ProbabilityOfPrecipitationSub = String.Concat(halfDayForecastModel10.ProbabilityOfPrecipitationSub, " ");
                                    }
                                    HalfDayForecastModel halfDayForecastModel11 = date;
                                    Object obj1 = halfDayForecastModel11.ProbabilityOfPrecipitationSub;
                                    Object[] value1 = new Object[] { obj1, "Sneeuw", ": ", default(Object), default(Object) };
                                    Int32? snowProbability1 = dailyForecast.Night.SnowProbability;
                                    value1[3] = snowProbability1.Value;
                                    value1[4] = "%";
                                    halfDayForecastModel11.ProbabilityOfPrecipitationSub = String.Concat(value1);
                                }
                                HalfDayForecastModel halfDayForecastModel12 = date;
                                halfDayForecastModel12.ProbabilityOfPrecipitationSub = String.Concat(halfDayForecastModel12.ProbabilityOfPrecipitationSub, ")");
                            }
                            date.PrecipationAmountIce = Convert.ToDouble(dailyForecast.Night.Ice.NumericValue);
                            date.PrecipationAmountIceUnits = dailyForecast.Night.Ice.Unit;
                            date.PrecipationAmountRain = Convert.ToDouble(dailyForecast.Night.Rain.NumericValue);
                            date.PrecipationAmountRainUnits = dailyForecast.Night.Rain.Unit;
                            date.PrecipationAmountSnow = Convert.ToDouble(dailyForecast.Night.Snow.NumericValue);
                            date.PrecipationAmountSnowUnits = dailyForecast.Night.Snow.Unit;
                            Int32? thunderstormProbability1 = dailyForecast.Night.ThunderstormProbability;
                            date.PrecipationAmountThunderstorms = Convert.ToInt32(thunderstormProbability1.Value);
                            if (dailyForecast.Moon != null)
                            {
                                date.MoonPhaseEnglish = dailyForecast.Moon.Phase;
                                if (dailyForecast.Moon.Phase.Equals("WaningCrescent"))
                                {
                                    dailyForecast.Moon.Phase = "WaningCrecent";
                                }
                                HalfDayForecastModel str2 = date;
                                //ResourceLoader resourceLoader = WeatherDataService._resourceLoader;
                                str = (dailyForecast.Moon.Phase == "New" ? "NewMoon" : dailyForecast.Moon.Phase);
                                str2.MoonPhase = str;// resourceLoader.GetString(str);
                                if (dailyForecast.Moon.Rise.HasValue)
                                {
                                    DateTimeOffset? rise1 = dailyForecast.Moon.Rise;
                                    date.Moonrise = rise1.Value.ToString("h:mm tt");
                                }
                                if (dailyForecast.Moon.Set.HasValue)
                                {
                                    DateTimeOffset? set1 = dailyForecast.Moon.Set;
                                    date.Moonset = set1.Value.ToString("h:mm tt");
                                }
                            }
                            HalfDayForecastModel halfDayForecastModel13 = date;
                            if (dailyForecast.Sources.ToList<String>() == null)
                            {
                                flag = false;
                            }
                            else
                            {
                                List<String> strs = dailyForecast.Sources;
                                flag3 = false;
                                /*
                                if (WeatherDataService.CS$<>9__CachedAnonymousMethodDelegate17 == null)
                                {
                                    WeatherDataService.CS$<>9__CachedAnonymousMethodDelegate17 = new Func<String, Boolean>(null, (String x) => x.ToUpper() == "HUAFENG");
                                }
                                flag = strs.Any<String>(WeatherDataService.CS$<>9__CachedAnonymousMethodDelegate17);
                                 */
                            }
                            //halfDayForecastModel13.IsAttributed = flag;
                            halfDayForecastModel13.IsAttributed = true;
                            HalfDayForecastModel halfDayForecastModel14 = date;
                            weatherAlarmTypes = (num < 4 ? this.GetWeatherAlarms(date) : new List<WeatherAlarmType>());
                            halfDayForecastModel14.Alarms = weatherAlarmTypes;
                            date.HasWeatherAlarm = date.Alarms.Any<WeatherAlarmType>();
                            HalfDayForecastModel halfDayForecastModel15 = date;
                            flag1 = (date.Alarms.Contains(WeatherAlarmType.Ice) || date.Alarms.Contains(WeatherAlarmType.Rain) || date.Alarms.Contains(WeatherAlarmType.Snow) ? true : date.Alarms.Contains(WeatherAlarmType.Thunderstorm));
                            halfDayForecastModel15.HasWeatherAlarmNonWind = flag1;
                            HalfDayForecastModel halfDayForecastModel16 = date;
                            flag2 = (date.Alarms.Contains(WeatherAlarmType.WindSpeed) ? true : date.Alarms.Contains(WeatherAlarmType.WindGust));
                            halfDayForecastModel16.HasWeatherAlarmWind = flag2;
                            halfDayForecastModel.UpdatedDateTime = DateTime.Now;
                        }
                        text.Add(date);
                        num++;
                    }
                }
                if (text.Count() > 0)
                {
                    //text.Item(0).LocalForecastSummary = this._forecastSummary.Headline.Text;
                    //text.Item(0).LocalForecastSummarySeverity = this._forecastSummary.Headline.Severity;
                    //text.Item(0).LocalForecastSummaryEffectiveDate = this._forecastSummary.Headline.EffectiveDate;
                    observableCollection = text;
                }
                else
                {
                    observableCollection = text;
                }
            }
            else
            {
                //Messenger.Default.Send<ErrorMessageTypeMessage>(new ErrorMessageTypeMessage()
                //{
                //    ErrorCode = ErrorCodeType.UnableToConnectNetwork
                //});
                observableCollection = text;
            }
            return observableCollection;
        }

        public async Task<ObservableCollection<HourlyModel>> GetHourlyAsync(String locationID, Boolean forceUpdate = false, WeatherUnitTypes metric = WeatherUnitTypes.Metric)
        {
            ObservableCollection<HourlyModel> observableCollection;
            String str;
            List<WeatherAlarmType> weatherAlarmTypes;
            ObservableCollection<HourlyModel> observableCollection1 = new ObservableCollection<HourlyModel>();
            if (WeatherRepository.IsNetworkAvailable())
            {
                List<HourlyForecast> hourlyForecasts = await this._weatherServiceProvider.GetHourlyForecasts(locationID, HourlyForecastQuery.OneDay, true);
                List<HourlyForecast> hourlyForecasts1 = hourlyForecasts;
                if (hourlyForecasts1 != null)
                {
                    Int32 num = 0;
                    foreach (HourlyForecast hourlyForecast in hourlyForecasts1)
                    {
                        try
                        {

                        HourlyModel hourlyModel = new HourlyModel()
                        {
                            Date = hourlyForecast.DateTime.DateTime
                        };
                        if (hourlyForecast.DewPoint.Value.HasValue)
                        {
                            Double num1 = WeatherRepository.MathRound(hourlyForecast.DewPoint.Value);
                            hourlyModel.DewPoint = num1.ToString();
                        }
                        if (hourlyForecast.WindGust.Speed.Metric != null && hourlyForecast.WindGust.Speed.Metric.Value.HasValue)
                        {
                            Double num2 = WeatherRepository.MathRound(hourlyForecast.WindGust.Speed.Metric.Value);
                            hourlyModel.GustSpeed = num2.ToString();
                            hourlyModel.GustSpeedUnit = hourlyForecast.WindGust.Speed.Metric.Unit;
                        }

                        hourlyModel.UVIndex = hourlyForecast.UVIndex.Value;

                        if (hourlyForecast.RelativeHumidity.HasValue)
                        {
                            hourlyModel.Humidity = hourlyForecast.RelativeHumidity.Value.ToString();
                        }
                        if (hourlyForecast.Ice.NumericValue.HasValue)
                        {
                            hourlyModel.PrecipationAmountIce = hourlyForecast.Ice.NumericValue.ToString();
                        }
                        hourlyModel.PrecipationAmountRain = hourlyForecast.Rain.NumericValue.ToString();
                        hourlyModel.PrecipationAmountRainUnits = hourlyForecast.Rain.Unit;
                        hourlyModel.PrecipationAmountSnow = hourlyForecast.Snow.NumericValue.ToString();
                        hourlyModel.PrecipationAmountSnowUnits = hourlyForecast.Snow.Unit;
                        hourlyModel.PrecipationAmountIce = hourlyForecast.Ice.NumericValue.ToString();
                        hourlyModel.PrecipationAmountIceUnits = hourlyForecast.Ice.Unit;
                        HourlyModel hourlyModel1 = hourlyModel;
                        if (hourlyForecast.ThunderstormProbability.HasValue)
                        {
                            Int32? thunderstormProbability = hourlyForecast.ThunderstormProbability;
                            str = String.Concat(thunderstormProbability.Value, "%");
                        }
                        else
                        {
                            str = "0%";
                        }
                        hourlyModel1.PrecipationAmountThunderstorms = str;
                        Double num3 = WeatherRepository.MathRound(hourlyForecast.RealFeelTemperature.NumericValue);
                        hourlyModel.PredictedRealFeel = num3.ToString();
                        hourlyModel.PredictedRealFeelUnit = hourlyForecast.RealFeelTemperature.Unit;

                        Double num4 = WeatherRepository.MathRound(hourlyForecast.Temperature.NumericValue);
                        hourlyModel.PredictedTemperature = num4.ToString();
                        hourlyModel.PredictedTemperatureUnit = hourlyForecast.Temperature.Unit;

                        if (hourlyForecast.PrecipitationProbability.HasValue)
                        {
                            Int32? precipitationProbability = hourlyForecast.PrecipitationProbability;
                            hourlyModel.ProbabilityOfPrecipation = String.Concat(precipitationProbability.Value, "%");
                        }

                        hourlyModel.ShortPhrase = hourlyForecast.IconPhrase;
                        hourlyModel.ShortTime = hourlyForecast.DateTime.ToString("HH tt");
                        DateTime date = hourlyForecast.DateTime.Date;
                        DateTime dateTime = DateTime.Now.Date;
                        DateTime dateTime1 = DateTime.Now.AddDays(1);
                        DateTime date1 = dateTime1.Date;
                        if (dateTime == date)
                        {
                            DateTimeOffset dateTimeOffset = hourlyForecast.DateTime;
                            hourlyModel.ShortTimeQualifier = String.Concat(dateTimeOffset.ToString("HH tt"), " ", "Vandaag");
                        }
                        else if (date1 != date)
                        {
                            hourlyModel.ShortTimeQualifier = hourlyForecast.DateTime.ToString("HH tt dddd");
                        }
                        else
                        {
                            DateTimeOffset dateTimeOffset1 = hourlyForecast.DateTime;
                            hourlyModel.ShortTimeQualifier = String.Concat(dateTimeOffset1.ToString("HH tt"), " ", "Morgen");
                        }
                        hourlyModel.WeatherCode = hourlyForecast.WeatherIcon.Value;
                        hourlyModel.WindDirection = hourlyForecast.Wind.Direction.Localized;
                        //Double num5 = WeatherRepository.MathRound(hourlyForecast.Wind.Speed.Metric.NumericValue);
                        //hourlyModel.WindSpeed = num5.ToString();
                        //hourlyModel.WindSpeedUnit = hourlyForecast.Wind.Speed.Metric.Unit;
                        HourlyModel hourlyModel2 = hourlyModel;
                        weatherAlarmTypes = (num < 4 ? this.GetWeatherAlarms(hourlyForecast) : new List<WeatherAlarmType>());
                        hourlyModel2.Alarms = weatherAlarmTypes;
                        hourlyModel.HasWeatherAlarm = hourlyModel.Alarms.Count > 0;
                        hourlyModel.UpdatedDateTime = DateTime.Now;
                        observableCollection1.Add(hourlyModel);
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                            //throw;
                        }

                        num++;

                    }
                    observableCollection = observableCollection1;
                }
                else
                {
                    observableCollection = observableCollection1;
                }
            }
            else
            {
                //Messenger.Default.Send<ErrorMessageTypeMessage>(new ErrorMessageTypeMessage()
                //{
                //    ErrorCode = ErrorCodeType.UnableToConnectNetwork
                //});
                observableCollection = observableCollection1;
            }
            return observableCollection;
        }

        private List<WeatherAlarmType> GetWeatherAlarms(HalfDayForecastModel halfDayForecastModel)
        {
            this.GetWeatherAlarmThresholdValues();
            List<WeatherAlarmType> weatherAlarmTypes = new List<WeatherAlarmType>();
            if (halfDayForecastModel.PrecipationAmountIce > this._weatherAlarmThresholdIce)
            {
                weatherAlarmTypes.Add(WeatherAlarmType.Ice);
            }
            if (halfDayForecastModel.PrecipationAmountRain > this._weatherAlarmThresholdRain)
            {
                weatherAlarmTypes.Add(WeatherAlarmType.Rain);
            }
            if (halfDayForecastModel.PrecipationAmountSnow > this._weatherAlarmThresholdSnow)
            {
                weatherAlarmTypes.Add(WeatherAlarmType.Snow);
            }
            if (halfDayForecastModel.PrecipationAmountThunderstorms > this._weatherAlarmThresholdThunderstorm)
            {
                weatherAlarmTypes.Add(WeatherAlarmType.Thunderstorm);
            }
            if ((Double)halfDayForecastModel.WindSpeed > this._weatherAlarmThresholdWindSpeed)
            {
                weatherAlarmTypes.Add(WeatherAlarmType.WindSpeed);
            }
            if ((Double)halfDayForecastModel.GustSpeed > this._weatherAlarmThresholdWindGusts)
            {
                weatherAlarmTypes.Add(WeatherAlarmType.WindGust);
            }
            return weatherAlarmTypes;
        }

        private List<WeatherAlarmType> GetWeatherAlarms(HourlyForecast hourlyForecast)
        {
            this.GetWeatherAlarmThresholdValues();
            List<WeatherAlarmType> weatherAlarmTypes = new List<WeatherAlarmType>();
            Double? numericValue = hourlyForecast.Ice.NumericValue;
            Double num = this._weatherAlarmThresholdIce;
            if (((Double)numericValue.GetValueOrDefault() <= num ? false : numericValue.HasValue))
            {
                weatherAlarmTypes.Add(WeatherAlarmType.Ice);
            }
            Double? nullable = hourlyForecast.Rain.NumericValue;
            Double num1 = this._weatherAlarmThresholdRain;
            if (((Double)nullable.GetValueOrDefault() <= num1 ? false : nullable.HasValue))
            {
                weatherAlarmTypes.Add(WeatherAlarmType.Rain);
            }
            Double? numericValue1 = hourlyForecast.Snow.NumericValue;
            Double num2 = this._weatherAlarmThresholdSnow;
            if (((Double)numericValue1.GetValueOrDefault() <= num2 ? false : numericValue1.HasValue))
            {
                weatherAlarmTypes.Add(WeatherAlarmType.Snow);
            }
            Int32? thunderstormProbability = hourlyForecast.ThunderstormProbability;
            Int32 num3 = this._weatherAlarmThresholdThunderstorm;
            if ((thunderstormProbability.GetValueOrDefault() <= num3 ? false : thunderstormProbability.HasValue))
            {
                weatherAlarmTypes.Add(WeatherAlarmType.Thunderstorm);
            }
            /*
            Double? nullable1 = hourlyForecast.Wind.Speed.Metric.NumericValue;
            Double num4 = this._weatherAlarmThresholdWindSpeed;
            if (((Double)nullable1.GetValueOrDefault() <= num4 ? false : nullable1.HasValue))
            {
                weatherAlarmTypes.Add(WeatherAlarmType.WindSpeed);
            }
            Double? numericValue2 = hourlyForecast.WindGust.Speed.Metric.NumericValue;
            Double num5 = this._weatherAlarmThresholdWindGusts;
            if (((Double)numericValue2.GetValueOrDefault() <= num5 ? false : numericValue2.HasValue))
            {
                weatherAlarmTypes.Add(WeatherAlarmType.WindGust);
            }
            */
            return weatherAlarmTypes;
        }

        private void GetWeatherAlarmThresholdValues()
        {
            this._weatherAlarmThresholdIce = 0.254;
            this._weatherAlarmThresholdRain = 12.7;
            this._weatherAlarmThresholdSnow = 2.54;
            this._weatherAlarmThresholdWindSpeed = 48;
            this._weatherAlarmThresholdWindGusts = 64;
            this._weatherAlarmThresholdThunderstorm = 75;
            return;
        }

        private static Boolean IsNetworkAvailable()
        {
            ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
            if (internetConnectionProfile == null)
            {
                return false;
            }
            NetworkConnectivityLevel networkConnectivityLevel = internetConnectionProfile.GetNetworkConnectivityLevel();
            if (networkConnectivityLevel == NetworkConnectivityLevel.InternetAccess)
            {
                return true;
            }
            return networkConnectivityLevel == NetworkConnectivityLevel.ConstrainedInternetAccess;
        }

        private static Double MathRound(Double? value)
        {
            if (!value.HasValue)
            {
                return 0;
            }
            return Math.Round((Double)value.Value, MidpointRounding.AwayFromZero);
        }
    }
}
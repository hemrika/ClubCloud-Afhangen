namespace ClubCloud.Afhangen.DesignViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using Microsoft.Practices.Prism.StoreApps;
    using Microsoft.Practices.Prism.StoreApps.Interfaces;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.Streams;

    public class WeerPageDesignViewModel : IView
    {
        public WeerPageDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            CurrentConditions = new CurrentConditionsModel { ApparentTemperature = "18", ApparentTemperatureUnit =" C", Temperature = "15", TemperatureUnit = " C", ShortPhrase = "Overwegend bewolkt", WindDirection= "zzo", WindSpeed = "50", WindSpeedUnit ="km/u", GustSpeed = "56", GustSpeedUnit = "km/u", Precip1hr = "10", Precip1hrUnit = "mm/h", UpdatedDateTime = DateTime.Now };
            HourlyModels = new ObservableCollection<HourlyModel>(){
                new HourlyModel { Date = DateTime.Now, PrecipationAmountRain = "10" , ProbabilityOfPrecipation = "10%", PredictedRealFeel = "-10", PredictedTemperature = "-8", UVIndex = 0, WeatherCode = 1   },
                new HourlyModel { Date = DateTime.Now.AddHours(1), PrecipationAmountRain = "10", ProbabilityOfPrecipation = "10%", PredictedRealFeel = "-10", PredictedTemperature = "-8" , UVIndex = 1 , WeatherCode = 2 },
                new HourlyModel { Date = DateTime.Now.AddHours(2), PrecipationAmountRain = "20", ProbabilityOfPrecipation = "20%", PredictedRealFeel = "0", PredictedTemperature = "4" , UVIndex = 2 , WeatherCode = 3  },
                new HourlyModel { Date = DateTime.Now.AddHours(3), PrecipationAmountRain = "5", ProbabilityOfPrecipation = "30%", PredictedRealFeel = "10", PredictedTemperature = "0" , UVIndex = 3 , WeatherCode = 4 },
                new HourlyModel { Date = DateTime.Now.AddHours(4), PrecipationAmountRain = "7", ProbabilityOfPrecipation = "40%", PredictedRealFeel = "10", PredictedTemperature = "8" , UVIndex = 3, WeatherCode =  5 },
            };

            
            try
            {
                StorageFile _storageFile = Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Weather/Icon/01.png")).GetResults();
                IBuffer readbuffer = FileIO.ReadBufferAsync(_storageFile).GetResults();
                CurrentConditions.WeatherIcon = readbuffer.ToArray();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            UVIndexList = new ObservableCollection<UVIndex> {
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/00.png"),"Ga gerust buiten tennissen.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/01.png"),"Ga gerust buiten tennissen."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/02.png"),"Ga gerust buiten tennissen."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/03.png"),"Niet langer dan 40 minuten tennissen of in de zon."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/04.png"),"Niet langer dan 40 minuten tennissen of in de zon."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/05.png"),"Niet langer dan 40 minuten tennissen of in de zon."+Environment.NewLine+"Rust in de schaduw.","SPF15+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/06.png"),"Niet langer dan 20 minuten in de zon."+Environment.NewLine+"Ga niet tennissen.","SPF30+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/07.png"),"Niet langer dan 20 minuten in de zon."+Environment.NewLine+"Ga niet tennissen.","SPF30+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/08.png"),"Niet langer dan 20 minuten in de zon."+Environment.NewLine+"Ga niet tennissen.","SPF30+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/09.png"),"Zeer Hoog, ga niet buiten tennissen","SPF50+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/10.png"),"Zeer Hoog, ga niet buiten tennissen","SPF50+"),
                    new UVIndex(new Uri("ms-appx:///Assets/Weather/UV/11.png"),"Extreem Hoog, ga niet buiten tennissen","SPF50+"),
                    };

            try
            {
                StorageFile _storageFile = Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Weather/UV/01.png")).GetResults();
                IBuffer readbuffer = FileIO.ReadBufferAsync(_storageFile).GetResults();
                CurrentConditions.UVIcon = readbuffer.ToArray();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            TemperatureRange = new TemperatureRange { Maximum = new Measurement { Value = 20 }, Minimum = new Measurement { Value = -10 } };
        }

        public CurrentConditionsModel CurrentConditions { get; private set; }

        public ObservableCollection<HourlyModel> HourlyModels { get; private set; }

        public TemperatureRange TemperatureRange { get; private set; }

        public ObservableCollection<UVIndex> UVIndexList { get; private set; }

        object IView.DataContext
        {
            get
            {
                return null;
            }
            set
            {
                object dc = value;
            }
        }
    }
}

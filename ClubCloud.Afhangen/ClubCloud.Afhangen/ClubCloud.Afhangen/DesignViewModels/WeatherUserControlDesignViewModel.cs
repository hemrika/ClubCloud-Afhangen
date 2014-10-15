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

    public class WeatherUserControlDesignViewModel : IView
    {
        public WeatherUserControlDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            CurrentConditions = new CurrentConditionsModel { ApparentTemperature = "18", ApparentTemperatureUnit =" C", Temperature = "15", TemperatureUnit = " C", ShortPhrase = "Overwegend bewolkt",   };
            HourlyModels = new ObservableCollection<HourlyModel>(){
                new HourlyModel { },
                new HourlyModel { },
                new HourlyModel { },
                new HourlyModel { },
                new HourlyModel { }
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

        }

        public CurrentConditionsModel CurrentConditions { get; private set; }

        public ObservableCollection<HourlyModel> HourlyModels { get; private set; }

        //public byte[] WeerIcoon { get; private set; }

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

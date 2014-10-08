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
            CurrentConditions = new CurrentConditionsModel { };
            HourlyModels = new ObservableCollection<HourlyModel>(){
                new HourlyModel { },
                new HourlyModel { },
                new HourlyModel { },
                new HourlyModel { },
                new HourlyModel { }
            };


            StorageFile _storageFile = Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Weather/01.png")).GetResults();

            try
            {
                IBuffer readbuffer = FileIO.ReadBufferAsync(_storageFile).GetResults();
                WeerIcoon = readbuffer.ToArray();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

        }

        public CurrentConditionsModel CurrentConditions { get; private set; }

        public ObservableCollection<HourlyModel> HourlyModels { get; private set; }

        public byte[] WeerIcoon { get; private set; }
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

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
    using Windows.UI.Xaml.Media;

    public class CloudUserControlDesignViewModel : IView
    {
        public CloudUserControlDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {

        }

        public ImageSource Image
        {
            get { return new CloudImage(new Uri("http://mijn.buienradar.nl/forecast/forecast_gps_0.png")).Image; }
        }

        public ImageSource NextImage
        {
            get { return new CloudImage(new Uri("http://mijn.buienradar.nl/forecast/forecast_gps_1.png")).Image; }
        }

        public ClubPosition Position { get { return new ClubPosition(); } }

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

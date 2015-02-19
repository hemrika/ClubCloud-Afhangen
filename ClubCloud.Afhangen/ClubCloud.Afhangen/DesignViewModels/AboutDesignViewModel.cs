namespace ClubCloud.Afhangen.DesignViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using Microsoft.Practices.Prism.StoreApps;
    using Microsoft.Practices.Prism.StoreApps.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class AboutDesignViewModel : IView
    {
        public AboutDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Time = DateTime.Now.ToString("HH:mm");
        }

        public string Time { get; private set; }

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

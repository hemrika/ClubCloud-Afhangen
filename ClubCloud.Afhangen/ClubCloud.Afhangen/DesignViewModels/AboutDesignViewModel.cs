namespace ClubCloud.Afhangen.DesignViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Core.Prism;
    using ClubCloud.Core.Prism.Interfaces;
    
    
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;
    public class AboutFlyoutDesignViewModel : IView
    {
        public AboutFlyoutDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            var version = Package.Current.Id.Version;
            Version = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        public string Version { get; set; }

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

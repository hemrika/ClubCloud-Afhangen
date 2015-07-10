namespace ClubCloud.Afhangen.DesignViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Core.Prism;
    using ClubCloud.Core.Prism.Interfaces;
    
    
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;

    public class MainPageDesignViewModel : IView
    {
        public MainPageDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Vereniging = new Vereniging { Id = Guid.NewGuid(), Naam = "Mijn ClubCloud Club Naam", Nummer = "00000" };
            PackageVersion version = Package.Current.Id.Version;
            Version = string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        public Vereniging Vereniging { get; private set; }

        public string Version { get; private set; }

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

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    using Microsoft.Practices.Prism.Mvvm;
    using Microsoft.Practices.Prism.Mvvm.Interfaces;
    using System;
    using Windows.ApplicationModel;

    public class AboutFlyoutViewModel : ViewModel, IFlyoutViewModel
    {
        
        private Action closeFlyout;

        public string Version { get; set; }

        public AboutFlyoutViewModel()
        {
            var version = Package.Current.Id.Version;
            Version = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        public Action CloseFlyout
        {
            get { return closeFlyout; }
            set { SetProperty(ref closeFlyout, value); }
        }
    }
}

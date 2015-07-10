namespace ClubCloud.Afhangen.Views
{
    using ClubCloud.Core.Prism;
    using ClubCloud.Core.Prism.Interfaces;
    using System;
    using Windows.ApplicationModel;
    using Windows.UI.Xaml.Controls;

    public sealed partial class AboutFlyout : SettingsFlyout, IFlyoutViewModel, IView
    {
        private Action closeFlyout;

        public string Version { get; set; }

        public Action CloseFlyout
        {
            get { return closeFlyout; }
            set { closeFlyout = value; }
        }

        /*
        public object DataContext
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        */
        public AboutFlyout()
        {
            this.InitializeComponent();

            //var viewModel = this.DataContext as IFlyoutViewModel;
            //viewModel.CloseFlyout = () => this.Hide();

            var version = Package.Current.Id.Version;
            Version = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

        }
    }
}

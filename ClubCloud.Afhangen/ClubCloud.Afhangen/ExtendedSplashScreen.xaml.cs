namespace ClubCloud.Afhangen
{
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class ExtendedSplashScreen : Page
    {
        private readonly SplashScreen splashScreen;
        public ExtendedSplashScreen()
        {
            this.InitializeComponent();
        }
        public ExtendedSplashScreen(SplashScreen splashScreen)
        {
            this.splashScreen = splashScreen;

            this.InitializeComponent();

            this.SizeChanged += ExtendedSplashScreen_SizeChanged;
            this.splashImage.ImageOpened += splashImage_ImageOpened;
        }
        void splashImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            Resize();
            Window.Current.Activate();
        }

        void ExtendedSplashScreen_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Resize();
        }

        private void Resize()
        {
            if (this.splashScreen == null) return;

            this.splashImage.Height = this.splashScreen.ImageLocation.Height;
            this.splashImage.Width = this.splashScreen.ImageLocation.Width;

            this.splashImage.SetValue(Canvas.TopProperty, this.splashScreen.ImageLocation.Top);
            this.splashImage.SetValue(Canvas.LeftProperty, this.splashScreen.ImageLocation.Left);

            this.progressRing.SetValue(Canvas.TopProperty, this.splashScreen.ImageLocation.Top + this.splashScreen.ImageLocation.Height);
            this.progressRing.SetValue(Canvas.LeftProperty, this.splashScreen.ImageLocation.Left + this.splashScreen.ImageLocation.Width / 2 - this.progressRing.Width / 2);
        }
    }
}

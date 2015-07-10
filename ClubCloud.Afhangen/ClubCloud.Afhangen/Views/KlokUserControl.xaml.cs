using ClubCloud.Core.Prism;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ClubCloud.Afhangen.Views
{
    public sealed partial class KlokUserControl : UserControl, IView
    {
        //DispatcherTimer timer = new DispatcherTimer();
        public KlokUserControl()
        {
            this.InitializeComponent();
            /*
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += timer_Tick;
            timer.Start();
            clock.Text = DateTime.Now.ToString("HH:mm");
            */
        }

        /*
        async void timer_Tick(object sender, object e)
        {
            clock.Text = DateTime.Now.ToString("HH:mm");
        }
        */

    }
}

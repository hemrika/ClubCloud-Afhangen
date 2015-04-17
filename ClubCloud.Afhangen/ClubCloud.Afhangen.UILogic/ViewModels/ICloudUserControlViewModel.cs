using ClubCloud.Afhangen.UILogic.Models;
using System;
using Windows.UI.Xaml.Media;
namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    interface ICloudUserControlViewModel
    {
        ImageSource Image { get; }
        ImageSource NextImage { get; }

        void UpdateCloudsAsync(bool update);
    }
}

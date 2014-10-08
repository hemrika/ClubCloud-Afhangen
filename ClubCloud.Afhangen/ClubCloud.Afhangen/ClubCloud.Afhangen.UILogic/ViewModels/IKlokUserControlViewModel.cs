using System;
namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public interface IKlokUserControlViewModel
    {
        string Time { get; }
        void UpdateKlokAsync(TimeSpan span);
    }
}

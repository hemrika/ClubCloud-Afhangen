using System;
namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public interface IKlokUserControlViewModel
    {
        string Time { get; }
        string Date { get; }

        void UpdateKlokAsync(TimeSpan span);
    }
}

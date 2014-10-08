using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Navigation;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public interface IBaanUserControlViewModel
    {
        void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewState);
        
        void OnNavigatedFrom(Dictionary<string, object> viewState, bool suspending);

        string Baansoort { get; }
        TimeSpan Duur { get; }
        Guid Id { get; }
        bool IsBaanSelected();
        string Naam { get; }
        int Nummer { get; }
        //System.Collections.ObjectModel.ObservableCollection<ReserveringViewModel> ReserveringViewModels { get; }
        Microsoft.Practices.Prism.Commands.DelegateCommand SelecterenBaanCommand { get; set; }
        TimeSpan BeginTijd { get; }
        void UpdateBaanAsync(object notUsed);
    }
}
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

        TimeSpan Duur { get; }
        Guid Id { get; }
        //bool IsBaanSelected();
        string Naam { get; }
        int Nummer { get; }
        string Locatie { get; }
        string Soort { get; }
        string Type { get; }
        bool Verlichting { get; }
        bool Selectable { get; }

        //System.Collections.ObjectModel.ObservableCollection<ReserveringViewModel> ReserveringViewModels { get; }
        ClubCloud.Core.Prism.Commands.DelegateCommand SelecterenBaanCommand { get; set; }
        TimeSpan BeginTijd { get; }
        //void UpdateBaanAsync(object notUsed);
    }
}
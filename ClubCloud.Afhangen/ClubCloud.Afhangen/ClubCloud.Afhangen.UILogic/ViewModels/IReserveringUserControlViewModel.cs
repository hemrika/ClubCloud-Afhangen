using ClubCloud.Afhangen.UILogic.Models;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Navigation;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public interface IReserveringUserControlViewModel
    {
        int AantalSpelers { get; }
        Microsoft.Practices.Prism.Commands.DelegateCommand Action { get; }
        string Actionable { get; }
        string ActionName { get; }
        Microsoft.Practices.Prism.Commands.DelegateCommand AnnulerenCommand { get; set; }
        ClubCloud.Afhangen.UILogic.Models.Baan Baan { get; }
        System.Threading.Tasks.Task BaanWijzigen();
        TimeSpan BeginTijd { get; }
        Microsoft.Practices.Prism.Commands.DelegateCommand BevestigenCommand { get; set; }
        TimeSpan Duur { get; }
        TimeSpan EindTijd { get; }
        bool IsBaanSelected();
        bool IsSpelerSelected();
        bool KanBevestigen();
        System.Collections.ObjectModel.ObservableCollection<ClubCloud.Afhangen.UILogic.Models.Speler> Spelers { get; }
        System.Threading.Tasks.Task SpelersWijzigen();
        void UpdateReserveringAsync(object notUsed);
        bool ValidateForm();
        Microsoft.Practices.Prism.Commands.DelegateCommand VerwijderenCommand { get; set; }
        Microsoft.Practices.Prism.Commands.DelegateCommand WijzigBaanCommand { get; set; }
        Microsoft.Practices.Prism.Commands.DelegateCommand WijzigSpelersCommand { get; set; }
    }
}
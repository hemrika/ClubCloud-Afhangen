using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.Commands;
using System;
using System.Collections.Generic;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public interface ISpelerUserControlViewModel
    {
        DelegateCommand Action { get; }
        string ActionName { get; }
        byte[] Foto { get; }
        Guid Id { get; }
        int Index { get; set; }
        bool IsSpelersSelected();
        string Naam { get; }
        string Nummer { get; }
        ClubCloud.Core.Prism.Commands.DelegateCommand SelecterenSpelerCommand { get; set; }
        ClubCloud.Core.Prism.Commands.DelegateCommand SpelerNavigationCommand { get; set; }
        void UpdateSpelerAsync(ClubCloud.Afhangen.UILogic.Models.Speler speler);
        ClubCloud.Core.Prism.Commands.DelegateCommand VerwijderenSpelerCommand { get; set; }
    }
}

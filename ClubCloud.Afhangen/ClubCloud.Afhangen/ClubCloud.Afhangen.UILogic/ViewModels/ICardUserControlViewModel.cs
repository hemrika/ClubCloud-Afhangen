using ClubCloud.Afhangen.UILogic.Models;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public interface ICardUserControlViewModel
    {
        Microsoft.Practices.Prism.Commands.DelegateCommand GoBackCommand { get; }
        bool IsOpen { get; set; }

        int Index { get; set; }

        Microsoft.Practices.Prism.Commands.DelegateCommand KeyUpCommand { get; set; }
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        void Show(object notused);
        System.Threading.Tasks.Task ShowAsync(int index);
        System.Threading.Tasks.Task<ClubCloud.Afhangen.UILogic.Models.Speler> Speler { get; }
        Microsoft.Practices.Prism.Commands.DelegateCommand TextChangedCommand { get; set; }


    }
}

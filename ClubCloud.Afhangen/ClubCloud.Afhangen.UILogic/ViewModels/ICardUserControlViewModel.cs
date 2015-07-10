using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.Interfaces;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace ClubCloud.Afhangen.UILogic.ViewModels
{
    public interface ICardUserControlViewModel
    {
        ClubCloud.Core.Prism.Commands.DelegateCommand GoBackCommand { get; }
        bool IsOpen { get; set; }

        int Index { get; set; }

        ClubCloud.Core.Prism.Commands.DelegateCommand KeyUpCommand { get; set; }
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        void Show(object notused);
        System.Threading.Tasks.Task ShowAsync(int index);
        System.Threading.Tasks.Task<ClubCloud.Afhangen.UILogic.Models.Speler> Speler { get; }
        ClubCloud.Core.Prism.Commands.DelegateCommand TextChangedCommand { get; set; }


    }
}

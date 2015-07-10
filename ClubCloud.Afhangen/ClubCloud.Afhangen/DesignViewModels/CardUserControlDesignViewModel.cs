using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Afhangen.UILogic.ViewModels;
using ClubCloud.Core.Prism.Commands;
using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism.PubSubEvents;


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class CardUserControlDesignViewModel //: IView
    {
        public CardUserControlDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
        }

        public DelegateCommand KeyUpCommand { get; set; }

        public DelegateCommand TextChangedCommand { get; set; }

        public DelegateCommand GoBackCommand { get; private set; }

        /*
        public ObservableCollection<ReserveringViewModel> ReserveringViewModels
        { get; private set; }
        */

        /*
        object IView.DataContext
        {
            get
            {
                return null;
            }
            set
            {
                object dc = value;
            }
        }
        */
    }
}

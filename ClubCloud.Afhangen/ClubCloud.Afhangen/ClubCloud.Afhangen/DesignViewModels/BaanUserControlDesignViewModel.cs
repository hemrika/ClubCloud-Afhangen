using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Afhangen.UILogic.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class BaanUserControlDesignViewModel //: IView
    {
        public BaanUserControlDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Baansoort = "Buiten";
            Id = Guid.Empty;
            Naam = "Baan 1";
            Nummer = 1;
            Duur = TimeSpan.FromMinutes(60);
            BeginTijd = DateTime.Now.TimeOfDay;

            Baan baan = new Baan { Id = Id, Naam = Naam, Baansoort = Baansoort, Nummer = Nummer };
        }

        public DelegateCommand SelecterenBaanCommand { get; set; }

        public string Baansoort { get; private set; }

        public Guid Id { get; private set; }


        public string Naam { get; private set; }

        public int Nummer { get; private set; }

        public TimeSpan Duur { get; private set; }

        public TimeSpan BeginTijd { get; private set; }

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

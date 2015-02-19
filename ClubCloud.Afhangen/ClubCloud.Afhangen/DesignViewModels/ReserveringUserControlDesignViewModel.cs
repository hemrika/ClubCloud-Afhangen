using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Repositories;
using ClubCloud.Afhangen.UILogic.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class ReserveringUserControlDesignViewModel //: IView
    {
        public ReserveringUserControlDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            BeginTijd = DateTime.Now.TimeOfDay;
            Duur = TimeSpan.FromMinutes(60);
            EindTijd = DateTime.Now.TimeOfDay.Add(Duur);

            Spelers = new ObservableCollection<Speler>(){
                new Speler{ Id = Guid.NewGuid(), Bondsnummer = "00000001", Achternaam = "1", Roepnaam ="Speler"},
                new Speler{ Id = Guid.NewGuid(), Bondsnummer = "00000002", Achternaam = "2", Roepnaam ="Speler"},
                new Speler{ Id = Guid.NewGuid(), Bondsnummer = "00000003", Achternaam = "3", Roepnaam ="Speler"},
                new Speler{ Id = Guid.NewGuid(), Bondsnummer = "00000004", Achternaam = "4", Roepnaam ="Speler"},
            };
            AantalSpelers = 4;
            Baan = new Baan { Id = Guid.NewGuid(), Naam = "Een Baan", Nummer = 1, Verlichting = true, Type = "", Soort = "", Locatie = "Buiten"};
        }

        public ObservableCollection<Speler> Spelers { get; private set; }

        public Baan Baan { get; private set; }

        public TimeSpan BeginTijd { get; private set; }

        public TimeSpan Duur { get; private set; }

        public TimeSpan EindTijd { get; private set; }

        public int AantalSpelers { get; private set; }

        public DelegateCommand BevestigenCommand { get; private set; }

        public DelegateCommand WijzigSpelersCommand { get; set; }

        public DelegateCommand WijzigBaanCommand { get; set; }

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

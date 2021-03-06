﻿using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.ViewModels;
using ClubCloud.Core.Prism.Commands;
using ClubCloud.Core.Prism;
using System;
using System.Collections.ObjectModel;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class SpelersPageDesignViewModel //: IView
    {
        public SpelersPageDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Spelers = new ObservableCollection<SpelerUserControlViewModel>(){
                //new Speler{Id = Guid.NewGuid(), Roepnaam = "Voornaam 1", Achternaam = "Achternaam", Bondsnummer = "12345678", Tussenvoegsel="Tussenvoegsel"},
                //new Speler{ Id = Guid.NewGuid(), Roepnaam = "Voornaam 2", Achternaam = "Achternaam", Bondsnummer = "12345678", Tussenvoegsel="Tussenvoegsel"},
                //new Speler{ Id = Guid.NewGuid(), Roepnaam = "Voornaam 3", Achternaam = "Achternaam", Bondsnummer = "12345678", Tussenvoegsel="Tussenvoegsel"},
                //new Speler{ Id = Guid.NewGuid(), Roepnaam = "Voornaam 4", Achternaam = "Achternaam", Bondsnummer = "12345678", Tussenvoegsel="Tussenvoegsel"},
                new SpelerUserControlViewModel(1, new Speler{ Id = Guid.NewGuid(), Roepnaam = "Voornaam 1", Achternaam = "Achternaam", Bondsnummer = "12345678", Tussenvoegsel="Tussenvoegsel"} ,null,null, null, null ,null,null,null),
                new SpelerUserControlViewModel(2, new Speler{ Id = Guid.NewGuid(), Roepnaam = "Voornaam 2", Achternaam = "Achternaam", Bondsnummer = "12345678", Tussenvoegsel="Tussenvoegsel"} ,null,null, null, null ,null,null,null),
                new SpelerUserControlViewModel(3, new Speler{ Id = Guid.NewGuid(), Roepnaam = "Voornaam 3", Achternaam = "Achternaam", Bondsnummer = "12345678", Tussenvoegsel="Tussenvoegsel"} ,null,null, null, null ,null,null,null),
                new SpelerUserControlViewModel(4, new Speler{ Id = Guid.NewGuid(), Roepnaam = "Voornaam 4", Achternaam = "Achternaam", Bondsnummer = "12345678", Tussenvoegsel="Tussenvoegsel"} ,null,null, null, null ,null,null,null),
            };
        }

        public ObservableCollection<SpelerUserControlViewModel> Spelers { get; private set; }

        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand GoNextCommand { get; private set; }

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

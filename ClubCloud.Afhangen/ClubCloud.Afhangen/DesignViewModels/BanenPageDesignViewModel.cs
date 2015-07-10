using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.ViewModels;
using ClubCloud.Core.Prism;
using System;
using System.Collections.ObjectModel;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class BanenPageDesignViewModel //: IView
    {
        public BanenPageDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Banen = new ObservableCollection<BaanUserControlViewModel>(){
                new BaanUserControlViewModel(new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 1", Nummer =1},null,null, null, null ,null,null,null),
                new BaanUserControlViewModel(new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 2", Nummer =2},null,null, null, null ,null,null,null),
                new BaanUserControlViewModel(new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 3", Nummer =3},null,null, null, null ,null,null,null),
                new BaanUserControlViewModel(new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 4", Nummer =4},null,null, null, null ,null,null,null),
                new BaanUserControlViewModel(new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 5", Nummer =5},null,null, null, null ,null,null,null),
            };
        }

        public ObservableCollection<BaanUserControlViewModel> Banen { get; private set; }

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

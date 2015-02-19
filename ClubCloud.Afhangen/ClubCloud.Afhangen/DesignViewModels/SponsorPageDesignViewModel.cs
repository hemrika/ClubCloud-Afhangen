using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class SponsorPageDesignViewModel //: IView
    {
        public SponsorPageDesignViewModel()
        {
            FillWithDummyData();

        }

        private void FillWithDummyData()
        {
            Sponsors = new ObservableCollection<Sponsor>(){
                new Sponsor{ Id = Guid.NewGuid(), Naam = "Sponsor 1", Type = "item", Path = new Uri("ms-appx:///Assets/placeHolderSponsor.png")},
                new Sponsor{ Id = Guid.NewGuid(), Naam = "Sponsor 2", Type = "item", Path = new Uri("ms-appx:///Assets/placeHolderSponsor.png")},
                new Sponsor{ Id = Guid.NewGuid(), Naam = "Sponsor 3", Type = "item", Path = new Uri("ms-appx:///Assets/placeHolderSponsor.png")},
                new Sponsor{ Id = Guid.NewGuid(), Naam = "Sponsor 4", Type = "item", Path = new Uri("ms-appx:///Assets/placeHolderSponsor.png")},
                new Sponsor{ Id = Guid.NewGuid(), Naam = "Sponsor 5", Type = "item", Path = new Uri("ms-appx:///Assets/placeHolderSponsor.png")},
            };

            Index = 0;
        }

        public ObservableCollection<Sponsor> Sponsors { get; private set; }

        public int Index { get; private set; }

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

using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.ViewModels;
using ClubCloud.Core.Prism.Commands;
using ClubCloud.Core.Prism;
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
                new Sponsor{ Id = Guid.NewGuid(), Naam = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed eros ligula, interdum scelerisque lacus ut, mattis efficitur sapien. In erat purus, suscipit ut tortor nec, accumsan pretium metus. In id pharetra lacus. Phasellus quis viverra turpis. Vestibulum tempor risus eget vestibulum sollicitudin. Curabitur fringilla quam at elit pharetra porttitor. Quisque at tincidunt leo. Nulla nisi orci, finibus ut luctus eget, scelerisque at lectus. Nullam ultricies arcu eu odio euismod aliquam. Mauris nec dolor lacus. Nullam hendrerit augue vestibulum purus blandit, ac pulvinar elit posuere. ", Type = "item", Path = new Uri("ms-appx:///Assets/placeHolderSponsor.png")},
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

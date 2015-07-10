namespace ClubCloud.Afhangen.DesignViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Core.Prism;
    using ClubCloud.Core.Prism.Interfaces;
    
    
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public class ReserveringPageDesignViewModel : IView
    {
        public ReserveringPageDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            ReserveringType = "Reservering Maken";
        }

        public bool KanBevestigen { get; private set; }
        public bool KanVerwijderen { get; private set; }
        public string ReserveringType { get; private set; }
        public ObservableCollection<Speler> Spelers { get; private set; }

        public int AantalSpelers { get; private set; }
        public Baan Baan { get; private set; }

        public Guid BaanId { get; private set; }

        public TimeSpan BeginTijd { get; private set; }
        public DateTime Datum { get; private set; }

        public TimeSpan Duur { get; private set; }

        public TimeSpan EindTijd { get; private set; }

        public bool Final { get; private set; }

        public Guid Id { get; private set; }
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
    }
}

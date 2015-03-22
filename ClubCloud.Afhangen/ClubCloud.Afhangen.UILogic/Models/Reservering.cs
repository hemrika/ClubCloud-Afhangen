using ClubCloud.Afhangen.UILogic.ViewModels;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.StoreApps;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Reservering : BindableBase
    {
        public Reservering(ObservableCollection<Speler> spelers)
        {
            Spelers = spelers;
        }

        public Reservering()
        {
            Spelers = new ObservableCollection<Speler>();
        }

        [DataMember]
        public ObservableCollection<Speler> Spelers { get; set; }

        [DataMember]
        public System.Guid Id { get; set; }

        [DataMember]
        public Baan Baan { get; set; }

        [DataMember]
        public string Beschrijving { get; set; }

        [DataMember]
        public System.Guid? BaanId { get; set; }

        [DataMember]
        public System.DateTime Datum { get; set; }

        [DataMember]
        public System.TimeSpan BeginTijd { get; set; }

        [DataMember]
        public System.TimeSpan Duur { get; set; }

        [DataMember]
        public System.TimeSpan EindTijd { get; set; }

        [DataMember]
        public bool Final { get; set; }

        [DataMember]
        public ReserveringSoort Soort { get; set; }

    }
}

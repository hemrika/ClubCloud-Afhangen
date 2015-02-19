using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.StoreApps;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Baan : BindableBase
    {
        [DataMember]
        public System.Guid Id { get; set; }

        [DataMember]
        public string Naam { get; set; }

        [DataMember]
        public int Nummer { get; set; }

        [DataMember]
        public Guid AccommodatieId { get; set; }

        [DataMember]
        public Guid BaanblokId { get; set; }

        [DataMember]
        public string Locatie { get; set; }

        [DataMember]
        public bool Verlichting { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Soort { get; set; }
    }
}

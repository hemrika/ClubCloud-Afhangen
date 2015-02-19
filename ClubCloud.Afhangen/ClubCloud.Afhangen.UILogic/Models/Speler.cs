using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.StoreApps;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Speler : BindableBase
    {
        [DataMember]
        public System.Guid Id { get; set; }

        [DataMember]
        public string Achternaam { get; set; }

        [DataMember]
        public string Bondsnummer { get; set; }

        [DataMember]
        public string Roepnaam { get; set; }

        [DataMember]
        public string Tussenvoegsel { get; set; }
    } 
}

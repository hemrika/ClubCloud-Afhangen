using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.StoreApps;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClubCloud.Afhangen.UILogic.Models
{
    public class Vereniging : BindableBase
    {
        [DataMember]
        public System.Guid Id { get; set; }

        [DataMember]
        public string Naam { get; set; }

        [DataMember]
        public string Nummer { get; set; }
    } 
}

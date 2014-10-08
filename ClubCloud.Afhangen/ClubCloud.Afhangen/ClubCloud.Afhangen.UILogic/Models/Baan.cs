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
        public string Baansoort { get; set; }
    }
}

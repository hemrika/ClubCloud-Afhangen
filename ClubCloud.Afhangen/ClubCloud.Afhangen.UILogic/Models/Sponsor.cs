using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Sponsor : BindableBase
    {
        [DataMember]
        public System.Guid Id { get; set; }

        [DataMember]
        public string Naam { get; set; }

        [DataMember]
        public string Type { get; set; }


        [DataMember]
        public Uri Path { get; set; }

        public string Tekst { get; set; }

        public Guid VerenigingId { get; set; }

        public Guid? AfbeeldingId { get; set; }
    } 
}

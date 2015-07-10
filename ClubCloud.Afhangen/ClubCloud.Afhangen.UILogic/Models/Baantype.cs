using ClubCloud.Core.Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Baantype : BindableBase
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Naam { get; set; }

        [DataMember]
        public string Beschrijving { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Meervoud { get; set; }

        [DataMember]
        public string Omschrijving { get; set; }
    }
}

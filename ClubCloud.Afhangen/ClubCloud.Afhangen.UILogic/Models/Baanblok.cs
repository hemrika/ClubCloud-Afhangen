using ClubCloud.Core.Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Baanblok : BindableBase
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Naam { get; set; }

        [DataMember]
        public Guid? BaansoortId { get; set; }

        [DataMember]
        public Guid? BaantypeId { get; set; }

        [DataMember]
        public string Locatie { get; set; }

        [DataMember]
        public bool Verlichting { get; set; }

        [DataMember]
        public Guid? AccommodatieId { get; set; }
    }
}

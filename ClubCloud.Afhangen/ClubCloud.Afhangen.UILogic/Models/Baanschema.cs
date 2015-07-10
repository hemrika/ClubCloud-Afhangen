using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Baanschema : BindableBase
    {
        [DataMember]
        public System.Guid Id { get; set; }

        [DataMember]
        public string Naam { get; set; }

        [DataMember]
        public Guid? BaanId { get; set; }

        [DataMember]
        public bool Beschikbaar { get; set; }

        [DataMember]
        public DayOfWeek Dag { get; set; }

        [DataMember]
        public TimeSpan DagBegin { get; set; }

        [DataMember]
        public TimeSpan DagEinde { get; set; }
    }
}

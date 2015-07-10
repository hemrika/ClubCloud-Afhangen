using ClubCloud.Core.Prism;
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

        [DataMember]
        public System.Guid AccommodatieId { get; set; }
    } 
}

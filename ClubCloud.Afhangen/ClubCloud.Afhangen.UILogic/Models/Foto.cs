using ClubCloud.Core.Prism;
using ClubCloud.Core.Prism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Foto : BindableBase
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int FotoId { get; set; }

        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public double ContentLength { get; set; }

        [DataMember]
        public byte[] ContentData { get; set; }

        [DataMember]
        public string Path { get; set; }

    }
}

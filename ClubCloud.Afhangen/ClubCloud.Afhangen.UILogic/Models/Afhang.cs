using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Afhang : BindableBase
    {
        public Guid Id { get; set; }

        public Guid? VerenigingId { get; set; }

        internal ClubCloudAfhangen.Month MaandEinde { get; set; }

        internal ClubCloudAfhangen.Month MaandBegin { get; set; }

        public int Duur_Vier { get; set; }

        public int Duur_Twee { get; set; }

        public int Duur_Precisie { get; set; }

        public int Duur_Een { get; set; }

        public int Duur_Drie { get; set; }

        public TimeSpan BaanEinde { get; set; }

        public TimeSpan BaanBegin { get; set; }
    }
}

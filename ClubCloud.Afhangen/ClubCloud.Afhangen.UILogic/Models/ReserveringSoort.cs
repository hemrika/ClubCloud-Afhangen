using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Models
{
    public enum ReserveringSoort : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Afhangen = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Les = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Competitie = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Toernooi = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Evenement = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Onderhoud = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Seizoen = 7,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Mobiel = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Overig = 8,
    }
}

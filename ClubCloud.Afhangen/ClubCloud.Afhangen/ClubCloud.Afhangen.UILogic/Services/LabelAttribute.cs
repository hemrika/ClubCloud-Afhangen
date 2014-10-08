using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public class LabelAttribute : Attribute
    {
        private String _label;

        public LabelAttribute(String label)
        {
            this._label = label;
        }

        public static String GetEnumLabelValue(Enum enumeration)
        {
            return enumeration.GetType().GetTypeInfo().GetDeclaredField(enumeration.ToString()).GetCustomAttribute<LabelAttribute>().ToString();
        }

        public override String ToString()
        {
            return this._label;
        }
    }

}
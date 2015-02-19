using System;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class Measurement
    {
        private string _unit;
        public double? NumericValue
        {
            get;
            set;
        }
        [DataMember]
        public double? Value
        {
            get
            {
                if (this.NumericValue.HasValue)
                {
                    return new double?(Math.Round(this.NumericValue.Value, this.Precision));
                }
                return this.NumericValue;
            }
            set
            {
                this.NumericValue = value;
            }
        }
        [DataMember]
        public string Unit
        {
            get
            {
                if (string.IsNullOrEmpty(this._unit))
                {
                    return Enum.GetName(typeof(UnitTypes), this.UnitTypeValue);
                }
                return this._unit;
            }
            set
            {
                this._unit = value;
            }
        }
        [IgnoreDataMember]
        public UnitTypes UnitTypeValue
        {
            get;
            set;
        }
        [DataMember]
        public int UnitType
        {
            get
            {
                return (int)this.UnitTypeValue;
            }
            set
            {
                this.UnitTypeValue = (UnitTypes)value;
            }
        }
        [IgnoreDataMember]
        public int Precision
        {
            get;
            set;
        }
        public Measurement()
        {
            this.Precision = 2;
        }
    }
}

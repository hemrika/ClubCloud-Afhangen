using System.ComponentModel;
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
	public class TemperatureRange :  INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private Measurement minimum;

        [DataMember]
		public Measurement Minimum
		{
            get { return minimum; }
            set
            {
                minimum = value;
                RaisePropertyChanged("Minimum");
            }
        }

        private Measurement maximum;

		[DataMember]
		public Measurement Maximum
		{
            get { return maximum; }
            set
            {
                maximum = value;
                RaisePropertyChanged("Maximum");
            }
		}
	}
}

/*
using System.Runtime.Serialization;
namespace ClubCloud.Afhangen.UILogic.Models
{
	[DataContract]
    public class TemperatureRange
	{
        [DataMember]
        public Measurements Minimum
		{
			get;
			set;
		}
		[DataMember]
        public Measurements Maximum
		{
			get;
			set;
		}
	}
}
*/
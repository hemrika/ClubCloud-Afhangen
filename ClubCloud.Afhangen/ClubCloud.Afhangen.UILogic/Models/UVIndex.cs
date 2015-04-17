using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ClubCloud.Afhangen.UILogic.Models
{
    [DataContract]
    public class UVIndex :  INotifyPropertyChanged
    {
        public UVIndex()
        {
        }

        public UVIndex(Uri image, string description, string spf)
        {
            try
            {
                Image = new BitmapImage(image);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            Description = description;
            SPF = spf;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private ImageSource image;

        [DataMember]
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                RaisePropertyChanged("Image");
            }
        }

        private string description;

        [DataMember]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }

        private string _SPF;

        public string SPF
        {
            get { return _SPF; }
            set
            {
                _SPF = value;
                RaisePropertyChanged("SPF");
            }

        }
    }
}

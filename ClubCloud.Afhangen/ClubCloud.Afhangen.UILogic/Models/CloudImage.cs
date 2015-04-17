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
    public class CloudImage :  INotifyPropertyChanged
    {
        public CloudImage()
        {
        }

        public CloudImage(Uri image)
        {
            try
            {
                Image = new BitmapImage(image);
                Ticks = DateTime.Now.Ticks;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private long ticks;

        public long Ticks
        {
            get { return ticks; }
            set
            {
                ticks = value;
                RaisePropertyChanged("Ticks");
            }
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
    }
}

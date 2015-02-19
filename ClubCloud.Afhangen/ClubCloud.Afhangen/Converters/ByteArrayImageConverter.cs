using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace ClubCloud.Afhangen.Converters
{
    public class ByteArrayImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value.GetType() != typeof(BitmapImage))
            {
                var imageBytes = (byte[])value;
                return ConvertToImage(imageBytes).Result;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            //throw new NotImplementedException();
            return value;
        }

        private static async Task<BitmapImage> ConvertToImage(byte[] imageBytes)
        {
            var image = new BitmapImage();
            using (var randomAccessStream = new InMemoryRandomAccessStream())
            {
                var dw = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
                dw.WriteBytes(imageBytes);
                await dw.StoreAsync();
                image.SetSource(randomAccessStream);
            }
            return image;
        }
    }
}

using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Services;
using ClubCloud.Core.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public class SpelerRepository : ISpelerRepository
    {
        public const string BaanIdKey = "BaanId";
        private readonly ISpelerService _spelerService;
        private readonly IEventAggregator _eventAggregator;

        private List<Speler> _cachedSpelers = null;

        public SpelerRepository(ISpelerService spelerService, IEventAggregator eventAggregator)
        {
            _spelerService = spelerService;
            _eventAggregator = eventAggregator;
        }

        public async Task<Models.Speler> GetSpelerAsync(Guid verenigingId, Guid spelerId)
        {
            if (_cachedSpelers == null) _cachedSpelers = new List<Speler>();

            Speler speler = null;
            if (_cachedSpelers != null) speler = _cachedSpelers.SingleOrDefault(s => s.Id == spelerId);

            if (speler == null)
            {
                speler = await _spelerService.GetSpelerAsync(verenigingId, spelerId);
                //if(_cachedSpelers.IndexOf(speler) <0 )
                if (_cachedSpelers.Where(s => s.Id == speler.Id).Count() == 0)
                    _cachedSpelers.Add(speler);

                //RaiseSpelerUpdated();
            }
            return _cachedSpelers.SingleOrDefault(s => s.Id == spelerId);
        }

        public async Task<Models.Speler> GetSpelerByNummerAsync(Guid verenigingId, string nummer)
        {
            if (_cachedSpelers == null) _cachedSpelers = new List<Speler>();

            Speler speler = null;
            if (_cachedSpelers != null) speler = _cachedSpelers.SingleOrDefault(s => s.Bondsnummer == nummer);

            if (speler == null)
            {
                speler = await _spelerService.GetSpelerByNummerAsync(verenigingId, nummer);
                //if(!_cachedSpelers.Contains(speler)) _cachedSpelers.Add(speler);
                if (_cachedSpelers.Where(s => s.Id == speler.Id).Count() == 0)
                    _cachedSpelers.Add(speler);

                //RaiseSpelerUpdated();
            }
            return _cachedSpelers.SingleOrDefault(s => s.Bondsnummer == nummer);

        }

        /*
        public async Task<Models.Foto> GetFotoByNummerAsync(Guid verenigingId, string nummer)
        {
            Foto foto = new Foto();

            string filename = nummer + ".jpg";
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFolder fotos = await folder.CreateFolderAsync("Fotos", CreationCollisionOption.OpenIfExists);
            StorageFile image = await fotos.TryGetItemAsync(filename) as StorageFile;

            try
            {

                if (image == null)
                {
                    foto = await _spelerService.GetFotoByNummerAsync(verenigingId, nummer);
                    if (foto != null)
                    {
                        image = await fotos.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                        IBuffer writebuffer = GetBufferFromContentData(foto.ContentData);
                        await Windows.Storage.FileIO.WriteBufferAsync(image, writebuffer);
                        foto.Path = image.Path;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            try
            {
                IBuffer readbuffer = await FileIO.ReadBufferAsync(image);

                if (foto.ContentData != null)
                    foto.ContentData = new byte[readbuffer.Length];

                foto.ContentData = readbuffer.ToArray();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return foto;
        }
        */

        public async Task<Foto> GetFotoAsync(Guid verenigingId, Guid gebruikerId)
        {
            Foto foto = new Foto();
            StorageFile image = null;
            Speler speler = _cachedSpelers.SingleOrDefault(s => s.Id == gebruikerId);

            if (speler != null)
            {
                string filename = speler.Bondsnummer + ".jpg";
                StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFolder fotos = await folder.CreateFolderAsync("Fotos", CreationCollisionOption.OpenIfExists);
                image = await fotos.TryGetItemAsync(filename) as StorageFile;

                try
                {

                    if (image == null)
                    {
                        foto = await _spelerService.GetFotoAsync(verenigingId, gebruikerId);

                        if (foto != null)
                        {
                            image = await fotos.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                            IBuffer writebuffer = GetBufferFromContentData(foto.ContentData);
                            await Windows.Storage.FileIO.WriteBufferAsync(image, writebuffer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }

                try
                {
                    IBuffer readbuffer = await FileIO.ReadBufferAsync(image);

                    if (foto.ContentData != null)
                        foto.ContentData = new byte[readbuffer.Length];

                    foto.ContentData = readbuffer.ToArray();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }

            return foto;
        }

        private IBuffer GetBufferFromContentData(byte[] bytes)
        {
            using (InMemoryRandomAccessStream memoryStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(memoryStream))
                {
                    writer.WriteBytes(bytes);
                    return writer.DetachBuffer();
                }
            }
        } 

        /*
        private void RaiseSpelerUpdated()
        {
            // Documentation on loosely coupled communication is at http://go.microsoft.com/fwlink/?LinkID=288820&clcid=0x409 
            _eventAggregator.GetEvent<SpelerUpdatedEvent>().Publish(null);
        }
        */

        /*
        private async Task LoadImage()
        {
            var file = await ApplicationData.Current.LocalFolder..GetFileAsync("text.png");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(stream);
            image.Source = bitmapImage;
        }

        private async Task SaveImage()
        {
            var renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(text);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("text.png", CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight, 96d, 96d,
                    pixelBuffer.ToArray());

                await encoder.FlushAsync();
            }
        }
        */
    }
}

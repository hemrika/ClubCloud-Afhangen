using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class SponsorRepository : ISponsorRepository
    {
        private readonly ISponsorService _sponsorService;
        private readonly IVerenigingRepository _verenigingRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly ISessionStateService _sessionStateService;
        private static DateTime _today;
        private ObservableCollection<Sponsor> _cachedSponsors = null;


        public SponsorRepository(ISponsorService sponsorService, IVerenigingRepository verenigingRepository, IEventAggregator eventAggregator, ISessionStateService sessionStateService)
        {
            _sponsorService = sponsorService;
            _eventAggregator = eventAggregator;
            _sessionStateService = sessionStateService;
            _verenigingRepository = verenigingRepository;
        }

        public async Task<Models.Sponsor> GetSponsorAsync(Guid verenigingId, Guid sponsorId)
        {
            if (_cachedSponsors == null) _cachedSponsors = new ObservableCollection<Sponsor>();

            Sponsor sponsor = null;
            if (_cachedSponsors != null) sponsor = _cachedSponsors.SingleOrDefault(s => s.Id == sponsorId);

            if (sponsor == null)
            {
                sponsor = await _sponsorService.GetSponsorAsync(verenigingId, sponsorId);
                //if(_cachedSpelers.IndexOf(speler) <0 )
                if (_cachedSponsors.Where(s => s.Id == sponsor.Id).Count() == 0)
                    _cachedSponsors.Add(sponsor);

                //RaiseSpelerUpdated();
            }
            return _cachedSponsors.SingleOrDefault(s => s.Id == sponsorId);
        }

        private static Uri _baseUri = new Uri("ms-appdata:///temp/Sponsors/"); 

        public async Task<ObservableCollection<Sponsor>> GetSponsorsAsync(Guid verenigingId)
        {
            if (_cachedSponsors == null || (_today == null || _today < DateTime.Today))
            {
                _cachedSponsors = new ObservableCollection<Sponsor>();

                Vereniging vereniging = await _verenigingRepository.GetVerenigingAsync();
                List<Sponsor> sponsoren = await _sponsorService.GetSponsorenAsync(vereniging.Id);

                foreach (Sponsor sponsor in sponsoren)
                {
                    if (_cachedSponsors.Count(s => s.Id == sponsor.Id) == 0)
                    {
                        Foto foto = await GetSponsorImageAsync(verenigingId, sponsor.Id);
                        sponsor.Path = new Uri(_baseUri,string.Format("{0}.jpg",sponsor.Id));

                        _cachedSponsors.Add(sponsor);
                    }

                }

                //Foto _foto = await GetSponsorImageAsync(verenigingId, Guid.Empty);

                _today = DateTime.Today;
            }

            return _cachedSponsors;
        }

        public async Task<Foto> GetSponsorImageAsync(Guid verenigingId, Guid sponsorId)
        {
            Foto foto = new Foto();
            StorageFile image = null;
            Sponsor sponsor = _cachedSponsors.SingleOrDefault(s => s.Id == sponsorId);

            //StorageFolder _folder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            //StorageFolder _fotos = await _folder.CreateFolderAsync("Sponsors", CreationCollisionOption.OpenIfExists);
            //string _filename = sponsorId + ".jpg";
            //image = await _fotos.CreateFileAsync(_filename, CreationCollisionOption.ReplaceExisting);
            //foto.Path = image.Path;

            if (sponsor != null)
            {
                string filename = sponsor.Id + ".jpg";
                StorageFolder folder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                StorageFolder fotos = await folder.CreateFolderAsync("Sponsors", CreationCollisionOption.OpenIfExists);
                image = await fotos.TryGetItemAsync(filename) as StorageFile;

                try
                {
                    if(image != null)
                    {
                        await image.DeleteAsync(StorageDeleteOption.PermanentDelete);
                    }

                    if (image == null)
                    {
                        foto = await _sponsorService.GetSponsorImageAsync(verenigingId, sponsorId);

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

                /*
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
                */
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

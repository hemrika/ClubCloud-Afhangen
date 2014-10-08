using Microsoft.Practices.Prism.Commands;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class SpelerUserControlDesignViewModel //: IView
    {
        public SpelerUserControlDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Id = Guid.Empty;
            Naam = "Speler";
            Nummer = "00000000";

            StorageFile _storageFile =  Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/placeHolderSpeler.png")).GetResults();

            try
            {
                IBuffer readbuffer = FileIO.ReadBufferAsync(_storageFile).GetResults();
                Foto = readbuffer.ToArray();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            //Foto = new BitmapImage(new Uri("ms-appx:///Assets/placeHolderSpeler.png"));
            Action = "Selecteer Speler";
        }

        public DelegateCommand SelecterenSpelerCommand { get; set; }

        public Guid Id { get; private set; }

        public string Naam { get; private set; }

        public string Nummer { get; private set; }

        public byte[] Foto { get; private set; }

        public string Action { get; private set; }

        /*
        public ObservableCollection<ReserveringViewModel> ReserveringViewModels
        { get; private set; }
        */

        /*
        object IView.DataContext
        {
            get
            {
                return null;
            }
            set
            {
                object dc = value;
            }
        }
        */
    }
}

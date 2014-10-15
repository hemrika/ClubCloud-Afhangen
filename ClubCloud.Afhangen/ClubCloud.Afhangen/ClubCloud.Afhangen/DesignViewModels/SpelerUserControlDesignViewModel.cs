using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class SpelerUserControlDesignViewModel : IView
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

            

            try
            {
                StorageFile _storageFile = Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/placeHolderSpeler.png")).GetResults();
                IBuffer readbuffer = FileIO.ReadBufferAsync(_storageFile).GetResults();
                Foto = readbuffer.ToArray();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            //Foto = new BitmapImage(new Uri("ms-appx:///Assets/placeHolderSpeler.png"));
            ActionName = "Selecteer Speler";
        }

        public string ActionName{ get; private set; } 
        
        public DelegateCommand Action { get; private set; }

        public DelegateCommand SpelerNavigationCommand { get; set; }

        public DelegateCommand SelecterenSpelerCommand { get; set; }

        public DelegateCommand VerwijderenSpelerCommand { get; set; }


        public Guid Id { get; private set; }

        public string Naam { get; private set; }

        public string Nummer { get; private set; }

        public byte[] Foto { get; private set; }

        //public string Action { get; private set; }

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
    }
}

namespace ClubCloud.Afhangen.DesignViewModels
{
    using ClubCloud.Afhangen.UILogic.Models;
    using ClubCloud.Afhangen.UILogic.Repositories;
    using ClubCloud.Core.Prism;
    using ClubCloud.Core.Prism.Interfaces;
    
    
    using System;
    using System.Threading.Tasks;

    public class OptionsFlyoutDesignViewModel : IView
    {
        public OptionsFlyoutDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Time = DateTime.Now.ToString("HH:mm");
        }

        public string Time { get; private set; }

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

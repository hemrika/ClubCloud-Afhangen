using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.ObjectModel;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class BaanschemaPageDesignViewModel : IView
    {
        public BaanschemaPageDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {
            Banen = new ObservableCollection<ResourceType>()
            {
                new ResourceType { TypeName = "Banen", ResourceCollection = new ObservableCollection<Resource>
                {
                    new Resource { TypeName = "Banen", ResourceName = "Baan 1", DisplayName = "Baan 1" },
                    new Resource { TypeName = "Banen", ResourceName = "Baan 2", DisplayName = "Baan 2" },
                    new Resource { TypeName = "Banen", ResourceName = "Baan 3", DisplayName = "Baan 3" },
                    new Resource { TypeName = "Banen", ResourceName = "Baan 4", DisplayName = "Baan 4" },
                    new Resource { TypeName = "Banen", ResourceName = "Baan 5", DisplayName = "Baan 5" },
                    }
                }
            };

            Afhang = new Afhang { BaanBegin = new DateTime().AddHours(7).TimeOfDay, BaanEinde = new DateTime().AddHours(23).TimeOfDay };

            Reserveringen = new ObservableCollection<ScheduleAppointment>()
            {
                new ScheduleAppointment { StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(30), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Afhangen.ToString() }, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(30), EndTime = DateTime.Now.AddMinutes(60), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Competitie.ToString() }, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(60), EndTime = DateTime.Now.AddMinutes(90), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Evenement.ToString()}, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(90), EndTime = DateTime.Now.AddMinutes(120), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Evenement.ToString()}, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(120), EndTime = DateTime.Now.AddMinutes(150), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Les.ToString()}, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(150), EndTime = DateTime.Now.AddMinutes(180), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Mobiel.ToString()}, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(180), EndTime = DateTime.Now.AddMinutes(210), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Onderhoud.ToString()}, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(210), EndTime = DateTime.Now.AddMinutes(240), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Overig.ToString()}, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(240), EndTime = DateTime.Now.AddMinutes(270), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Seizoen.ToString()}, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                new ScheduleAppointment { StartTime = DateTime.Now.AddMinutes(270), EndTime = DateTime.Now.AddMinutes(300), Location = "Baan 1", Status = new ScheduleAppointmentStatus { Status = ReserveringSoort.Toernooi.ToString()}, ResourceCollection = new ObservableCollection<Resource>{ new Resource() { ResourceName = "Baan 1", TypeName = "Banen" } }, Subject = "Afhangen", ReadOnly = true },
                //new Reservering { BeginTijd = DateTime.Now.TimeOfDay, EindTijd = DateTime.Now.AddMinutes(30).TimeOfDay, Soort = UILogic.ClubCloudService.ReserveringSoort.Afhangen, Baan = new Baan{ Naam = "Baan 1"}},
            };
            /*
            Banen = new ObservableCollection<Baan>(){
                new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 1", Nummer =1},
                new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 2", Nummer =2},
                new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 3", Nummer =3},
                new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 4", Nummer =4},
                new Baan{ Verlichting = true, Type = "type", Soort = "soort", Locatie = "Buiten", Id = Guid.NewGuid(), Naam = "Baan 5", Nummer =5},
            };
            */
        }

        public DelegateCommand<Syncfusion.UI.Xaml.Schedule.VisibleDatesChangingEventArgs> VisibleDatesChangingCommand { get; set; }

        public Afhang Afhang { get; private set; }
        public ObservableCollection<ResourceType> Banen { get; private set; }
        public ObservableCollection<ScheduleAppointment> Reserveringen { get; private set; }
        //public ObservableCollection<Baan> Banen { get; private set; }

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

        public object DataContext
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

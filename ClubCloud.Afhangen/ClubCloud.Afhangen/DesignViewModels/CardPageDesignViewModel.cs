using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace ClubCloud.Afhangen.DesignViewModels
{
    public class CardPageDesignViewModel //: IView
    {
        public CardPageDesignViewModel()
        {
            FillWithDummyData();
        }

        private void FillWithDummyData()
        {

            Reserveringen = new ObservableCollection<Reservering>(){
                new Reservering{ Baan = new Baan{ Id = Guid.Empty, Naam= "Baan1 (Buiten)", Nummer =1}, BeginTijd = DateTime.Now.TimeOfDay, Datum = DateTime.Now.Date, Duur = TimeSpan.FromMinutes(45), EindTijd = DateTime.Now.TimeOfDay, Final = false, 
                    Spelers = new ObservableCollection<Speler>(){
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                    } 
                },
                                new Reservering{ Baan = new Baan{ Id = Guid.Empty, Naam= "Baan1 (Buiten)", Nummer =1}, BeginTijd = DateTime.Now.TimeOfDay, Datum = DateTime.Now.Date, Duur = TimeSpan.FromMinutes(45), EindTijd = DateTime.Now.TimeOfDay, Final = false, 
                    Spelers = new ObservableCollection<Speler>(){
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                    } 
                },
                                new Reservering{ Baan = new Baan{ Id = Guid.Empty, Naam= "Baan1 (Buiten)", Nummer =1}, BeginTijd = DateTime.Now.TimeOfDay, Datum = DateTime.Now.Date, Duur = TimeSpan.FromMinutes(45), EindTijd = DateTime.Now.TimeOfDay, Final = false, 
                    Spelers = new ObservableCollection<Speler>(){
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                        new Speler { Id = Guid.Empty},
                    } 
                }
            };
            

            Message = "Haal uw kaart door de lezer.";
            Index = 0;
            Huidig = true;
            Bestaand = true;
        }

        public DelegateCommand GoNextCommand { get; set; }
        public int  Index { get; private set; }
        public string CardInput { get; private set; }
        public string Message { get; private set; }

        public string CardOuput { get; private set; }

        public bool Huidig { get; private set; }
        public bool Bestaand { get; private set; }

        public ObservableCollection<Reservering> Reserveringen { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Projekat2.Tip;
using Projekat2.Etiketa;
using Projekat2.Vrsta;

namespace Projekat2
{
    public class Podaci : INotifyPropertyChanged
    {
        private static Podaci instance = null;

        private List<EtiketaKlasa> etikete;
        private List<TipKlasa> tipovi;
        private List<VrstaKlasa> vrste;

        private Podaci()
        {
            this.etikete = new List<EtiketaKlasa>();
            this.tipovi = new List<TipKlasa>();
            this.vrste = new List<VrstaKlasa>();
        }

        public static Podaci getInstance()
        {
            if (instance == null)
                instance = new Podaci();
            return instance;
        }

        public List<EtiketaKlasa> Etikete
        {
            get
            {
                return etikete;
            }

            set
            {
                if (value != etikete)
                {
                    etikete = value;
                    OnPropertyChanged("Etikete");
                }
            }
        }

        public List<TipKlasa> Tipovi
        {
            get
            {
                return tipovi;
            }

            set
            {
                if (value != tipovi)
                {
                    tipovi = value;
                    OnPropertyChanged("Tipovi");
                }
            }
        }

        public List<VrstaKlasa> Vrste
        {
            get
            {
                return vrste;
            }

            set
            {
                if (value != vrste)
                {
                    vrste = value;
                    OnPropertyChanged("Vrste");
                }
            }
        }

        //cuvanje novih kordinata vrste pri pomeranju ikonice na kanvasu
        public static void ChangeDroppedVrsta(VrstaKlasa vrsta)
        {
            foreach (VrstaKlasa v in instance.Vrste)
            {
                if (v.Oznaka == vrsta.Oznaka)
                {
                    v.X = vrsta.X;
                    v.Y = vrsta.Y;
                    break;
                }
            }
            SerijalizacijaVrste.serijalizacijaVrste();
        }   

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}

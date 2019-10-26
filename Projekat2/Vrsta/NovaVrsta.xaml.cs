using Microsoft.Win32;
using Projekat2.Dijalog;
using Projekat2.Etiketa;
using Projekat2.Tip;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekat2.Vrsta
{
    /// <summary>
    /// Interaction logic for NovaVrsta.xaml
    /// </summary>
    public partial class NovaVrsta : Window, INotifyPropertyChanged
    {
        private ViewModel vm;
        private ObservableCollection<VrstaKlasa> vrste;

        public class ViewModel
        {
            public VrstaKlasa Vrsta { get; set; }   //nova vrsta koju cu da ubacujem u podatke

            public List<TipKlasa> sviTipovi { get; set; }   //svi tagovi koje sam sacuvao/ ucitavam ih iz podataka
            public List<EtiketaKlasa> sveEtikete { get; set; }

            public List<ListBoxItem> prikEtikete { get; set; }  //lista etiketa
            public List<EtiketaKlasa> selektovane { get; set; }     //etikete koje su selektovane
        }

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public NovaVrsta()
        {
            InitializeComponent();

            this.DataContext = this;
            OznakaBox.Focus();
        }

        public NovaVrsta(ObservableCollection<VrstaKlasa> vr)
        {
            InitializeComponent();
            vrste = new ObservableCollection<VrstaKlasa>();
            vrste = vr;

            vm = new ViewModel();
            vm.Vrsta = new VrstaKlasa();    //nova vrsta koju cu da dodajem

            vm.sviTipovi = Podaci.getInstance().Tipovi;     //ucitavam sve tipove iz podataka
            vm.sveEtikete = Podaci.getInstance().Etikete;

            vm.prikEtikete = new List<ListBoxItem>();
            vm.selektovane = new List<EtiketaKlasa>();

            ucitavanjeTipova();
            ucitavanjeEtiketa();

            this.DataContext = vm;

            OznakaBox.Focus();
        }

        private void ucitavanjeTipova()
        {
            foreach (TipKlasa tip in vm.sviTipovi)  //combobox popunjavam svim tipovima
            {
                cmbTip.Items.Add(tip.Oznaka/* + " - " + tip.Ime*/); //ispis oznake i imena u comboboxu
            }
        }

        private void ucitavanjeEtiketa()
        {
            foreach(EtiketaKlasa etiketa in vm.sveEtikete)
            {
                //https://stackoverflow.com/questions/13267657/adding-a-listboxitem-in-a-listbox-in-c
                ListBoxItem itm = new ListBoxItem();
                itm.Content = etiketa.Oznaka;
                listaEtiketa.Items.Add(itm);    // kad se radi sa listboxom bez bindinga potrebno je proslediti i listbox iteme
                vm.prikEtikete.Add(itm);
            }
        }

        #region Komande za Sacuvaj i Odustani
        private void SacuvajVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SacuvajVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (OznakaBox.Text != "" && boxIme.Text != "" && boxOpis.Text != "" && cmbTip.Text != null && boxStUgr.Text != null && boxTurSt.Text != null)
            {
                for(int i = 0; i < vm.prikEtikete.Count; i++)
                {
                    if(vm.prikEtikete[i].IsSelected)    //ako je etiketa selektovana dodajem je u listu
                    {
                        vm.selektovane.Add(vm.sveEtikete[i]); 
                    }
                }
                vm.Vrsta.Etikete = vm.selektovane;  //samo one koje su selektovane ce biti sacuvane

                if (Ikonica.Source == null) //za preuzimanje ikonice od tipa ukoliko se ne doda
                {
                    SerijalizacijaTipa.deserijalizacijaTipa();
                    foreach (TipKlasa tip in Podaci.getInstance().Tipovi)
                    {
                        if (tip.Oznaka.Equals(vm.Vrsta.Tip))
                        {
                            vm.Vrsta.Ikonica = tip.Ikonica;
                        }
                    }
                }

                Podaci.getInstance().Vrste.Add(vm.Vrsta);
                SerijalizacijaVrste.serijalizacijaVrste();

                vrste.Add(vm.Vrsta);
                MessageBox.Show("Podaci o vrsti su uspešno sačuvani.");

                this.Close();

            }
            else
                MessageBox.Show("Niste popunili sva obavezna polja!");
        }

        private void OdustaniVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OdustaniVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnIkonica_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Izaberite ikonicu";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                Ikonica.Source = new BitmapImage(new Uri(op.FileName));
                vm.Vrsta.Ikonica = op.FileName;
            }
        }

        private void btnIkonicaUkloni_Click(object sender, RoutedEventArgs e)
        {
            vm.Vrsta.Ikonica = "";
        }

        private void btnNoviTip_Click(object sender, RoutedEventArgs e)
        {
            int poc = Podaci.getInstance().Tipovi.Count;    //na pocetku proverim duzinu liste

            var s = new NoviTip();
            if (s.ShowDialog().Equals(true)) { }

            int kraj = Podaci.getInstance().Tipovi.Count;   //kad se zatvori dijalog proverim duzinu liste

            if (poc != kraj)     //ukoliko se kraj i pocetak razlikuju znaci da je nesto dodato, te dodajem poslednji kako ne bi pravio duplikate tipova
            {
                cmbTip.Items.Add(Podaci.getInstance().Tipovi.Last().Oznaka/* + " - " + Podaci.getInstance().Tipovi.Last().Ime*/);   //na postojecu listu tipova dodaje zadnje dodati tip tj novi tip
            }
        }

        private void Nova_Etiketa(object sender, RoutedEventArgs e)
        {
            var w = new Etiketa.NovaEtiketa();
            w.ShowDialog();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            var w = new Help.Pomoc();
            w.ShowDialog();
        }
    }
}

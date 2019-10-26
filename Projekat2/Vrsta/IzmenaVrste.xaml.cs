using Microsoft.Win32;
using Projekat2.Etiketa;
using Projekat2.Tip;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for IzmenaVrste.xaml
    /// </summary>
    public partial class IzmenaVrste : Window
    {
        private ViewModel vm;
        public int etBr = 0;

        public class ViewModel
        {
            public VrstaKlasa Vrsta { get; set; }
            public string stVrsta { get; set; } //da li postoji vrsta sa tom oznakom u fajlu

            public List<EtiketaKlasa> sveEtikete { get; set; }      //sve etikete koje sam sacuvao
            public List<TipKlasa> sviTipovi { get; set; }    //svi tagovi koje sam sacuvao

            public List<EtiketaKlasa> selektovane { get; set; } //selektovane etikete
            public List<ListBoxItem> prikEtikete { get; set; }  //lista etiketa
        }

        public IzmenaVrste()
        {
            InitializeComponent();

            this.DataContext = this;
            OznakaBox.Focus();
        }

        public IzmenaVrste(VrstaKlasa vrsta)
        {
            InitializeComponent();
            vm = new ViewModel();

            vm.Vrsta = new VrstaKlasa();
            vm.Vrsta = vrsta;   //dodjeljujem prosljedjenu vrstu

            vm.stVrsta = vrsta.Oznaka;

            vm.sviTipovi = new List<TipKlasa>();
            vm.sviTipovi = Podaci.getInstance().Tipovi;

            vm.sveEtikete = new List<EtiketaKlasa>();
            vm.sveEtikete = Podaci.getInstance().Etikete;

            vm.selektovane = new List<EtiketaKlasa>();
            vm.prikEtikete = new List<ListBoxItem>();

            ucitavanjeTipova();
            ucitavanjeEtiketa();

            vm.Vrsta.Etikete = vm.selektovane;
            vm.Vrsta.Ikonica = vrsta.Ikonica;

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
            foreach (EtiketaKlasa etiketa in vm.sveEtikete)
            {
                //https://stackoverflow.com/questions/13267657/adding-a-listboxitem-in-a-listbox-in-c
                ListBoxItem itm = new ListBoxItem();
                itm.Content = etiketa.Oznaka;
                listaEtiketa.Items.Add(itm);    // kad se radi sa listboxom bez bindinga potrebno je proslediti i listbox iteme
                vm.prikEtikete.Add(itm);

                foreach (EtiketaKlasa oznacenaEti in vm.Vrsta.Etikete)  //prolazi kroz sve etikete i proverava koja je selektovana
                {
                    if (oznacenaEti.Oznaka == etiketa.Oznaka)
                    {
                        itm.IsSelected = true;
                        break;
                    }
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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

        private void Sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            List<VrstaKlasa> vrste = new List<VrstaKlasa>();

            for (int i = 0; i < vm.prikEtikete.Count; i++)
            {
                if (vm.prikEtikete[i].IsSelected)    //ako je etiketa selektovana dodajem je u listu
                {
                    vm.selektovane.Add(vm.sveEtikete[i]);
                }
            }
            vm.Vrsta.Etikete = vm.selektovane;  //samo one koje su selektovane ce biti sacuvane

            foreach (VrstaKlasa vrsta in Podaci.getInstance().Vrste)
            {
                if (vrsta.Oznaka == vm.stVrsta) //trazi vrstu koju azuriramo
                {
                    vrste.Add(vm.Vrsta);
                }
                else
                {
                    vrste.Add(vrsta);
                }
            }

            Podaci.getInstance().Vrste = vrste;
            SerijalizacijaVrste.serijalizacijaVrste();
            this.Close();
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
    }
}

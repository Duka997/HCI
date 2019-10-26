using Projekat2.Etiketa;
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

namespace Projekat2.Etiketa
{
    /// <summary>
    /// Interaction logic for NovaEtiketa.xaml
    /// </summary>
    public partial class NovaEtiketa : Window
    {
        private ViewModel vm;
        ObservableCollection<EtiketaKlasa> etikete;

        public class ViewModel
        {
            public EtiketaKlasa Etiketa { get; set; }
        }

        public NovaEtiketa()
        {
            InitializeComponent();

            vm = new ViewModel();
            vm.Etiketa = new EtiketaKlasa();
            
            this.DataContext = vm;

            OznakaBox.Focus();
        }

        //potrebno je za azuriranje tabele
        public NovaEtiketa(ObservableCollection<EtiketaKlasa> etiketa)
        {
            InitializeComponent();

            etikete = new ObservableCollection<EtiketaKlasa>();
            etikete = etiketa;

            vm = new ViewModel();
            vm.Etiketa = new EtiketaKlasa();    //nova etiketa koji cu da dodajem

            this.DataContext = vm;

            OznakaBox.Focus();
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

        private void Button_Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Sacuvaj(object sender, RoutedEventArgs e)
        {
            if (OznakaBox.Text != "" && OpisBox.Text != "" && BojaBox != null)
            {
                Podaci.getInstance().Etikete.Add(vm.Etiketa);   //u listu etiketa dodaje etiketu
                vm.Etiketa.Boja = BojaBox.SelectedColor.Value;
                SerijalizacijaEtikete.serijalizacijaEtikete();

                if(etikete != null) //pravljenje nove etikete iz nove vrste listboxom ne poziva konstruktor sa parametrom pa dodavanje vm-a nije moguce
                    etikete.Add(vm.Etiketa);

                MessageBox.Show("Podaci o etiketi su uspešno sačuvani.");

                this.Close();
            }
            else
                MessageBox.Show("Niste popunili sva polja!");
        }

        
    }
}

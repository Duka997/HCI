using Projekat2.Vrsta;
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

namespace Projekat2.Tip
{
    /// <summary>
    /// Interaction logic for ListaTipova.xaml
    /// </summary>
    public partial class ListaTipova : Window
    {
        public ListaTipova()
        {
            InitializeComponent();
            TabelaTipova.ItemsSource = Podaci.getInstance().Tipovi;
            this.DataContext = this;
        }

        private void dodajAkcija(object sender, RoutedEventArgs e)
        {
            NoviTip w1 = new NoviTip();
            w1.ShowDialog();
            TabelaTipova.Items.Refresh();
        }

        private void odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void izmeniAkcija(object sender, RoutedEventArgs e)
        {
            if (TabelaTipova.SelectedItem != null)
            {
                TipKlasa tip = (TipKlasa)TabelaTipova.SelectedItem;

                IzmeniTip w1 = new IzmeniTip(tip);
                w1.ShowDialog();
                TabelaTipova.Items.Refresh();
            }

            else
            {
                MessageBox.Show("Niste selektovali tip");
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            Boolean prekid = false;
            foreach (TipKlasa tip in Podaci.getInstance().Tipovi.ToList())
            {
                if (tip.Equals(TabelaTipova.SelectedItem))
                {
                    foreach (VrstaKlasa vrsta in Podaci.getInstance().Vrste)
                    {
                        if (tip.Oznaka == vrsta.Tip)
                        {
                            MessageBox.Show("Nije moguće obrisati tip jer je povezan sa vrstom " + vrsta.Oznaka + " !");
                            prekid = true;
                            break;
                        }
                    }

                    if(prekid == false) { 
                        MessageBoxResult msg = MessageBox.Show("Da li ste sigurni da želite da obrišete selektovani tip?", "Potvrda brisanja tipa", MessageBoxButton.YesNo);

                        if (msg == MessageBoxResult.Yes)
                        {
                            Podaci.getInstance().Tipovi.Remove(tip);
                            SerijalizacijaTipa.serijalizacijaTipa();
                            TabelaTipova.Items.Refresh();

                        }
                    }
                }
            }
        }

        //pretraga po oznaci
        private void txtOznaka_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(TabelaTipova.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    TipKlasa vrsta = o as TipKlasa;
                    return (vrsta.Oznaka.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }
    }
}

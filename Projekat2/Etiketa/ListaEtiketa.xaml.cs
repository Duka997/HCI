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
    /// Interaction logic for ListaEtiketa.xaml
    /// </summary>
    public partial class ListaEtiketa : Window
    {
        public ListaEtiketa()
        {
            InitializeComponent();

            TabelaEtiketa.ItemsSource = Podaci.getInstance().Etikete;
            this.DataContext = this;
        }

        private void dodajAkcija(object sender, RoutedEventArgs e)
        {
            NovaEtiketa w1 = new NovaEtiketa();
            w1.ShowDialog();
            TabelaEtiketa.Items.Refresh();
        }

        private void odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            foreach (EtiketaKlasa etiketa in Podaci.getInstance().Etikete.ToList())
            {
                if (etiketa.Equals(TabelaEtiketa.SelectedItem))
                {
                    MessageBoxResult msg = MessageBox.Show("Da li ste sigurni da želite da obrišete selektovanu etiketu?", "Potvrda brisanja etikete", MessageBoxButton.YesNo);

                    if (msg == MessageBoxResult.Yes)
                    {
                        Podaci.getInstance().Etikete.Remove(etiketa);
                        SerijalizacijaEtikete.serijalizacijaEtikete();
                        TabelaEtiketa.Items.Refresh();
                    }
                }
            }
        }

        private void izmeniAkcija(object sender, RoutedEventArgs e)
        {
            if (TabelaEtiketa.SelectedItem != null)
            {
                EtiketaKlasa etiketa = (EtiketaKlasa)TabelaEtiketa.SelectedItem;

                var s = new IzmenaEtikete(etiketa);
                if (s.ShowDialog().Equals(true)) { }
                TabelaEtiketa.Items.Refresh();

                SerijalizacijaEtikete.deserijalizacijaEtikete();
                TabelaEtiketa.ItemsSource = Podaci.getInstance().Etikete;
            }

            else
            {
                MessageBox.Show("Niste selektovali etiketu");
            }
        }

        //pretraga po oznaci
        private void txtOznaka_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(TabelaEtiketa.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    EtiketaKlasa etiketa = o as EtiketaKlasa;
                    return (etiketa.Oznaka.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }

    }
}

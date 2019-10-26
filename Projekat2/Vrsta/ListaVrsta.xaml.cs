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
    /// Interaction logic for ListaVrsta.xaml
    /// </summary>
    public partial class ListaVrsta : Window
    {
        private ViewModel vm;

        private ObservableCollection<VrstaKlasa> vrste;
        private ObservableCollection<VrstaKlasa> vrsteNaCanvasu;
        private Canvas can;

        public class ViewModel
        {
            public ObservableCollection<VrstaKlasa> Vrste { get; set; }
        }

        /*public ListaVrsta()
        {
            InitializeComponent();

            SerijalizacijaTipa.deserijalizacijaTipa();          //ucitavam sve sacuvane tipove
            SerijalizacijaEtikete.deserijalizacijaEtikete();    //ucitavam sve sacuvane etikete

            vm = new ViewModel();
            vm.Vrste = new ObservableCollection<VrstaKlasa>();

            TabelaVrsta.ItemsSource = Podaci.getInstance().Vrste;
            this.DataContext = vm;
        }*/

        public ListaVrsta(ObservableCollection<VrstaKlasa> vr, ObservableCollection<VrstaKlasa> vrCanvas, Canvas canvas)
        {
            InitializeComponent();

            vrste = new ObservableCollection<VrstaKlasa>();
            vrsteNaCanvasu = new ObservableCollection<VrstaKlasa>();
            can = new Canvas();

            vrste = vr;
            vrsteNaCanvasu = vrCanvas;
            can = canvas;

            vm = new ViewModel();
            vm.Vrste = new ObservableCollection<VrstaKlasa>();

            TabelaVrsta.ItemsSource = Podaci.getInstance().Vrste;
        }

        private void OpisVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpisVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = new Vrsta.NovaVrsta(vm.Vrste);
            w.ShowDialog();
            TabelaVrsta.Items.Refresh();
        }

        private void IzmenaVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void IzmenaVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (TabelaVrsta.SelectedItem != null)
            {
                VrstaKlasa vrsta = (VrstaKlasa)TabelaVrsta.SelectedItem;

                IzmenaVrste w1 = new IzmenaVrste(vrsta);
                w1.ShowDialog();
                TabelaVrsta.Items.Refresh();

                SerijalizacijaVrste.deserijalizacijaVrste();
                TabelaVrsta.ItemsSource = Podaci.getInstance().Vrste;
            }
            else
            {
                MessageBox.Show("Niste selektovali vrstu");
            }
        }

        private void ObrisiVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ObrisiVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (VrstaKlasa vrsta in Podaci.getInstance().Vrste.ToList())
            {
                if (vrsta.Equals(TabelaVrsta.SelectedItem))
                {
                    MessageBoxResult msg = MessageBox.Show("Da li ste sigurni da želite da obrišete selektovanu vrstu?", "Potvrda brisanja vrste", MessageBoxButton.YesNo);

                    if (msg == MessageBoxResult.Yes)
                    {
                        Podaci.getInstance().Vrste.Remove(vrsta);
                        SerijalizacijaVrste.serijalizacijaVrste();
                        TabelaVrsta.Items.Refresh();

                        //ukljanja vrstu sa kanvasa ili iz panela
                        vrste.Remove(vrsta);
                        vrsteNaCanvasu.Remove(vrsta);

                        UIElement remove = null;
                        foreach (UIElement elem in can.Children)
                        {
                            if (elem.Uid == vrsta.Oznaka)
                            {
                                remove = elem;
                                break;
                            }
                        }
                        can.Children.Remove(remove);
                    }
                }
            }
        }

        private void OdustaniVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OdustaniVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        //pretraga po oznaci
        private void txtOznaka_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(TabelaVrsta.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    VrstaKlasa vrsta = o as VrstaKlasa;
                    return (vrsta.Oznaka.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }

        #region Klikovi (zakomentarisano)
        /*
        private void dodajAkcija(object sender, RoutedEventArgs e)
        {
            NovaVrsta w1 = new NovaVrsta(vm.Vrste);
            w1.ShowDialog();
            TabelaVrsta.Items.Refresh();
        }

        private void odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void izmeniAkcija(object sender, RoutedEventArgs e)
        {
            if (TabelaVrsta.SelectedItem != null)
            {
                VrstaKlasa vrsta = (VrstaKlasa)TabelaVrsta.SelectedItem;

                IzmenaVrste w1 = new IzmenaVrste(vrsta);
                w1.ShowDialog();
                TabelaVrsta.Items.Refresh();
            }

            else
            {
                MessageBox.Show("Niste selektovali vrstu");
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            foreach (VrstaKlasa vrsta in Podaci.getInstance().Vrste.ToList())
            {
                if (vrsta.Equals(TabelaVrsta.SelectedItem))
                {
                    MessageBoxResult msg = MessageBox.Show("Da li ste sigurni da želite da obrišete selektovanu vrstu?", "Potvrda brisanja vrste", MessageBoxButton.YesNo);

                    if (msg == MessageBoxResult.Yes)
                    {
                        Podaci.getInstance().Vrste.Remove(vrsta);
                        SerijalizacijaVrste.serijalizacijaVrste();
                        TabelaVrsta.Items.Refresh();
                    }
                }
            }
        }
        */
        #endregion
    }
}

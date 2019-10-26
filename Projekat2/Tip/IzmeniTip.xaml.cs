using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for IzmeniTip.xaml
    /// </summary>
    public partial class IzmeniTip : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public TipKlasa Tip { get; set; }
            public string stTip { get; set; }
        }

        public IzmeniTip()
        {
            InitializeComponent();
            OznakaBox.Focus();
        }

        public IzmeniTip(TipKlasa tip)
        {
            InitializeComponent();

            vm = new ViewModel();
            vm.Tip = tip;   //preuzimam prosledjeni tip tj selektovani

            vm.stTip = tip.Oznaka;

            this.DataContext = vm;
            OznakaBox.Focus();
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
                vm.Tip.Ikonica = op.FileName;
            }
        }

        private void btnIkonicaUkloni_Click(object sender, RoutedEventArgs e)
        {
            vm.Tip.Ikonica = "";
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            List<TipKlasa> tipovi = new List<TipKlasa>();

            foreach (TipKlasa tip in Podaci.getInstance().Tipovi)
            {
                if (tip.Oznaka == vm.stTip)
                {
                    tipovi.Add(vm.Tip);
                }
                else
                {
                    tipovi.Add(tip);
                }
            }

            Podaci.getInstance().Tipovi = tipovi;
            SerijalizacijaTipa.serijalizacijaTipa();
            this.Close();
        }
    }
}

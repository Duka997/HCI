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

namespace Projekat2.Etiketa
{
    /// <summary>
    /// Interaction logic for IzmenaEtikete.xaml
    /// </summary>
    public partial class IzmenaEtikete : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public EtiketaKlasa Etiketa { get; set; }
            public string stTip { get; set; }
        }

        /*public IzmenaEtikete()
        {
            InitializeComponent();
            OznakaBox.Focus();
        }*/

        public IzmenaEtikete(EtiketaKlasa etiketa)
        {
            InitializeComponent();

            vm = new ViewModel();
            vm.Etiketa = etiketa;   //preuzimam prosledjeni tip tj selektovani

            vm.stTip = etiketa.Oznaka;

            this.DataContext = vm;
            OznakaBox.Focus();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            List<EtiketaKlasa> etikete = new List<EtiketaKlasa>();

            foreach (EtiketaKlasa etiketa in Podaci.getInstance().Etikete)
            {
                if (etiketa.Oznaka == vm.stTip)
                {
                    etikete.Add(vm.Etiketa);
                }
                else
                {
                    etikete.Add(etiketa);
                }
            }

            Podaci.getInstance().Etikete = etikete;
            SerijalizacijaEtikete.serijalizacijaEtikete();
            this.Close();
        }
    }
}

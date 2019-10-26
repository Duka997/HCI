using Projekat2.Etiketa;
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
using System.Windows.Threading;

namespace Projekat2.Demo
{
    /// <summary>
    /// Interaction logic for NovaEtiketaDemo.xaml
    /// </summary>
    public partial class NovaEtiketaDemo : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public EtiketaKlasa Etiketa { get; set; }
        }

        DispatcherTimer t2;
        public List<String> listaOznaka = new List<string>{"o", "oz", "ozn", "ozna", "oznak", "oznaka", "oznak", "ozna", "ozn", "ozn1"};

        public List<String> listaOpis = new List<string>{ "o", "", "ov", "ovo ", "ovo je", "ovo je o", "ovo je opi", "ovo je opis.", "ovo je opis." };

        public NovaEtiketaDemo()
        {
            InitializeComponent();

            vm = new ViewModel();
            vm.Etiketa = new EtiketaKlasa();
            this.DataContext = vm;

            t2 = new DispatcherTimer();
            t2.Tick += t_Tick1;
            t2.Interval = new TimeSpan(0, 0, 0, 0, 500);
            t2.Start();
        }

        private void Button_Potvrdi(object sender, RoutedEventArgs e)
        {
            t2.Stop();
            this.Close();
        }

        private void Button_Odustani(object sender, RoutedEventArgs e)
        {
            t2.Stop();
            this.Close();
        }

        public int i = 0;
        void t_Tick1(object sender, EventArgs e)    //za unos oznake
        {
            OznakaBox.Text = listaOznaka[i];
            i++;
            if (i == listaOznaka.Count())
            {
                t2.Stop();

                t2 = new DispatcherTimer();
                t2.Tick += t_Tick2;
                t2.Interval = new TimeSpan(0, 0, 0, 0, 500);
                t2.Start();
            }
        }

        public int j = 0;
        void t_Tick2(object sender, EventArgs e)    //unos opisa
        {
            OpisBox.Text = listaOpis[j];
            j++;
            if (j == listaOpis.Count())
            {
                t2.Stop();
                reset();
            }
        }

        public void reset()
        {
            i = 0;
            j = 0;
            OznakaBox.Text = "";
            OpisBox.Text = "";
            t2 = new DispatcherTimer();
            t2.Tick += t_Tick1;
            t2.Interval = new TimeSpan(0, 0, 0, 0, 500);
            t2.Start();
        }
    }
}

using Projekat2.Tip;
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
    /// Interaction logic for NoviTipDemo.xaml
    /// </summary>
    public partial class NoviTipDemo : Window
    {
        private ViewModel vm;

        public class ViewModel
        {
            public TipKlasa Tip { get; set; }
        }

        DispatcherTimer t2;
        public List<String> listaOznaka = new List<string>{ "o", "oz", "ozn", "ozna", "oznak", "oznaka", "oznak", "ozna", "ozn", "ozn1" };

        public List<String> listaIme = new List<string> { "i", "", "i", "im", "ime" };

        public List<String> listaOpis = new List<string>{ "o", "", "ov", "ovo ", "ovo je", "ovo je o", "ovo je opi", "ovo je opis.", "ovo je opis." };

        public NoviTipDemo()
        {
            InitializeComponent();

            vm = new ViewModel();
            vm.Tip = new TipKlasa();
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
        void t_Tick2(object sender, EventArgs e)    //za unos imena
        {
            boxIme.Text = listaIme[j];
            j++;
            if (j == listaIme.Count())
            {
                t2.Stop();

                t2 = new DispatcherTimer();
                t2.Tick += t_Tick3;
                t2.Interval = new TimeSpan(0, 0, 0, 0, 500);
                t2.Start();
            }

        }


        void t_Tick3(object sender, EventArgs e)    //za unos ikonice
        {
            Ikonica.Source = new BitmapImage(new Uri("C:/Users/Duka/Desktop/HCI/Projekat/slike vrsta/nosorog.png"));
            t2.Stop();

            t2 = new DispatcherTimer();
            t2.Tick += t_Tick4;
            t2.Interval = new TimeSpan(0, 0, 0, 0, 500);
            t2.Start();
        }

        public int k = 0;
        void t_Tick4(object sender, EventArgs e)    //za unos opisa
        {
            boxOpis.Text = listaOpis[k];
            k++;
            if (k == listaOpis.Count())
            {
                t2.Stop();

                reset();
            }
        }

        public void reset()
        {
            i = 0;
            j = 0;
            k = 0;
            OznakaBox.Text = "";
            boxIme.Text = "";
            Ikonica.Source = null;
            boxOpis.Text = "";
            t2 = new DispatcherTimer();
            t2.Tick += t_Tick1;
            t2.Interval = new TimeSpan(0, 0, 0, 0, 500);
            t2.Start();
        }
    }
}

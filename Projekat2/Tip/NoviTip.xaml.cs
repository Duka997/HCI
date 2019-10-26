using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

using Projekat2.Tip;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Projekat2.Tip
{
    /// <summary>
    /// Interaction logic for NoviTip.xaml
    /// </summary>
    public partial class NoviTip : Window
    {
        //https://intellitect.com/getting-started-model-view-viewmodel-mvvm-pattern-using-windows-presentation-framework-wpf/
        private ViewModel vm;
        ObservableCollection<TipKlasa> tipovi;

        public class ViewModel
        {
            public TipKlasa Tip { get; set; }
        }

        public NoviTip()
        {
            InitializeComponent();
            vm = new ViewModel();
            vm.Tip = new TipKlasa();
            this.DataContext = vm;

            OznakaBox.Focus();
        }

        public NoviTip(ObservableCollection<TipKlasa> tip)
        {
            InitializeComponent();

            tipovi = new ObservableCollection<TipKlasa>();
            tipovi = tip;

            vm = new ViewModel();
            vm.Tip = new TipKlasa();    //novi tip koji cu da dodajem

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
            //TODO: dodati jos da azurira trenutni dijalog
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (OznakaBox.Text != "" && boxIme.Text != "" && boxOpis.Text != "" && Ikonica.Source != null)
            {
                Podaci.getInstance().Tipovi.Add(vm.Tip);
                SerijalizacijaTipa.serijalizacijaTipa();

                if(tipovi != null)  //pravljenje novog tipa iz nove vrste sa cekboxom ne poziva konstruktor sa parametrom pa dodavanje vm-a nije moguce
                    tipovi.Add(vm.Tip);

                MessageBox.Show("Podaci o tipu su uspešno sačuvani.");

                this.Close();
            }

            else
                MessageBox.Show("Niste popunili sva polja!");
        }

        //nova validacija preko klase tipa
        #region validacija tipa (zakomentarisano da ne bi doslo do overridovannja)
        /*private string _OznakaVrste;
        private string _ImeVrste;
        private string _OpisVrste;

        public string OznakaVrste
        {
            get
            {
                return _OznakaVrste;
            }
            set
            {
                if (value != _OznakaVrste)
                {
                    _OznakaVrste = value;
                    OnPropertyChanged("OznakaVrste");
                }
            }
        }

        public string ImeVrste
        {
            get
            {
                return _ImeVrste;
            }
            set
            {
                if (value != _ImeVrste)
                {
                    _ImeVrste = value;
                    OnPropertyChanged("ImeVrste");
                }
            }
        }

        public string OpisVrste
        {
            get
            {
                return _OpisVrste;
            }
            set
            {
                if (value != _OpisVrste)
                {
                    _OpisVrste = value;
                    OnPropertyChanged("OpisVrste");
                }
            }
        }*/
        #endregion

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

        #region Demo
        private Boolean demoMode = false;
        
        public void beginDemo()
        {
            demoMode = true;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                Point p3 = getOznakaPoint();
                p3.Y += 5;
                p3.X += 50;
                LinearSmoothMove(p3, 100);
                System.Threading.Thread.Sleep(500);

            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                OznakaBox.Text = "O";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                OznakaBox.Text = "Oz";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                OznakaBox.Text = "Ozn";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                OznakaBox.Text = "Ozn1";

            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                OznakaBox.Text = "Ozn1";

            });
            if (!demoMode) return;

            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(500);
                Point p2 = getImePoint();
                p2.Y += 5;
                p2.X += 50;
                LinearSmoothMove(p2, 100);
                System.Threading.Thread.Sleep(500);
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxIme.Text = "I";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxIme.Text = "Im";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxIme.Text = "Ime";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxIme.Text = "Ime";
            });
            if (!demoMode) return;

            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                Point p3 = getOpisPoint();
                p3.Y = 0 - p3.Y; // Ne znam zasto vraca negativno xD
                p3.X = 0 - p3.X;

                p3.X += 50;
                p3.Y += 10;
                Console.WriteLine("Debug: " + p3.X + " " + p3.Y);
                LinearSmoothMove(p3, 100);
                System.Threading.Thread.Sleep(500);
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxOpis.Text = "O";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxOpis.Text = "Op";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxOpis.Text = "Opi";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxOpis.Text = "Opis";
            });
            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(100);
                boxOpis.Text = "Opis";
            });
            if (!demoMode) return;
            // na odustani
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(500);
                Point p2 = getOdustaniPoint();
                p2.Y += 5;
                p2.X += 20;
                LinearSmoothMove(p2, 100);
                System.Threading.Thread.Sleep(500);
            });

            if (!demoMode) return;
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                System.Threading.Thread.Sleep(2000);
                this.Close();
                Application.Current.MainWindow.Show();
            });

        }
        
        public void LinearSmoothMove(Point newPosition, int steps)
        {
            if (demoMode)
            {
                Point start = Win32.GetMousePosition();
                Point iterPoint = start;

                // Find the slope of the line segment defined by start and newPosition
                Point slope = new Point(newPosition.X - start.X, newPosition.Y - start.Y);

                // Divide by the number of steps
                slope.X = slope.X / steps;
                slope.Y = slope.Y / steps;

                // Move the mouse to each iterative point.
                for (int i = 0; i < steps; i++)
                {
                    if (!demoMode)
                    {
                        return;
                    }
                    iterPoint = new Point(iterPoint.X + slope.X, iterPoint.Y + slope.Y);
                    Win32.SetCursorPos((int)iterPoint.X, (int)iterPoint.Y);
                    System.Threading.Thread.Sleep(20);
                }

                // Move the mouse to the final destination.
                Win32.SetCursorPos((int)newPosition.X, (int)newPosition.Y);
            }

        }

        public Point getOznakaPoint()
        {
            if (!demoMode) return new Point(0, 0);
            return OznakaBox.PointToScreen(new Point(0d, 0d));
        }

        public Point getImePoint()
        {
            if (!demoMode) return new Point(0, 0);
            Point p2 = boxIme.PointToScreen(new Point(0d, 0d));
            return p2;
        }

        public Point getOpisPoint()
        {
            if (!demoMode) return new Point(0, 0);
            return boxOpis.PointFromScreen(new Point(0d, 0d));
        }

        public Point getOdustaniPoint()
        {
            if (!demoMode) return new Point(0, 0);
            return OdustaniDugme.PointToScreen(new Point(0d, 0d));
        }

        #endregion
    }
}

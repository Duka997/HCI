using Projekat2.Demo;
using Projekat2.Etiketa;
using Projekat2.Help;
using Projekat2.Tip;
using Projekat2.Vrsta;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel vm;
        private static MainWindow instance = null;

        //drag and drop
        Point start = new Point();
        private const int ICON_SIZE = 70;
        private const int OFFSET = ICON_SIZE / 2;
        private const string FROM_SIDEBAR = "VrstaDraggedFromSidebar";
        private const string FROM_CANVAS = "VrstaDraggedFromCanvas";

        public class ViewModel
        {
            public ObservableCollection<VrstaKlasa> Vrste { get; set; }     //vrste koje nisu jos ubacene na kanvas    
            public VrstaKlasa ClickedVrsta { get; set; }    //kliknuta vrsta
            public ObservableCollection<VrstaKlasa> droppedVrste { get; set; }  //vrste spustene na kanvas
        }

        public MainWindow()
        {
            InitializeComponent();

            SerijalizacijaVrste.deserijalizacijaVrste();        //ucitavam sve sacuvane vrste
            SerijalizacijaTipa.deserijalizacijaTipa();          //ucitavam sve sacuvane tipove
            SerijalizacijaEtikete.deserijalizacijaEtikete();    //ucitavam sve sacuvane etikete

            vm = new ViewModel();
            vm.Vrste = new ObservableCollection<VrstaKlasa>();
            vm.droppedVrste = new ObservableCollection<VrstaKlasa>();

            foreach (VrstaKlasa vrsta in Podaci.getInstance().Vrste)    //prolazim kroz sve vrste
            {
                if (vrsta.X == 0 && vrsta.Y == 0)   //ako koordinate vrste 0 znaci da se ne nalazi na kanvasu 
                {
                    vm.Vrste.Add(vrsta);    //dodajem vrstu u vrste koje nisu na kanvasu tj u prikaz sa leve strane
                }

                else    //inace su vrste na kanvasu i spustam ih na kanvas
                {
                    Canvas canvas = mapaVrsta;

                    try     //try catch u slucaju da se ikonica obrise iz foldera
                    {
                        Image Ikonica = new Image
                        {
                            Width = ICON_SIZE,
                            Height = ICON_SIZE,
                            Uid = vrsta.Oznaka,
                            Source = new BitmapImage(new Uri(vrsta.Ikonica, UriKind.Absolute)),
                        };

                        Ikonica.ToolTip = vrsta.Oznaka; //ucitavam tooltipove na ikonicama na kanvasu

                        canvas.Children.Add(Ikonica);

                        Canvas.SetLeft(Ikonica, vrsta.X);
                        Canvas.SetTop(Ikonica, vrsta.Y);

                        vm.droppedVrste.Add(vrsta);
                    }

                    catch
                    {
                        MessageBox.Show("Neke ikonice nece biti prikazane jer su obrisane!");
                    }
                }
            }

            //this.DataContext = vm;
            ugroVrste.ItemsSource = vm.Vrste;
        }

        public static MainWindow getInstance()
        {
            if (instance == null)
                instance = new MainWindow();
            return instance;
        }

        private void OpisVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpisVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = new Vrsta.NovaVrsta(vm.Vrste);
            w.ShowDialog();
        }

        private void TipVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void TipVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = new Tip.NoviTip();
            w.ShowDialog();
        }

        private void EtiketaVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EtiketaVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = new Etiketa.NovaEtiketa();
            w.ShowDialog();
        }

        //liste
        private void ListaVrste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ListaVrste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = new Vrsta.ListaVrsta(vm.Vrste, vm.droppedVrste, mapaVrsta);
            w.ShowDialog();
        }

        private void ListaTipova_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ListaTipova_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = new Tip.ListaTipova();
            w.ShowDialog();
        }

        private void ListaEtiketa_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ListaEtiketa_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = new Etiketa.ListaEtiketa();
            w.ShowDialog();
        }

        //Pomocni dijalog
        private void Pomoc_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Pomoc_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var w = new Help.Pomoc();
            w.ShowDialog();
        }

        //Demo za etiketu
        private void DemoEtiketa_Click(object sender, RoutedEventArgs e)
        {
            var w = new NovaEtiketaDemo();
            w.ShowDialog();
        }

        //Demo za tip
        private void DemoTip_Click(object sender, RoutedEventArgs e)
        {
            var w = new NoviTipDemo();
            w.ShowDialog();
        }

        //Drag and drop
        //leva strana
        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)   //sa panela na kanvas
        {
            start = e.GetPosition(null);

            StackPanel panel = sender as StackPanel;
            VrstaKlasa dataObject = null;

            foreach (VrstaKlasa vrsta in vm.Vrste)
            {
                if ((string)panel.Tag == vrsta.Oznaka)
                {
                    dataObject = vrsta;
                    break;
                }
            }

            DataObject data = new DataObject(FROM_SIDEBAR, dataObject);
            DragDrop.DoDragDrop(panel, data, DragDropEffects.Move);
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {

        }

        //kanvas (desna strana)
        private void mapaVrsta_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void mapaVrsta_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void mapaVrsta_DragEnter(object sender, DragEventArgs e)
        {

        }

        private VrstaKlasa ClickedVrsta(int x, int y)   //vraca kliknutu vrstu na kanvasu
        {
            foreach (VrstaKlasa vrsta in vm.droppedVrste)   //prolazim kroz vrste spustene na kanvas
            {
                if (Math.Sqrt(Math.Pow((x - vrsta.X - OFFSET), 2) + Math.Pow((y - vrsta.Y - OFFSET), 2)) < 1 * OFFSET)
                {
                    return vrsta;
                }
            }
            return null;
        }

        private void mapaVrsta_Drop(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(mapaVrsta);

            if (ClickedVrsta((int)dropPosition.X, (int)dropPosition.Y) != null)
            {
                return;
            }

            if (e.Data.GetDataPresent(FROM_SIDEBAR))    //sa panela na kanvas
            {
                VrstaKlasa vrsta = e.Data.GetData(FROM_SIDEBAR) as VrstaKlasa;

                vm.Vrste.Remove(vrsta); //uklanjam iz vrsta koje nisu na kanvasu vrstu
                vrsta.X = (int)dropPosition.X - OFFSET;
                vrsta.Y = (int)dropPosition.Y - OFFSET;

                Podaci.ChangeDroppedVrsta(vrsta);   //pozivam serijalizaciju zbog koordinata

                vm.droppedVrste.Add(vrsta); //dodajem u vrste na kanvasu vrstu

                Canvas canvas = this.mapaVrsta;

                Image VrstaIkonica = new Image
                {
                    Width = ICON_SIZE,
                    Height = ICON_SIZE,
                    Uid = vrsta.Oznaka,
                    Source = new BitmapImage(new Uri(vrsta.Ikonica, UriKind.Absolute))
                };

                VrstaIkonica.ToolTip = vrsta.Oznaka;

                canvas.Children.Add(VrstaIkonica);

                Canvas.SetLeft(VrstaIkonica, vrsta.X);
                Canvas.SetTop(VrstaIkonica, vrsta.Y);

                return;
            }

            if (e.Data.GetDataPresent(FROM_CANVAS)) //sa kanvasa na kanvas
            {
                VrstaKlasa vrsta = e.Data.GetData(FROM_CANVAS) as VrstaKlasa;

                vm.droppedVrste.Remove(vrsta);
                vrsta.X = (int)dropPosition.X - OFFSET;
                vrsta.Y = (int)dropPosition.Y - OFFSET;

                Podaci.ChangeDroppedVrsta(vrsta);
                vm.droppedVrste.Add(vrsta);

                Canvas canvas = this.mapaVrsta;

                UIElement remove = null;
                foreach (UIElement elem in canvas.Children)
                {
                    if (elem.Uid == vrsta.Oznaka)
                    {
                        remove = elem;
                        break;
                    }
                }
                canvas.Children.Remove(remove);

                Image VrstaIkonica = new Image
                {
                    Width = ICON_SIZE,
                    Height = ICON_SIZE,
                    Uid = vrsta.Oznaka,
                    Source = new BitmapImage(new Uri(vrsta.Ikonica, UriKind.Absolute)),
                };

                VrstaIkonica.ToolTip = vrsta.Oznaka;

                canvas.Children.Add(VrstaIkonica);

                Canvas.SetLeft(VrstaIkonica, vrsta.X);
                Canvas.SetTop(VrstaIkonica, vrsta.Y);

                return;
            }
        }

        private void mapaVrsta_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(null);

            Canvas map = sender as Canvas;

            VrstaKlasa dataObject = null;
            Point mousePosition = e.GetPosition(mapaVrsta);

            dataObject = ClickedVrsta((int)mousePosition.X, (int)mousePosition.Y);

            if (dataObject != null)
            {
                DataObject data = new DataObject(FROM_CANVAS, dataObject);
                DragDrop.DoDragDrop(map, data, DragDropEffects.Move);
            }
        }

        private void mapaLokala_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point dropPosition = e.GetPosition(mapaVrsta);
            VrstaKlasa vrsta = ClickedVrsta((int)dropPosition.X, (int)dropPosition.Y);

            if (vrsta != null)
            {
                for (int i = 0; i < vm.droppedVrste.Count; i++)
                {
                    if (vm.droppedVrste[i].Oznaka.Equals(vrsta.Oznaka))
                    {
                        vm.Vrste.Add(vm.droppedVrste[i]);
                        mapaVrsta.Children.RemoveAt(i);
                        ugroVrste.ItemsSource = vm.Vrste;
                        vm.droppedVrste.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        #region demo
        private void demo_Begin(object sender, RoutedEventArgs e)
        {
            demoMode = true;
            Point p = dodajTipDugme.PointToScreen(new Point(0d, 0d));
            p.X += 20;
            p.Y += 10;

            LinearSmoothMove(p, 100);

            NoviTip w2 = new NoviTip();
            w2.Show();
            Thread.Sleep(500);
            Application.Current.Dispatcher?.BeginInvoke(new Action(() => {

                w2.beginDemo();
            }));


            //Win32.ClientToScreen(this.Handle, ref p);

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
                    Thread.Sleep(20);
                }

                // Move the mouse to the final destination.
                Win32.SetCursorPos((int)newPosition.X, (int)newPosition.Y);
            }
        }

        private Boolean demoMode = false;

        private void ExitDemo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (demoMode == true)
            {
                e.CanExecute = true;
            }
        }

        private void ExitDemo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            demoMode = false;
        }

        #endregion  

        //Pomoc
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            var s = new Pomoc();
            if (s.ShowDialog().Equals(true)) { }
        }
    }
}

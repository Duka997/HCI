using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekat2.Dijalog
{
    public static class VrsteCmd
    {
        public static readonly RoutedUICommand Opis_vrste = new RoutedUICommand(
            "Nova vrsta",
            "NovaVrsta",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.A, ModifierKeys.Control),
                new KeyGesture(Key.A, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Tip_vrste = new RoutedUICommand(
            "Tip vrste",
            "TipVrste",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.T, ModifierKeys.Control),
                new KeyGesture(Key.T, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Etiketa_vrste = new RoutedUICommand(
            "Etiketa vrste",
            "EtiketaVrste",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.B, ModifierKeys.Control),
                new KeyGesture(Key.B, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Lista_vrsta = new RoutedUICommand(
            "Lista vrsta",
            "ListaVrsta",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.L, ModifierKeys.Control),
                new KeyGesture(Key.L, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Lista_tipova = new RoutedUICommand(
            "Lista tipova",
            "ListaTipova",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.K, ModifierKeys.Control),
                new KeyGesture(Key.K, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Lista_etiketa = new RoutedUICommand(
            "Lista etiketa",
            "ListaEtiketa",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.J, ModifierKeys.Control),
                new KeyGesture(Key.J, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Pomoc = new RoutedUICommand(
            "Pomoć",
            "Pomoc",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.H, ModifierKeys.Control),
                new KeyGesture(Key.H, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Accept = new RoutedUICommand(
             "Potvrdi",
              "Potvrdi",
             typeof(VrsteCmd),
             new InputGestureCollection()
            {
               new KeyGesture(Key.Enter)
            }
        );

        public static readonly RoutedUICommand Izmena_vrste = new RoutedUICommand(
            "Izmeni",
            "Izmeni",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Control),
                new KeyGesture(Key.E, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Obrisi_vrste = new RoutedUICommand(
            "Obriši",
            "Obrisi",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D, ModifierKeys.Control),
                new KeyGesture(Key.D, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Odustani_vrste = new RoutedUICommand(
            "Odustani",
            "Odustani",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.W, ModifierKeys.Control),
                new KeyGesture(Key.W, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );

        public static readonly RoutedUICommand Sacuvaj_vrste = new RoutedUICommand(
            "Sačuvaj",
            "Sacuvaj",
            typeof(VrsteCmd),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control),
                new KeyGesture(Key.S, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekat2.Vrsta
{
    class ValidacijaVrste : ValidationRule  //validacija da proveri da li postoji vec vrsta sa tom oznakom
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                foreach (VrstaKlasa vrsta in Podaci.getInstance().Vrste)
                {
                    if (vrsta.Oznaka == text)
                    {
                        return new ValidationResult(false, "Unesena oznaka već postoji!");
                    }
                }

                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Greška.");
            }
        }
    }

    public class ValidacijaVrsteOznaka : ValidationRule   //validacija oznake
    {
        private static int MIN_LENGTH = 3;
        private static int MAX_LENGTH = 5;

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Oznaka vrste je obavezna.");
                }
                else if (text.Length < MIN_LENGTH)
                {
                    return new ValidationResult(false, "Minimalna dužina polja je " + MIN_LENGTH + " karaktera.");
                }
                else if (text.Length > MAX_LENGTH)
                {
                    return new ValidationResult(false, "Maksimalna dužina polja je " + MAX_LENGTH + " karaktera.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Greška.");
            }
        }
    }

    public class ValidacijaVrsteIme : ValidationRule   //validacija imena
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Ime vrste je obavezno.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Greška.");
            }
        }
    }

    public class ValidacijaVrsteOpis : ValidationRule   //validacija opisa
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Opis vrste je obavezan.");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            catch
            {
                return new ValidationResult(false, "Greška.");
            }
        }
    }

    public class ValidacijaVrstePrihod : ValidationRule  //validacija godisnjeg prihoda
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                try
                {
                    //int prihod = Int32.Parse(text);
                    float prihod = float.Parse(text);

                    if (prihod < 0)
                    {
                        return new ValidationResult(false, "Godišnji prihod ne može biti negativan broj!");
                    }
                    else
                    {
                        return new ValidationResult(true, null);
                    }
                }
                catch
                {
                    return new ValidationResult(false, "Godišnji prihod mora biti broj.");
                }
            }
            catch
            {
                return new ValidationResult(false, "Greška.");
            }
        }
    }
}

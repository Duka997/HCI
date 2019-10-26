using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekat2.Etiketa
{
    class ValidacijaEtikete : ValidationRule   //validacija da proveri da li postoji vec etiketa sa tom oznakom
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;

                foreach (EtiketaKlasa et in Podaci.getInstance().Etikete)
                {
                    if (et.Oznaka == text)
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

    public class ValidacijaEtiketeOznaka : ValidationRule   //validacija oznake
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
                    return new ValidationResult(false, "Oznaka etikete je obavezna.");
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

    public class ValidacijaEtiketeOpis : ValidationRule   //validacija opisa
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var text = value as string;
                if (0 == text.Length)
                {
                    return new ValidationResult(false, "Opis etikete je obavezan.");
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
}

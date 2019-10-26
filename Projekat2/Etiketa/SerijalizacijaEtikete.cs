using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projekat2.Etiketa
{
    public class SerijalizacijaEtikete
    {
        private static string file = "etikete.xml";

        public SerijalizacijaEtikete() { }

        //http://www.newthinktank.com/2017/03/c-tutorial-18/
        public static void serijalizacijaEtikete()
        {
            using (Stream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<EtiketaKlasa>));
                serializer.Serialize(fs, Podaci.getInstance().Etikete);
            }

        }

        public static void deserijalizacijaEtikete()
        {
            if (File.Exists(file) == false) //ako ne postoji fajl 
                serijalizacijaEtikete();

            XmlSerializer serializer = new XmlSerializer(typeof(List<EtiketaKlasa>));

            using (FileStream fs = File.OpenRead(file))
            {
                Podaci.getInstance().Etikete = (List<EtiketaKlasa>)serializer.Deserialize(fs);
            }
        }
    }
}

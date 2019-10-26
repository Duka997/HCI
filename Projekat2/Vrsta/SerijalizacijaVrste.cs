using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projekat2.Vrsta
{
    public class SerijalizacijaVrste
    {
        private static string file = "vrste.xml";

        public SerijalizacijaVrste() { }

        //http://www.newthinktank.com/2017/03/c-tutorial-18/
        public static void serijalizacijaVrste()
        {
            using (Stream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<VrstaKlasa>));
                serializer.Serialize(fs, Podaci.getInstance().Vrste);
            }

        }

        public static void deserijalizacijaVrste()
        {
            if (File.Exists(file) == false) //ako ne postoji fajl 
                serijalizacijaVrste();

            XmlSerializer serializer = new XmlSerializer(typeof(List<VrstaKlasa>));

            using (FileStream fs = File.OpenRead(file))
            {
                Podaci.getInstance().Vrste = (List<VrstaKlasa>)serializer.Deserialize(fs);
            }
        }
    }
}

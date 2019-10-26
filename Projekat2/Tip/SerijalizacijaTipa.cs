using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Projekat2.Tip
{
    public class SerijalizacijaTipa
    {
        private static string file = "tipovi.xml";

        public SerijalizacijaTipa() {

        }

        //http://www.newthinktank.com/2017/03/c-tutorial-18/
        public static void serijalizacijaTipa()
        {
            using (Stream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<TipKlasa>));
                serializer.Serialize(fs, Podaci.getInstance().Tipovi);
            }

        }

        public static void deserijalizacijaTipa()
        {
            if (File.Exists(file) == false) //ako ne postoji fajl 
                serijalizacijaTipa();

            XmlSerializer serializer = new XmlSerializer(typeof(List<TipKlasa>));

            using (FileStream fs = File.OpenRead(file))
            {
                Podaci.getInstance().Tipovi = (List<TipKlasa>)serializer.Deserialize(fs);
            }
        }
    }
}

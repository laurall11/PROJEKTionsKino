using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Lebensmittel
    {
        //LebensmittelID INT, Name string, Preis double, kategorie string
        public int LebensmittelID { get; set; }
        public string Name { get; set; }
        public double Preis { get; set; }
        public string Kategorie { get; set; }
    }
}

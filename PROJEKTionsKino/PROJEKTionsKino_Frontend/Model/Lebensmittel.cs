using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Lebensmittel
    {
        public Lebensmittel(int lebensmittelID, string name, string kategorie, decimal preis)
        {
            LebensmittelID = lebensmittelID;
            Name = name;
            Preis = preis;
            Kategorie = kategorie;
        }

        //LebensmittelID INT, Name string, Preis double, kategorie string
        public int LebensmittelID { get; set; }
        public string Name { get; set; }
        public decimal Preis { get; set; }
        public string Kategorie { get; set; }
    }
}

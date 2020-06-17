using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Sitzplatz
    {
        public int SitzplatzID { get; set; }
        public int SitzpatzkategorieID { get; set; }
        public int SaalID { get; set; }
        public int Sitzplatznummer { get; set; }
        public int Reihe { get; set; }

        public Sitzplatz(int sitzplatzID, int sitzpatzkategorieId, int saalId, int sitzplatznummer, int reihe)
        {
            SitzpatzkategorieID = sitzpatzkategorieId;
            SitzplatzID = sitzplatzID;
            SaalID = saalId;
            Sitzplatznummer = sitzplatznummer;
            Reihe = reihe;
        }
    }
}
//sitzplatzid INT, sitzplatzkategorieid INT, saalid INT, sitzplatznr INT, reihe int
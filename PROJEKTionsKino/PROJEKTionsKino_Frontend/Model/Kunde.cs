using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Kunde
    {
        public Kunde(int id, string vorname, string nachname, string straße, int hausNr, int pLZ, string ort, DateTime geburtsdatum, DateTime erstelldatum)
        {
            ID = id;
            Vorname = vorname;
            Nachname = nachname;
            Straße = straße;
            HausNr = hausNr;
            PLZ = pLZ;
            Ort = ort;
            Geburtsdatum = geburtsdatum;
            Erstelldatum = erstelldatum;
        }

        public Kunde(string vorname, string nachname, string straße, int hausNr, int pLZ, DateTime geburtsdatum)
        {
            Vorname = vorname;
            Nachname = nachname;
            Straße = straße;
            HausNr = hausNr;
            PLZ = pLZ;
            Geburtsdatum = geburtsdatum;
            Erstelldatum = DateTime.Now;
        }

        public int ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Straße { get; set; }
        public int HausNr { get; set; }
        public int PLZ { get; set; }
        public string Ort { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public DateTime Erstelldatum { get; set; }
        public bool IsVorteilskunde { get; set; }
    }
}

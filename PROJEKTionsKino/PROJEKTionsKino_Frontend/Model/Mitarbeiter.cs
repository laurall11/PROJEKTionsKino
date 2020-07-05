using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Mitarbeiter
    {
        public Mitarbeiter(int id, string vorname, string nachname, string straße, int hausnummer, int plz, string stadt, DateTime geburtstag, DateTime ersterArbeitstag, int urlaubstage, string bezeichnung, string arbeitsbeginn, string arbeitsende, double wochenstunden, double gehalt)
        {
            ID = id;
            Vorname = vorname;
            Nachname = nachname;
            Straße = straße;
            Hausnummer = hausnummer;
            PLZ = plz;
            Stadt = stadt;
            Geburtstag = geburtstag;
            ErsterArbeitstag = ersterArbeitstag;
            Urlaubstage = urlaubstage;
            Bezeichnung = bezeichnung;
            Arbeitsbeginn = arbeitsbeginn;
            Arbeitsende = arbeitsende;
            Wochenstunden = wochenstunden;
            Gehalt = gehalt;
        }


        //Mitarbeiterid, vorname, nachname, strasse, hausnummer, postleitzahl, stadt, geburtstag, erster_arbeitstag, offene_urlaubstage, bezeichnung, arbeitsbeginn, arbeitsende, wochenstunden, gehalt
        public int ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Straße { get; set; }
        public int Hausnummer { get; set; }
        public int Plz { get; }
        public int PLZ { get; set; }
        public string Stadt { get; set; }
        public DateTime Geburtstag { get; set; }
        public DateTime ErsterArbeitstag { get; set; }
        public int Urlaubstage { get; set; }
        public string Bezeichnung { get; set; }
        public string Arbeitsbeginn { get; set; }
        public string Arbeitsende { get; set; }
        public double Wochenstunden { get; set; }
        public double Gehalt { get; set; }
    }
}

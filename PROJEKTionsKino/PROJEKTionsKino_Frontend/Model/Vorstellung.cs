using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Vorstellung
    {
        //progammbeginn, programmende, filmname, dauer, altersfreigabe, sitzplatzanzahl, beschreibung, genre
        //regie, filmid, erscheinungsjahr, ratinganzahl, ratingsterne, saalid, programmid
        public DateTime Programmbeginn { get; set; }
        public DateTime Programmende { get; set; }
        public string Filmname { get; set; }
        public string Beschreibung { get; set; }
        public int SaalId { get; }
        public int SaalID { get; set; }
        public int Sitzplatzanzahl { get; set; }

        public Vorstellung(DateTime programmbeginn, DateTime programmende, string filmname, string beschreibung, int saalId, int sitzplatzanzahl)
        {
            Programmbeginn = programmbeginn;
            Programmende = programmende;
            Filmname = filmname;
            Beschreibung = beschreibung;
            SaalId = saalId;
            Sitzplatzanzahl = sitzplatzanzahl;
        }


    }
}

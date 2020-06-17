using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Film
    {
        public Film(int filmID, int dauer, int altersfreigabe, int erscheinungsjahr, int ratinganzahl, 
            int ratingsterne, string filmname, string beschreibung, string genre, string regie)
        {
            FilmID = filmID;
            Dauer = dauer;
            Altersfreigabe = altersfreigabe;
            Erscheinungsjahr = erscheinungsjahr;
            Ratinganzahl = ratinganzahl;
            Ratingsterne = ratingsterne;
            Filmname = filmname;
            Beschreibung = beschreibung;
            Genre = genre;
            Regie = regie;
        }

        public int FilmID { get; set; }
        public int Dauer { get; set; }
        public int Altersfreigabe { get; set; }
        public int Erscheinungsjahr { get; set; }
        public int Ratinganzahl { get; set; }
        public int Ratingsterne { get; set; }
        public string Filmname { get; set; }
        public string Beschreibung { get; set; }
        public string Genre { get; set; }
        public string Regie { get; set; }
    }
}

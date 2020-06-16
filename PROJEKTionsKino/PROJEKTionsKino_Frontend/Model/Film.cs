using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Film
    {
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

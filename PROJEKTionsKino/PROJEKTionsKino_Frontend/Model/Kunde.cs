using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Kunde
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Straße { get; set; }
        public int HausNr { get; set; }
        public int PLZ { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public bool IsVorteilskunde { get; set; }
    }
}

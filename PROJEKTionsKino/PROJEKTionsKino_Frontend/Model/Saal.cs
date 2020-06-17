using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKTionsKino_Frontend.Model
{
    public class Saal
    {
        public int SaalID { get; set; }
        public int Sitzplatzanzahl { get; set; }
        public ObservableCollection<int> SitzplatzIDs { get; set; }

        public Saal(int saalid, int sitzplatzanzahl)
        {
            SaalID = saalid;
            Sitzplatzanzahl = sitzplatzanzahl;
            SitzplatzIDs = new ObservableCollection<int>();
        }
    }
}

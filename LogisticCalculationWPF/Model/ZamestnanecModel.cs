using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCalculationWPF.Model
{
    public class ZamestnanecModel
    {
        public int Id { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public DateTime Narozeni { get; set; }
        public string PracovniPomer { get; set; }
        public DateTime ZamestnanOd { get; set; }
        public DateTime? ZamestnanDo { get; set; }
    }
}

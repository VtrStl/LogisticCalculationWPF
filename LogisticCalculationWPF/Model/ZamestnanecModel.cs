using System;

namespace LogisticCalculationWPF.Model
{
    public class ZamestnanecModel
    {
        public int Id { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public DateOnly Narozeni { get; set; }
        public string PracovniPomer { get; set; }
        public DateOnly ZamestnanOd { get; set; }
        public DateOnly? ZamestnanDo { get; set; }
    }
}

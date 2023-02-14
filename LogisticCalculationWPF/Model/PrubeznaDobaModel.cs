using LogisticCalculationWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LogisticCalculationWPF.Model
{
    public class PrubeznaDobaModel
    {
        private int Tpz1 { get; set; }
        private int Tksum { get; set; }
        private int Tkmax { get; set; }
        private int Tmsum { get; set;}
        private int TmWithValue { get; set; }
        private int DavkaQ { get; set; }
        private int DavkaQD { get; set; }
        private int SystemZpracovani { get; set; }

        public PrubeznaDobaModel(ObservableCollection<Pracoviste> PrubeznaDoba, int davkaQ, int davkaQD, int systemZpracovani)
        {
            Tpz1 = Convert.ToInt32(PrubeznaDoba[0].Tpz);
            Tksum = PrubeznaDoba.Sum(row => Convert.ToInt32(row.Tk));
            Tkmax = PrubeznaDoba.Max(row => Convert.ToInt32(row.Tk));
            Tmsum = PrubeznaDoba.Sum(row => Convert.ToInt32(row.Tm));
            TmWithValue = PrubeznaDoba.Count(row => Convert.ToInt32(row.Tm) !=0);
            DavkaQ = davkaQ;
            DavkaQD = davkaQD;
            SystemZpracovani = systemZpracovani;

        }
        
        private int SoubezneJednotlive()
        {
            return Tpz1 + Tksum + (DavkaQ - 1) * Tkmax + Tmsum;
        }
        
        private int SoubeznePoDavkach()
        {
            return Tpz1 + DavkaQD * Tksum + (DavkaQ - DavkaQD) * Tkmax + Tmsum;
        }

        public int PrubeznaDobaVysledek()
        {
            return SystemZpracovani switch
            {
                0 => SoubezneJednotlive(),
                1 => SoubeznePoDavkach(),
                _ => 0
            };
        }
    }
    
    public class Pracoviste
    {
        public int? PracovisteNumber { get; set; }
        public int? Tk { get; set; }
        public int? Tpz { get; set; }
        public int? Tm { get; set; }
    }
}
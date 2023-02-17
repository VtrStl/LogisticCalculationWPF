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
        private int TkSum { get; set; }
        private int TkMax { get; set; }
        private int TmSum { get; set;}
        private int TmWithValue { get; set; }
        private int PocetPracovist { get; set; }
        private int DavkaQ { get; set; }
        private int DavkaQD { get; set; }
        private int SystemZpracovani { get; set; }
        public int PocetPracovniku { get; private set; }

        public PrubeznaDobaModel(ObservableCollection<Pracoviste> PrubeznaDoba, int davkaQ, int davkaQD, int systemZpracovani)
        {
            Tpz1 = Convert.ToInt32(PrubeznaDoba[0].Tpz);
            TkSum = PrubeznaDoba.Sum(row => Convert.ToInt32(row.Tk));
            TkMax = PrubeznaDoba.Max(row => Convert.ToInt32(row.Tk));
            TmSum = PrubeznaDoba.Sum(row => Convert.ToInt32(row.Tm));
            TmWithValue = PrubeznaDoba.Count(row => Convert.ToInt32(row.Tm) !=0);
            PocetPracovist = PrubeznaDoba.Count;
            DavkaQ = davkaQ;
            DavkaQD = davkaQD;
            SystemZpracovani = systemZpracovani;
        }
        
        private int SoubezneJednotlive()
        {
            PocetPracovniku = PocetPracovist + TmWithValue;
            return Tpz1 + TkSum + (DavkaQ - 1) * TkMax + TmSum;
        }
        
        private int SoubeznePoDavkach()
        {
            PocetPracovniku = PocetPracovist + TmWithValue - DavkaQD;
            return Tpz1 + DavkaQD * TkSum + (DavkaQ - DavkaQD) * TkMax + TmSum;
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
}
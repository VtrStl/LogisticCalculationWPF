using LogisticCalculationWPF.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogisticCalculationWPF.Model
{
    public class PrubeznaDobaModel
    {
        private int Tpz1 { get; set; }
        private int TkSum { get; set; }
        private int TkMax { get; set; }
        private int TmSum { get; set; }
        private int TmWithValue { get; set; }
        private int PocetPracovist { get; set; }
        private int DavkaQ { get; set; }
        private int DavkaQD { get; set; }
        private int SystemZpracovani { get; set; }
        public int PocetPracovniku { get; private set; }

        public PrubeznaDobaModel(ObservableCollection<Pracoviste> PrubeznaDoba, int davkaQ, int davkaQD, int systemZpracovani)
        {
            Tpz1 = PrubeznaDoba[0].Tpz.GetValueOrDefault();
            TkSum = PrubeznaDoba.Sum(row => row.Tk.GetValueOrDefault());
            TkMax = PrubeznaDoba.Max(row => row.Tk.GetValueOrDefault());
            TmSum = PrubeznaDoba.Sum(row => row.Tm.GetValueOrDefault());
            TmWithValue = PrubeznaDoba.Count(row => row.Tm.GetValueOrDefault() != 0);
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
        public string PrubeznaDobaSystemyText()
        {
            return SystemZpracovani switch
            {
                0 => "Souběžně, jednotlivě, překryté",
                1 => "Souběžně, po dávkách, překryté",
                _ => ""
            }; 
        }
    }
}
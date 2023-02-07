using LogisticCalculationWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCalculationWPF.Model
{
    public class QoptModel
    {
        private double? VelikostPoptavky { get; set; }
        private double? Npz { get; set; }
        private double? Ns { get; set; }
        private double? Nj { get; set; }
        private double? Obdobi { get; set; }

        public QoptModel(double? velikostPoptavky, double? npz, double? ns, double? nj, double? obdobi) 
        { 
            VelikostPoptavky= velikostPoptavky;
            Npz = npz;
            Ns = ns;
            Nj = nj;
            Obdobi = obdobi;
        }

        public double Qopt()
        {
            return Math.Ceiling(Math.Sqrt(2 * (VelikostPoptavky??0) * (Npz??0)) / Math.Sqrt((Nj??0) * (Ns??0) * (Obdobi??0)));
        }
        
        public double PocetDavek()
        {
            return Math.Ceiling(Convert.ToDouble(VelikostPoptavky / Qopt()));
        }
        
        public double PeriodicitaZadavani()
        {
            return Math.Ceiling(Convert.ToDouble(360 * Obdobi / PocetDavek()));
        }
        public double CelkoveNaklady()
        {
            double prislusneNaklady = Convert.ToDouble(Qopt() / 2 * Nj * Ns * Obdobi);
            return Math.Round(Convert.ToDouble(VelikostPoptavky / Qopt() * Npz + prislusneNaklady), 2);            
        }
    }
}
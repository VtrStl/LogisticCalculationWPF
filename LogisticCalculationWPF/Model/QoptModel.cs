using System;

namespace LogisticCalculationWPF.Model
{
    public class QoptModel
    {
        private double VelikostPoptavky { get; set; }
        private double Npz { get; set; }
        private double Ns { get; set; }
        private double Nj { get; set; }
        private double Obdobi { get; set; }

        public QoptModel(double? velikostPoptavky, double? npz, double? ns, double? nj, double? obdobi)
        {
            VelikostPoptavky = velikostPoptavky.GetValueOrDefault();
            Npz = npz.GetValueOrDefault();
            Ns = ns.GetValueOrDefault();
            Nj = nj.GetValueOrDefault();
            Obdobi = obdobi.GetValueOrDefault();
        }

        public double Qopt()
        {
            return Math.Ceiling(Math.Sqrt(2 * VelikostPoptavky * Npz) / Math.Sqrt(Nj * Ns * Obdobi));
        }

        public double PocetDavek()
        {
            return Math.Ceiling(VelikostPoptavky / Qopt());
        }

        public double PeriodicitaZadavani()
        {
            return Math.Ceiling(360 * Obdobi / PocetDavek());
        }
        public double CelkoveNaklady()
        {
            double PrislusneNaklady = Qopt() / 2 * Nj * Ns * Obdobi;
            return Math.Round(VelikostPoptavky / Qopt() * Npz + PrislusneNaklady, 2);
        }
    }
}
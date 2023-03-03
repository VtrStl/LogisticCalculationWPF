using System;

namespace LogisticCalculationWPF.Model
{
    public class AnalyzaZasobModel
    {
        private double Spotreba { get; set; }
        private double ObjednavaciDavka { get; set; }
        private double PojistnaZasoba { get; set; }
        private double PokrytiPoptavky { get; set; }
        private double DodaciLhuta { get; set; }
        public double DnyNaTyden { get; set; }
        private int IntervalKontroly { get; set; }
        private int Systemy { get; set; }
        private double XPojistnaZasoba { get; set; }
        private double OcekavanaSpotreba { get; set; }

        public AnalyzaZasobModel(double? spotreba, double? objednavaciDavka, double? pojistnaZasoba, double? pokrytiPoptavky, double? dodaciLhuta, double? dnynarok,
            int? intervalKontroly, int systemy)
        {
            Spotreba = spotreba.GetValueOrDefault();
            ObjednavaciDavka = objednavaciDavka.GetValueOrDefault();
            PojistnaZasoba = pojistnaZasoba.GetValueOrDefault();
            PokrytiPoptavky = pokrytiPoptavky.GetValueOrDefault();
            DodaciLhuta = dodaciLhuta.GetValueOrDefault();
            DnyNaTyden = dnynarok.GetValueOrDefault();
            IntervalKontroly = intervalKontroly.GetValueOrDefault();
            Systemy = systemy;
            OcekavanaSpotreba = Spotreba / DnyNaTyden;
            XPojistnaZasoba = OcekavanaSpotreba * PokrytiPoptavky;
        }

        private double BQsystem()
        {
            switch (PokrytiPoptavky)
            {
                case > 0:
                    return Math.Ceiling(XPojistnaZasoba + DodaciLhuta * OcekavanaSpotreba);
                default:
                    return Math.Ceiling(PojistnaZasoba + DodaciLhuta * OcekavanaSpotreba);
            }
        }

        private double SQsystem()
        {
            switch (PokrytiPoptavky)
            {
                case > 0:
                    return Math.Ceiling(XPojistnaZasoba + OcekavanaSpotreba * (DodaciLhuta + 0.7 * IntervalKontroly));
                default:
                    return Math.Ceiling(PojistnaZasoba + OcekavanaSpotreba * (DodaciLhuta + 0.7 * IntervalKontroly));
            }
        }

        public double ObjUrovenVysledek()
        {
            return Systemy switch
            {
                0 => BQsystem(),
                1 => SQsystem(),
                _ => 0
            };

        }

        public string ObjUrovenText()
        {
            return Systemy switch
            {
                0 => "B,Q",
                1 => "s,Q",
                _ => ""
            };
        }
        public double PrumernaZasoba()
        {
            double TydnyNaDny = DnyNaTyden * 7;
            return Math.Round(TydnyNaDny / OcekavanaSpotreba, 2);
        }
        public double PocetObjednavekZaRok()
        {
            return Math.Ceiling(Spotreba / ObjednavaciDavka);
        }
    }
}
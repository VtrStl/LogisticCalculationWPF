using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCalculationWPF.Model
{
    public class AnalyzaZasobModel
    {
        private double? Spotreba;
        private double? ObjednavaciDavka;
        private double? PojistnaZasoba;
        private double? PokrytiPoptavky;
        private double? DodaciLhuta;
        public double? DnyNaTyden;
        private int? IntervalKontroly;
        private int Systemy;
        private double? xPojistnaZasoba;
        private double? OcekavanaSpotreba;
        public double? BQsystem;
        private double? sQsystem;

        public AnalyzaZasobModel(double? spotreba, double? objednavaciDavka, double? pojistnaZasoba, double? pokrytiPoptavky, double? dodaciLhuta, double? dnynarok,
            int? intervalKontroly, int systemy)
        {
            Spotreba = spotreba;
            ObjednavaciDavka = objednavaciDavka;
            PojistnaZasoba = pojistnaZasoba;
            PokrytiPoptavky = pokrytiPoptavky;
            DodaciLhuta = dodaciLhuta;
            DnyNaTyden = dnynarok;
            IntervalKontroly = intervalKontroly;
            Systemy = systemy;
            OcekavanaSpotreba = Spotreba / DnyNaTyden;
        }
        
        public void VyberSystem()
        {
            switch (Systemy)
            {
                case 0:
                    BQsystemKalkulace();
                    break;
                case 1:
                    SQsystemKalkulace();
                    break;
            }
        }

        private void BQsystemKalkulace()
        {
            switch (PokrytiPoptavky)
            {
                case > 0:
                    xPojistnaZasoba = (OcekavanaSpotreba??0) * (PokrytiPoptavky??0);
                    BQsystem = Math.Ceiling((xPojistnaZasoba??0) + (DodaciLhuta??0) * (OcekavanaSpotreba??0));
                    break;
                default:
                    BQsystem = Math.Ceiling((PojistnaZasoba??0) + (DodaciLhuta??0) * (OcekavanaSpotreba??0));
                    break;
            }
        }

        private void SQsystemKalkulace()
        {
            switch (PokrytiPoptavky)
            {
                case > 0:
                    xPojistnaZasoba = (OcekavanaSpotreba??0) * (PokrytiPoptavky??0);
                    sQsystem = Math.Ceiling((xPojistnaZasoba??0) + (OcekavanaSpotreba??0) * ((DodaciLhuta??0) + 0.7 * (IntervalKontroly??0)));
                    break; 
                default:
                    sQsystem = Math.Ceiling((PojistnaZasoba??0) + (OcekavanaSpotreba??0) * ((DodaciLhuta ?? 0) + 0.7 * (IntervalKontroly??0)));
                    break;
            }
        }
        public double? VypisVysledek()
        {
            return Systemy switch
            {
                0 => BQsystem,
                1 => sQsystem,
                _ => null
            }; 
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using LogisticCalculationWPF.Model;

namespace LogisticCalculationWPF.ViewModel
{
    public class AnalyzaZasobViewModel: INotifyPropertyChanged 
    {
        private AnalyzaZasobModel? Kalkulace;
        private double? spotreba;
        private double? objednavaciDavka;
        private double? dnyTydny;
        private double pojistnaZasoba;
        private double pokrytiPoptavky;
        private double? dodaciLhuta;
        private int? intervalKontroly;
        private int systemyZasob;
        private string vysledekAnalyzaZasob;

        public double? Spotreba
        {
            get { return spotreba; }
            set { spotreba = value; OnPropertyChanged(nameof(Spotreba)); }
        }
        public double? ObjednavaciDavka
        {
            get { return objednavaciDavka; }
            set { objednavaciDavka = value; OnPropertyChanged(nameof(ObjednavaciDavka)); }
        }       
        public double PojistnaZasoba 
        {
            get { return pojistnaZasoba; } 
            set { pojistnaZasoba = value; }
        }
        public double PokrytiPoptavky
        {
            get { return pokrytiPoptavky; }
            set { pokrytiPoptavky = value; }
        }
        public double? DodaciLhuta
        {
            get { return dodaciLhuta; }
            set { dodaciLhuta = value; OnPropertyChanged(nameof(DodaciLhuta)); }
        }
        public double? DnyTydny
        {
            get { return dnyTydny; }
            set { dnyTydny = value; OnPropertyChanged(nameof(DnyTydny)); }
        }
        public int? IntervalKontroly 
        {
            get { return intervalKontroly; }
            set { intervalKontroly = value;}
        }
        public int SystemyZasob 
        {
            get { return systemyZasob; }
            set { systemyZasob = value; OnPropertyChanged(nameof(SystemyZasob)); }
        }
        public string VysledekAnalyzaZasob 
        {
            get { return vysledekAnalyzaZasob; }
            set { vysledekAnalyzaZasob = value; OnPropertyChanged(nameof(VysledekAnalyzaZasob)); }
        }
        public ICommand VypocitejAnalyzaZasob { get; }
        public ICommand PrevodDnyNaTydny { get; }

        public AnalyzaZasobViewModel()
        {
            VypocitejAnalyzaZasob = new RelayCommand(AnalyzaZasobVypocet);
            PrevodDnyNaTydny = new RelayCommand(Prevod);
            vysledekAnalyzaZasob = "";
        }

        private void AnalyzaZasobVypocet()
        {
            Kalkulace = new AnalyzaZasobModel(spotreba, objednavaciDavka, pojistnaZasoba, pokrytiPoptavky, dodaciLhuta, dnyTydny, intervalKontroly, systemyZasob);
            VysledekAnalyzaZasob = $"{Kalkulace.ObjednavaciUrovenVysledek()} Ks budem objednávat\r\n" +
                $"{Kalkulace.PrumernaZasoba()} týdnů nám vystačí zásoba\r\n" +
                $"{Kalkulace.PocetObjednavekZaRok()}x budeme objednávat za rok";
        }
        
        private void Prevod()
        {
            DnyTydny = Math.Round(Convert.ToDouble(dnyTydny) / 7, 2);
        }
               
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using LogisticCalculationWPF.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LogisticCalculationWPF.ViewModel
{
    public class AnalyzaZasobViewModel: INotifyPropertyChanged 
    {
        private AnalyzaZasobModel? Kalkulace;
        private ObservableCollection<VysledekAnalyzaZasobDG> vysledekAnalyzaZasob;
        public ObservableCollection<VysledekAnalyzaZasobDG> VysledekAnalyzaZasob
        {
            get { return vysledekAnalyzaZasob; }
            private set
            {
                vysledekAnalyzaZasob = value;
                OnPropertyChanged(nameof(VysledekAnalyzaZasob));
            }
        }
        private double? spotreba;
        public double? Spotreba
        {
            get { return spotreba; }
            set { spotreba = value; OnPropertyChanged(nameof(Spotreba)); }
        }
        private double? objednavaciDavka;
        public double? ObjednavaciDavka
        {
            get { return objednavaciDavka; }
            set { objednavaciDavka = value; OnPropertyChanged(nameof(ObjednavaciDavka)); }
        }

        private double pojistnaZasoba;
        public double PojistnaZasoba 
        {
            get { return pojistnaZasoba; } 
            set { pojistnaZasoba = value; }
        }
        private double pokrytiPoptavky;
        public double PokrytiPoptavky
        {
            get { return pokrytiPoptavky; }
            set { pokrytiPoptavky = value; }
        }
        private double? dodaciLhuta;
        public double? DodaciLhuta
        {
            get { return dodaciLhuta; }
            set { dodaciLhuta = value; OnPropertyChanged(nameof(DodaciLhuta)); }
        }
        private double? dnyTydny;
        public double? DnyTydny
        {
            get { return dnyTydny; }
            set { dnyTydny = value; OnPropertyChanged(nameof(DnyTydny)); }
        }
        private int? intervalKontroly;
        public int? IntervalKontroly 
        {
            get { return intervalKontroly; }
            set { intervalKontroly = value;}
        }
        private int systemyZasob;
        public int SystemyZasob 
        {
            get { return systemyZasob; }
            set { systemyZasob = value; OnPropertyChanged(nameof(SystemyZasob)); }

            
        }                
        
        public ICommand VypocitejAnalyzaZasob { get; }
        public ICommand PrevodDnyNaTydny { get; }

        public AnalyzaZasobViewModel()
        {
            vysledekAnalyzaZasob = new ObservableCollection<VysledekAnalyzaZasobDG>();
            VypocitejAnalyzaZasob = new RelayCommand(AnalyzaZasobVypocet);
            PrevodDnyNaTydny = new RelayCommand(Prevod);
        }

        private void AnalyzaZasobVypocet()
        {
            Kalkulace = new AnalyzaZasobModel(spotreba, objednavaciDavka, pojistnaZasoba, pokrytiPoptavky, dodaciLhuta, dnyTydny, intervalKontroly, systemyZasob);
            VysledekAnalyzaZasob.Add(new VysledekAnalyzaZasobDG() { SystemAnalyzaZasob = Kalkulace.ObjUrovenText(),
                ObjednavaciUrovenVysledek = Kalkulace.ObjUrovenVysledek(), PrumernaZasoba = Kalkulace.PrumernaZasoba(), PocetObjednavekZaRok = Kalkulace.PocetObjednavekZaRok() }); 
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
    
    public class VysledekAnalyzaZasobDG
    {
        public string? SystemAnalyzaZasob { get; set; }
        public double ObjednavaciUrovenVysledek { get; set; }
        public double PrumernaZasoba { get; set; }
        public double PocetObjednavekZaRok { get; set; }
    }
}
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
        private float? spotreba;
        private float? objednavaciDavka;
        private float? tydnyRok;
        private float? pojistnaZasoba;
        private float? pokrytiPoptavky;
        private float? dodaciLhuta;
        private int? intervalKontroly;
        private bool _SystemyZasob;

        public float? Spotreba 
        { 
            get { return spotreba; }
            set { spotreba = value; OnPropertyChanged(nameof(Spotreba)); }        
        }
        public float? ObjednavaciDavka 
        {
            get { return objednavaciDavka; }
            set { objednavaciDavka = value; OnPropertyChanged(nameof(ObjednavaciDavka)); }
        }
        public float? TydnyRok 
        {
            get { return tydnyRok; } 
            set { tydnyRok = value; OnPropertyChanged(nameof(TydnyRok)); }
        }
        public float? PojistnaZasoba 
        {
            get { return pojistnaZasoba; } 
            set { pojistnaZasoba = value; }
        }
        public float? PokrytiPoptavky
        {
            get { return pokrytiPoptavky; }
            set { pokrytiPoptavky = value; }
        }
        public float? DodaciLhuta
        {
            get { return dodaciLhuta; }
            set { dodaciLhuta = value; OnPropertyChanged(nameof(DodaciLhuta)); }
        }
        public int? IntervalKontroly 
        {
            get { return intervalKontroly; }
            set { intervalKontroly = value;}
        }
        public bool SystemyZasob 
        {
            get { return _SystemyZasob; }
            set { _SystemyZasob = value; OnPropertyChanged(nameof(SystemyZasob)); }
        }
        public ICommand VypocitejAnalyzaZasob { get; set; }
        public ICommand PrevodDnyNaRok { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using LogisticCalculationWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Security.Permissions;

namespace LogisticCalculationWPF.ViewModel
{
    class QoptDavkaViewModel: INotifyPropertyChanged
    {
        private KalkulaceModel kalkulace;
        private double? davka;
        private double? npz;
        private double? ns;
        private double? nj;
        private double? obdobi;
        private string vysledek;

        public QoptDavkaViewModel()
        {
            VypocitejQoptBt = new RelayCommand(Kalkulace);
            vysledek = "";
        }
        public double? Davka 
        { 
            get { return davka; } 
            set { davka = value; OnPropertyChanged(nameof(Davka)); } 
        }
        public double? Npz 
        { 
            get { return npz; }
            set { npz = value; OnPropertyChanged(nameof(Npz)); } 
        }
        public double? Ns {
            get { return ns; } 
            set { ns = value; OnPropertyChanged(nameof(Ns)); } 
        }
        public double? Nj {
            get { return nj; } 
            set { nj = value; OnPropertyChanged(nameof(Nj)); } 
        }
        public double? Obdobi {
            get { return obdobi; } 
            set { obdobi = value; OnPropertyChanged(nameof(obdobi)); } 
        }
        public string VysledekQopt 
        { 
            get { return vysledek; } 
            set { vysledek = value; OnPropertyChanged(nameof(VysledekQopt)); } 
        }
        public ICommand VypocitejQoptBt { get; set; }

        private void Kalkulace()
        {
            kalkulace = new KalkulaceModel(davka, npz, ns, nj, obdobi);
            VysledekQopt = 
                $"Optimální dávka: {kalkulace.Qopt()}\r\n" +
                $"Počet dávek: {kalkulace.PocetDavek()}\r\n" +
                $"Periodicita Zadávání: {kalkulace.PeriodicitaZadavani()} dnů (360 dnů)\r\n" +
                $"Celkové náklady: {kalkulace.CelkoveNaklady()} CZK";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
using CommunityToolkit.Mvvm.Input;
using LogisticCalculationWPF.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace LogisticCalculationWPF.ViewModel
{
    public class PrubeznaDobaViewModel : INotifyPropertyChanged
    {
        private PrubeznaDobaModel? prubeznaDobaModel;
        private ObservableCollection<Pracoviste> prubeznaDoba;
        public ObservableCollection<Pracoviste> PrubeznaDoba
        {
            get { return prubeznaDoba; }
            private set
            {
                prubeznaDoba = value;
                OnPropertyChanged(nameof(PrubeznaDoba));
            }
        }
        private ObservableCollection<VysledekPrubeznaDobaDG> vysledekPrubeznaDoba;
        public ObservableCollection<VysledekPrubeznaDobaDG> VysledekPrubeznaDoba
        {
            get { return vysledekPrubeznaDoba; }
            private set
            {
                vysledekPrubeznaDoba = value;
                OnPropertyChanged(nameof(VysledekPrubeznaDoba));
            }
        }
        private int davkaQ;
        public int DavkaQ
        {
            get { return davkaQ; }
            set { davkaQ = value; OnPropertyChanged(nameof(DavkaQ)); }
        }
        private int davkaQD;
        public int DavkaQD
        {
            get { return davkaQD; }
            set { davkaQD = value; OnPropertyChanged(nameof(DavkaQD)); }
        }
        private int systemZpracovani;
        public int SystemZpracovani
        {
            get { return systemZpracovani; }
            set { systemZpracovani = value; OnPropertyChanged(nameof(SystemZpracovani)); }
        }

        private static int PracovisteID = 1;
        public ICommand PridatPracovisteButton { get; }
        public ICommand OdebratPracovisteButton { get; }
        public ICommand VypocitatPrubButton { get; }

        public PrubeznaDobaViewModel()
        {
            prubeznaDoba = new ObservableCollection<Pracoviste>();
            vysledekPrubeznaDoba = new ObservableCollection<VysledekPrubeznaDobaDG>();
            PridatPracovisteButton = new RelayCommand(PridatPracoviste);
            OdebratPracovisteButton = new RelayCommand(OdebratPracoviste);
            VypocitatPrubButton = new RelayCommand(VypocitatTabulku);
        }

        private void VypocitatTabulku()
        {
            try
            {
                prubeznaDobaModel = new PrubeznaDobaModel(prubeznaDoba, davkaQ, davkaQD, systemZpracovani);
                var vysledek = new VysledekPrubeznaDobaDG()
                {
                    PrubeznaDobaID = VysledekPrubeznaDoba.Count + 1,
                    SystemyPrubeznaDoba = prubeznaDobaModel.PrubeznaDobaSystemyText(),
                    PrubeznaDobaVysledek = prubeznaDobaModel.PrubeznaDobaVysledek(),
                    PocetPracovist = PracovisteID - 1,
                    PocetPracovniku = prubeznaDobaModel.PocetPracovniku,
                    DavkaQin = DavkaQ,
                    DavkaQDin = DavkaQD
                };
                VysledekPrubeznaDoba.Add(vysledek);
                OnPropertyChanged(nameof(VysledekPrubeznaDoba));
            }
            catch (Exception ex) { MessageBox.Show("Chyba: " + ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void PridatPracoviste()
        {
            PrubeznaDoba.Add(new Pracoviste() { PracovisteNumber = PracovisteID });
            PracovisteID++;
            OnPropertyChanged(nameof(PrubeznaDoba));
        }

        private void OdebratPracoviste()
        {
            if (PrubeznaDoba.Count > 0)
            {
                PrubeznaDoba.RemoveAt(prubeznaDoba.Count - 1);
                PracovisteID--;
                OnPropertyChanged(nameof(PrubeznaDoba));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Pracoviste
    {
        public int? PracovisteNumber { get; set; }
        public int? Tk { get; set; }
        public int? Tpz { get; set; }
        public int? Tm { get; set; }
    }

    public class VysledekPrubeznaDobaDG
    {
        public int PrubeznaDobaID { get; set; }
        public string? SystemyPrubeznaDoba { get; set; }
        public int PrubeznaDobaVysledek { get; set; }
        public int PocetPracovist { get; set; }
        public int PocetPracovniku { get; set; }
        public int DavkaQin { get; set; }
        public int DavkaQDin { get; set; }
    }
}
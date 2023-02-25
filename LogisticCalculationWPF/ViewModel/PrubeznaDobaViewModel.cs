using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using LogisticCalculationWPF.Model;

namespace LogisticCalculationWPF.ViewModel
{
    public class PrubeznaDobaViewModel : INotifyPropertyChanged
    {
        private PrubeznaDobaModel? kalkulace;
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
            get { return systemZpracovani;}
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
                kalkulace = new PrubeznaDobaModel(prubeznaDoba, davkaQ, davkaQD, systemZpracovani);
                VysledekPrubeznaDoba.Add(new VysledekPrubeznaDobaDG() { SystemyPrubeznaDoba = kalkulace.PrubeznaDobaSystemyText(), 
                    PrubeznaDobaVysledek = kalkulace.PrubeznaDobaVysledek(), PocetPracovniku = kalkulace.PocetPracovniku, DavkaQin = DavkaQ, DavkaQDin = DavkaQD });                
            }
            catch (Exception ex) { MessageBox.Show("Chyba: " + ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void PridatPracoviste()
        {
            prubeznaDoba.Add(new Pracoviste() { PracovisteNumber = PracovisteID});
            PracovisteID++;
        }

        private void OdebratPracoviste()
        {
            if (prubeznaDoba.Count > 0)
            {
                prubeznaDoba.RemoveAt(prubeznaDoba.Count - 1);
                PracovisteID--;
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
        public string? SystemyPrubeznaDoba { get; set; }
        public int PrubeznaDobaVysledek { get; set; }
        public int PocetPracovniku { get; set; }
        public int DavkaQin { get; set; }
        public int DavkaQDin { get; set; }
    }
}
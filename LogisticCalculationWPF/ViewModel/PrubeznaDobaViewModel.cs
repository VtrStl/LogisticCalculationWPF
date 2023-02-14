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
        private PrubeznaDobaModel kalkulace;
        private ObservableCollection<Pracoviste> prubeznaDoba;
        public ObservableCollection<Pracoviste> PrubeznaDobaVM
        {
            get { return prubeznaDoba; }
            set
            {
                prubeznaDoba = value;
                OnPropertyChanged(nameof(PrubeznaDobaVM));
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
        private string vysledekPrubeznaDoba;
        public string VysledekPrubeznaDoba
        {
            get { return vysledekPrubeznaDoba; }
            set { vysledekPrubeznaDoba = value; OnPropertyChanged(nameof(VysledekPrubeznaDoba)); }
        }
        private int PracovisteCounter = 1;
        public ICommand PridatPracovisteButton { get; set; }
        public ICommand OdebratPracovisteButton { get; set; }
        public ICommand VypocitatPrubButton { get; set; }

        public PrubeznaDobaViewModel()
        {
            prubeznaDoba = new ObservableCollection<Pracoviste>();
            PridatPracovisteButton = new RelayCommand(PridatPracoviste);
            OdebratPracovisteButton = new RelayCommand(OdebratPracoviste);
            VypocitatPrubButton = new RelayCommand(VypocitatTabulku);
        }

        private void VypocitatTabulku()
        {
            try
            {
                kalkulace = new PrubeznaDobaModel(prubeznaDoba, davkaQ, davkaQD, systemZpracovani);
                VysledekPrubeznaDoba = $"T = {kalkulace.PrubeznaDobaVysledek()} min";
            }
            catch (Exception ex) { MessageBox.Show("Chyba: " + ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void PridatPracoviste()
        {
            prubeznaDoba.Add(new Pracoviste() { PracovisteNumber = PracovisteCounter});
            PracovisteCounter++;
        }

        private void OdebratPracoviste()
        {
            if (prubeznaDoba.Count > 0)
            {
                prubeznaDoba.RemoveAt(prubeznaDoba.Count - 1);
                PracovisteCounter--;
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
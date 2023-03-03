using CommunityToolkit.Mvvm.Input;
using LogisticCalculationWPF.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace LogisticCalculationWPF.ViewModel
{
    public class QoptDavkaViewModel : INotifyPropertyChanged
    {
        private QoptModel? kalkulace;
        private ObservableCollection<VysledekQoptDG> vysledekQOPT;
        public ObservableCollection<VysledekQoptDG> VysledekQOPT
        {
            get { return vysledekQOPT; }
            private set
            {
                vysledekQOPT = value;
                OnPropertyChanged(nameof(VysledekQOPT));
            }
        }
        private double? davka;
        public double? Davka
        {
            get { return davka; }
            set { davka = value; OnPropertyChanged(nameof(Davka)); }
        }
        private double? npz;
        public double? Npz
        {
            get { return npz; }
            set { npz = value; OnPropertyChanged(nameof(Npz)); }
        }
        private double? ns;
        public double? Ns
        {
            get { return ns; }
            set { ns = value; OnPropertyChanged(nameof(Ns)); }
        }
        private double? nj;
        public double? Nj
        {
            get { return nj; }
            set { nj = value; OnPropertyChanged(nameof(Nj)); }
        }
        private double? obdobi;
        public double? Obdobi
        {
            get { return obdobi; }
            set { obdobi = value; OnPropertyChanged(nameof(obdobi)); }
        }

        public ICommand VypocitejQoptBt { get; }

        public QoptDavkaViewModel()
        {
            vysledekQOPT = new ObservableCollection<VysledekQoptDG>();
            VypocitejQoptBt = new RelayCommand(Kalkulace);
        }

        private void Kalkulace()
        {
            kalkulace = new QoptModel(davka, npz, ns, nj, obdobi);
            VysledekQOPT.Add(new VysledekQoptDG()
            {
                QoptID = vysledekQOPT.Count + 1,
                Qopt = kalkulace.Qopt(),
                PocetDavek = kalkulace.PocetDavek(),
                PeriodicitaZadavani = kalkulace.PeriodicitaZadavani(),
                CelkoveNaklady = kalkulace.CelkoveNaklady()
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class VysledekQoptDG
    {
        public int QoptID { get; set; }
        public double Qopt { get; set; }
        public double PocetDavek { get; set; }
        public double PeriodicitaZadavani { get; set; }
        public double CelkoveNaklady { get; set; }
    }
}
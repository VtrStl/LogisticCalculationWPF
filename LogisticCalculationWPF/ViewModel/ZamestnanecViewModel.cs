using LogisticCalculationWPF.Model;
using LogisticCalculationWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticCalculationWPF.ViewModel
{
    //For future use
    public class ZamestnanecViewModel : INotifyPropertyChanged
    {
        private readonly ZamestnanecModel zamestnanci;

        public ZamestnanecViewModel(ZamestnanecModel zamestnanec)
        {
            zamestnanci = zamestnanec;

        }
        public int Id
        {
            get { return zamestnanci.Id; }
            set
            {                
                zamestnanci.Id = value;
                OnPropertyChanged(nameof(Id));                
            }
        }
        public string Jmeno
        {
            get { return zamestnanci.Jmeno; }
            set
            {
                zamestnanci.Jmeno = value;
                OnPropertyChanged(nameof(Jmeno));                
            }
        }
        public string Prijmeni
        {
            get { return zamestnanci.Prijmeni; }
            set
            {
                zamestnanci.Prijmeni = value;
                OnPropertyChanged(nameof(Prijmeni));                
            }
        }
        public DateOnly Narozeni
        {
            get { return zamestnanci.Narozeni; }
            set
            {               
                zamestnanci.Narozeni = value;
                OnPropertyChanged(nameof(Narozeni));
            }
        }
        public string PracovniPomer
        {
            get { return zamestnanci.PracovniPomer; }
            set
            {            
                zamestnanci.PracovniPomer = value;
                OnPropertyChanged(nameof(PracovniPomer));    
            }
        }
        public DateOnly ZamestnanOd
        {
            get { return zamestnanci.ZamestnanOd; }
            set
            {                
                zamestnanci.ZamestnanOd = value;
                OnPropertyChanged(nameof(ZamestnanOd));                
            }
        }
        public DateOnly? ZamestnanDo
        {
            get { return zamestnanci.ZamestnanDo; }
            set
            {               
                zamestnanci.ZamestnanDo = value;
                OnPropertyChanged(nameof(ZamestnanDo));                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
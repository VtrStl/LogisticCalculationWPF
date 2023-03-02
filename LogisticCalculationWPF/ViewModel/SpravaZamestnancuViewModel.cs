using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LogisticCalculationWPF.Model;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Data;
using System.Collections.ObjectModel;

namespace LogisticCalculationWPF.ViewModel
{
    public class SpravaZamestnancuViewModel
    {
        private readonly ZamestnanecRepository _zamestnanecRepository;
        
        private ObservableCollection<ZamestnanecModel> _zamestnanec;
        public ObservableCollection<ZamestnanecModel> Zamestnanec
        {
            get { return _zamestnanec; }
            set
            {
                if (_zamestnanec != value)
                {
                    _zamestnanec = value;
                    OnPropertyChanged(nameof(Zamestnanec));
                }
            }
        }
        public ICommand NacistDatabaziBT { get; }
                
        public SpravaZamestnancuViewModel()
        {
            _zamestnanecRepository = new ZamestnanecRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZamestnanciDB;Integrated Security=True");
            NacistDatabaziBT = new RelayCommand(NactiDatabazi);
            Zamestnanec = new ObservableCollection<ZamestnanecModel>(_zamestnanecRepository.ZiskejZamestnance());
        }

        private void NactiDatabazi()
        {
            Zamestnanec = new ObservableCollection<ZamestnanecModel>(_zamestnanecRepository.ZiskejZamestnance());
            MessageBox.Show("funguji");
        }
        
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
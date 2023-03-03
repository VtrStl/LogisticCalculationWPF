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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

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
            _zamestnanec = new ObservableCollection<ZamestnanecModel>();
        }

        private void NactiDatabazi()
        {
            var zamestnanci = _zamestnanecRepository.ZiskejZamestnance();
            foreach (var zamestnanec in zamestnanci)
            {
                Zamestnanec.Add(zamestnanec);
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
    }
    
    public class DateTimeToShortDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly dateTime)
            {
                CultureInfo customCulture = new CultureInfo("cs-CZ");
                return dateTime.ToString("d", customCulture);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse((string)value, out DateTime result))
            {
                return DateOnly.FromDateTime(result);
            }
            else
            {
                return Binding.DoNothing;
            }
        }
    }
}
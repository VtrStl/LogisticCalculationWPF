using LogisticCalculationWPF.ViewModel;
using LogisticCalculationWPF;
using LogisticCalculationWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LogisticCalculationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CoreViewModel()
            {
                QoptVM = new QoptDavkaViewModel(),
                AnalyzaZasobVM = new AnalyzaZasobViewModel(),
                PrubeznaDobaVM = new PrubeznaDobaViewModel(),
                SpravaVM = new SpravaZamestnancuViewModel()
            };            
        }

        private void UvodButton_Click(object sender, RoutedEventArgs e)
        {
            tabController.SelectedValue = UvodTab;
        }

        private void QoptButton_Click(object sender, RoutedEventArgs e)
        {
            tabController.SelectedValue = DavkaTab;
        }

        private void AnalyzatButton_Click(object sender, RoutedEventArgs e)
        {
            tabController.SelectedValue = AnalyzaZasobTab;
        }

        private void PrubeznaDobaButton_Click(object sender, RoutedEventArgs e)
        {
            tabController.SelectedValue = PrubeznaDobaTab;
        }

        private void SpravaZamestnancuButton_Click(object sender, RoutedEventArgs e)
        {
            tabController.SelectedValue = SpravaZamestnancuTab;
        }
    }
}
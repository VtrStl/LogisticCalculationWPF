using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using LogisticCalculationWPF.Model;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections;

namespace LogisticCalculationWPF.Model
{
    public class ZamestnanecRepository
    {
        private readonly string _connectionString;

        public ZamestnanecRepository(string connectionString)
        {
            _connectionString = connectionString;
            
        }
        
        public ObservableCollection<ZamestnanecModel> ZiskejZamestnance()
        {
            var zamestnanci = new ObservableCollection<ZamestnanecModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand query = new SqlCommand("SELECT * FROM dbo.Zamestnanci", connection);
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);              
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);         
                foreach (DataRow row in dataTable.Rows) 
                {
                    ZamestnanecModel zamestnanec = new ZamestnanecModel
                    {
                        Id = (int)row["ID"],
                        Jmeno = (string)row["Jmeno"],
                        Prijmeni = (string)row["Prijmeni"],
                        Narozeni = (DateTime)row["Narozeni"],
                        PracovniPomer = (string)row["PracovniPomer"],
                        ZamestnanOd = (DateTime)row["ZamestnanOd"],
                        ZamestnanDo = (DateTime?)row["ZamestnanDo"] 
                    };
                    zamestnanci.Add(zamestnanec);                    
                }                
            }            
            return zamestnanci;
        }
    }
}
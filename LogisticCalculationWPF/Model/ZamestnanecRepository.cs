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
                SqlCommand command = new("SELECT * FROM dbo.Zamestnanci", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();        
                while(reader.Read()) 
                {
                    ZamestnanecModel zamestnanec = new()
                    {
                        Id = (int)reader["Id"],
                        Jmeno = (string)reader["Jmeno"],
                        Prijmeni = (string)reader["Prijmeni"],
                        Narozeni = DateOnly.FromDateTime((DateTime)reader["Narozeni"]),
                        PracovniPomer = (string)reader["PracovniPomer"],
                        ZamestnanOd = DateOnly.FromDateTime((DateTime)reader["ZamestnanOd"]),
                        ZamestnanDo = reader.IsDBNull(reader.GetOrdinal("ZamestnanDo")) ? null : DateOnly.FromDateTime((DateTime)reader["ZamestnanDo"])
                    };
                    zamestnanci.Add(zamestnanec);                    
                }                
            }            
            return zamestnanci;
        }
    }
}
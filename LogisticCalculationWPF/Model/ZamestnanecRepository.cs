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
                string query = "SELECT * FROM dbo.Zamestnanci";
                SqlDataAdapter adapter = new(query, connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "Zamestnanci");

                foreach (DataRow row in dataSet.Tables["Zamestnanci"].Rows)
                {
                    ZamestnanecModel zamestnanec = new()
                    {
                        Id = (int)row["Id"],
                        Jmeno = (string)row["Jmeno"],
                        Prijmeni = (string)row["Prijmeni"],
                        Narozeni = DateOnly.FromDateTime((DateTime)row["Narozeni"]),
                        PracovniPomer = (string)row["PracovniPomer"],
                        ZamestnanOd = DateOnly.FromDateTime((DateTime)row["ZamestnanOd"]),
                        ZamestnanDo = row.IsNull("ZamestnanDo") ? null : DateOnly.FromDateTime((DateTime)row["ZamestnanDo"])
                    };
                    zamestnanci.Add(zamestnanec);
                }
            }
            return zamestnanci;
        }

        public void UpravZamestnance(ObservableCollection<ZamestnanecModel> zamestnanci)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE dbo.Zamestnanci SET Jmeno = @Jmeno, Prijmeni = @Prijmeni, Narozeni = @Narozeni, PracovniPomer = @PracovniPomer, ZamestnanOd = @ZamestnanOd, ZamestnanDo = @ZamestnanDo WHERE Id = @Id", connection, transaction);

                    foreach (var zamestnanec in zamestnanci)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Id", zamestnanec.Id);
                        command.Parameters.AddWithValue("@Jmeno", zamestnanec.Jmeno);
                        command.Parameters.AddWithValue("@Prijmeni", zamestnanec.Prijmeni);
                        command.Parameters.AddWithValue("@Narozeni", new DateTime(zamestnanec.Narozeni.Year, zamestnanec.Narozeni.Month, zamestnanec.Narozeni.Day));
                        command.Parameters.AddWithValue("@PracovniPomer", zamestnanec.PracovniPomer);
                        command.Parameters.AddWithValue("@ZamestnanOd", new DateTime(zamestnanec.ZamestnanOd.Year, zamestnanec.ZamestnanOd.Month, zamestnanec.ZamestnanOd.Day));
                        command.Parameters.AddWithValue("@ZamestnanDo", zamestnanec.ZamestnanDo.HasValue ? new DateTime(zamestnanec.ZamestnanDo.Value.Year, zamestnanec.ZamestnanDo.Value.Month, zamestnanec.ZamestnanDo.Value.Day) : DBNull.Value);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Chyba při ukládání změn v databázi.", ex);
                }
            }
        }
    }    
}
﻿using System;
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
using System.ComponentModel.DataAnnotations;

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

            using (SqlConnection connection = new(_connectionString))
            {
                SqlDataAdapter adapter = new("SELECT * FROM dbo.Zamestnanci", connection);
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Zamestnanci", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Zamestnanci");
                dataSet.Tables["Zamestnanci"].PrimaryKey = new DataColumn[] { dataSet.Tables["Zamestnanci"].Columns["Id"] };
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                int maxId = dataSet.Tables["Zamestnanci"].AsEnumerable()
                    .Max(row => row.Field<int>("Id"));
                foreach (var zamestnanec in zamestnanci)
                {
                    var row = dataSet.Tables["Zamestnanci"].Rows.Find(zamestnanec.Id);
                    if (row == null)
                    {
                        row = dataSet.Tables["Zamestnanci"].NewRow();
                        maxId++;
                        row["Id"] = maxId;

                        dataSet.Tables["Zamestnanci"].Rows.Add(row);
                    }
                    row["Jmeno"] = zamestnanec.Jmeno;
                    row["Prijmeni"] = zamestnanec.Prijmeni;
                    row["Narozeni"] = ToDateTime(zamestnanec.Narozeni);
                    row["PracovniPomer"] = zamestnanec.PracovniPomer;
                    row["ZamestnanOd"] = ToDateTime(zamestnanec.ZamestnanOd);
                    row["ZamestnanDo"] = zamestnanec.ZamestnanDo.HasValue ? ToDateTime(zamestnanec.ZamestnanDo.Value) : DBNull.Value;
                }
                // Update the database with changes made to the data in the DataSet
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.Update(dataSet, "Zamestnanci");
            }
        }

        public void SmazZamestnance(int id, ObservableCollection<ZamestnanecModel> zamestnanci)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM dbo.Zamestnanci", connection);

                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Zamestnanci");
                dataSet.Tables["Zamestnanci"].PrimaryKey = new DataColumn[] { dataSet.Tables["Zamestnanci"].Columns["Id"] };
                var rowToDelete = dataSet.Tables["Zamestnanci"].Rows.Find(id);
                if (rowToDelete != null && rowToDelete.RowState != DataRowState.Deleted)
                {
                    rowToDelete.Delete();
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(dataSet, "Zamestnanci");
            }
        }

        public static DateTime ToDateTime(DateOnly dateOnly)
        {
            return new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
        }
    }    
}
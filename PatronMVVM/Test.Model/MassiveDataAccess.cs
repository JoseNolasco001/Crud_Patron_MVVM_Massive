using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model.Models;

namespace Test.Model
{
    public class MassiveDataAccess
    {
        public void SaveMassive(ObservableCollection<CustomerLocal> customers) 
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CustomerSaleDB"].ConnectionString))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Edad", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                DataRow dr;

                for (int i = 0; i < customers.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = customers[i].Nombre;
                    dr[1] = customers[i].Edad;
                    dr[2] = customers[i].Email;
                    dt.Rows.Add(dr);
                    Console.WriteLine(dr[0]);
                }

                SqlCommand command = new SqlCommand("[PuntoDeVenta].[InsTestCustomer]", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "testCustomers";
                parameter.Value = dt;
                command.Parameters.Add(parameter);

                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.ToString());
                    connection.Close();
                }
            }
        }
    }
}

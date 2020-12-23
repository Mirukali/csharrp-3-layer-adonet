﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BTCA_3_lop
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
        }

        private DataProvider() { }
        private string connectionStr = @"Data Source=ALIKUXAC;Initial Catalog=BaiTapLayer;User ID=sa;Password=demo123";
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] temp = query.Split(' ');
                    int i = 0;
                    List<string> listPara = new List<string>();
                    foreach (string item in temp)
                    {
                        if (item[0] == '@') listPara.Add(item);
                    }
                    for (i = 0; i < parameter.Length; i++)
                    {
                        command.Parameters.AddWithValue(listPara[i], parameter[i]);
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            try
            {

                int count = -1;
                using (var connection = new SqlConnection(query))
                {
                    connection.Open();
                    using (var objCommand = new SqlCommand(query, connection))
                    {

                        if (parameter != null)
                        {
                            var storeQuery = query.Split(' ');
                            int i = 0;
                            foreach (var item in storeQuery)
                            {
                                if (item.StartsWith("@"))
                                {
                                    objCommand.Parameters.AddWithValue(item, parameter[i]);
                                    i++;
                                }
                            }
                        }
                        count = objCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return count;
            }
            catch (System.Exception ex)
            {

                System.Diagnostics.Debug.WriteLine($"{ex.ToString() }");

                throw ex;
            }
            //throw new NotImplementedException();
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}

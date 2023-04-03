﻿using System.Data;
using Npgsql;

namespace WoCevv.DAL
{
    public class DatamodelDAL
    {
        public DatamodelDAL()
        {
            TestConnection();
        }

        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"User ID=root_evv;Password=WOC1evv1;Host=woc.postgres.database.azure.com;Port=5432;Database=woc-orchestrator;");
        }

        private static void TestConnection()
        {
            using (NpgsqlConnection con=GetConnection())
            {
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    Console.WriteLine("Connected");
                }
            }
        }

        public List<string> ReadAllDatamodelNames()
        {
            List<string> names = new List<string>();

            using (NpgsqlConnection con = GetConnection())
            {
                string query = "SELECT name FROM datamodels";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    names.Add(reader.GetString(0));
                }

                con.Close();
            }

            return names;
        }
    }
}
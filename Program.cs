using System;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost; Database=MikaDB; User Id=u_mika; Password=XZ9GFNj4;";
            int loop = 10;

            using SqlConnection connectionReader = new SqlConnection(connectionString);
            connectionReader.Open();

            using SqlConnection connectionWriter = new SqlConnection(connectionString);
            connectionWriter.Open();

            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionWriter);
            sqlBulkCopy.DestinationTableName = "session_wait_stats";

            do
            {
                using SqlCommand command = new SqlCommand("p_getData", connectionReader);
                using SqlDataReader reader = command.ExecuteReader();

                if (sqlBulkCopy.ColumnMappings.Count == 0)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        sqlBulkCopy.ColumnMappings.Add(columnName, columnName);
                    }
                }

                sqlBulkCopy.WriteToServer(reader);

                loop--;
            } while (loop > 0);
        }
    }
}

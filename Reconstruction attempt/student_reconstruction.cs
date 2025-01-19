using System;
using System.Data;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

class Program
{
    static void Main()
    {
        // Connection strings for each site
        string sqlServerConnectionString = "Server=your_sql_server;Database=your_database;User Id=your_user;Password=your_password;";
        string mySqlConnectionString = "Server=your_mysql_server;Database=your_database;User=your_user;Password=your_password;";
        string postgreSqlConnectionString = "Host=your_postgres_server;Database=your_database;Username=your_user;Password=your_password;";

        // DataTables to hold fragments
        DataTable studentGeneralInfo = new DataTable();
        DataTable studentMajorsScience = new DataTable();
        DataTable studentMajorsEngineering = new DataTable();
        DataTable studentMajorsArts = new DataTable();
        DataTable studentMajorsHumanities = new DataTable();

        // Fetch data from SQL Server (Site 1)
        using (SqlConnection sqlConn = new SqlConnection(sqlServerConnectionString))
        {
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Student_Major_Science", sqlConn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(studentMajorsScience);

            cmd = new SqlCommand("SELECT * FROM Student_Major_Engineering", sqlConn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(studentMajorsEngineering);
        }

        // Fetch data from MySQL (Site 2)
        using (MySqlConnection mySqlConn = new MySqlConnection(mySqlConnectionString))
        {
            mySqlConn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Student_General_Info", mySqlConn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(studentGeneralInfo);
        }

        // Fetch data from PostgreSQL (Site 3)
        using (NpgsqlConnection npgSqlConn = new NpgsqlConnection(postgreSqlConnectionString))
        {
            npgSqlConn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Student_Major_Arts", npgSqlConn);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(studentMajorsArts);

            cmd = new NpgsqlCommand("SELECT * FROM Student_Major_Humanities", npgSqlConn);
            adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(studentMajorsHumanities);
        }

        // Combine all fragments into a single DataTable
        DataTable completeStudents = studentGeneralInfo.Clone();
        completeStudents.Columns.Add("DateOfBirth", typeof(DateTime));
        completeStudents.Columns.Add("Gender", typeof(string));
        completeStudents.Columns.Add("Major", typeof(string));

        MergeDataTables(completeStudents, studentGeneralInfo, studentMajorsScience);
        MergeDataTables(completeStudents, studentGeneralInfo, studentMajorsEngineering);
        MergeDataTables(completeStudents, studentGeneralInfo, studentMajorsArts);
        MergeDataTables(completeStudents, studentGeneralInfo, studentMajorsHumanities);

        // Display the reconstructed table
        Console.WriteLine("Reconstructed Students Table:");
        foreach (DataRow row in completeStudents.Rows)
        {
            Console.WriteLine($"{row["StudentID"]}, {row["FirstName"]}, {row["LastName"]}, {row["DateOfBirth"]}, {row["Gender"]}, {row["Major"]}");
        }
    }

    static void MergeDataTables(DataTable target, DataTable generalInfo, DataTable fragment)
    {
        foreach (DataRow row in fragment.Rows)
        {
            DataRow[] generalInfoRows = generalInfo.Select($"StudentID = {row["StudentID"]}");
            if (generalInfoRows.Length > 0)
            {
                DataRow newRow = target.NewRow();
                newRow["StudentID"] = row["StudentID"];
                newRow["FirstName"] = generalInfoRows[0]["FirstName"];
                newRow["LastName"] = generalInfoRows[0]["LastName"];
                newRow["DateOfBirth"] = row["DateOfBirth"];
                newRow["Gender"] = row["Gender"];
                newRow["Major"] = row["Major"];
                target.Rows.Add(newRow);
            }
        }
    }
}

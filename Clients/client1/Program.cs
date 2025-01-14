// mysql client site
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;

{
    try
    {
        IDbConnection dbConnection = new MySqlConnection("Server=localhost,3306;Database=test_database;User=root;Password=123456789");

        string? readLine;
        dbConnection.Open();

        while (true)
        {
            Console.Clear();
            Console.Write("SQL statement - ");
            readLine = Console.ReadLine();

            if (!string.IsNullOrEmpty(readLine))
            {
                if (readLine.Trim().ToLower() == "exit")
                    break;

                try
                {
                    var result = dbConnection.Query(readLine);

                    foreach (var row in result)
                    { Console.WriteLine(row); }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Press Enter to continue... ");
            Console.ReadLine();
        }

        dbConnection.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
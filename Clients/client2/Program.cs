// postgres client site
using Dapper;
using System.Data;
using Npgsql;

{
    try
    {
        IDbConnection dbConnection = new NpgsqlConnection("Host=localhost,5432;Database=postgres;Username=postgres;Password=123456789");

        dbConnection.Open();
        string? readLine;

        while (true)
        {
            Console.Clear();
            Console.Write("SQL statement - ");
            readLine = Console.ReadLine();

            if (!string.IsNullOrEmpty(readLine))
            {
                if (readLine.Trim().ToLower() == "exit")
                    break;

                var result = dbConnection.Query(readLine);
                

                foreach (var row in result)
                { Console.WriteLine(row); }
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
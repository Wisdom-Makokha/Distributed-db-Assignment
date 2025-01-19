using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

{
    try
    {
        IDbConnection dbConnection = new SqlConnection("Server=localhost,1433;User Id=sa;Database=university_db;Password=TH1515TH3pa55w0rdf0rM55QL;TrustServerCertificate=true;");

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
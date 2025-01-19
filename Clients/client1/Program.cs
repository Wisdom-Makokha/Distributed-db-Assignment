// mysql client site
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Data.SqlClient;
using Npgsql;

{
    try
    {
        string mySQLConnectionString = "Server=localhost,3306;Database=university_db;User=root;Password=123456789";
        string sqlServerConnectionString = "Server=localhost,1433;User Id=sa;Database=university_db;Password=TH1515TH3pa55w0rdf0rM55QL;TrustServerCertificate=true;";
        string postGresConnectionString = "Host=localhost,5432;Database=postgres;Username=postgres;Password=123456789";

        // DataTables to hold student fragments
        DataTable studentGeneralInfo = new DataTable();
        DataTable studentMajorScience = new DataTable();
        DataTable studentComputerMath = new DataTable();

        // Datatables to hold course fragments
        DataTable courseCredits = new DataTable();
        DataTable coursePhysics = new DataTable();
        DataTable courseBiology = new DataTable();
        DataTable courseChemistry = new DataTable();
        DataTable courseComputerScience = new DataTable();
        DataTable courseMathematics = new DataTable();

        // Datatables to hold professor fragments
        DataTable professorBasicDetails = new DataTable();
        DataTable professorDepartment = new DataTable();

        // Datatables to hold enrollment fragments
        DataTable enrollmentScience = new DataTable();
        DataTable enrollmentComputerMath = new DataTable();
        DataTable enrollmentDates = new DataTable();

        // sql server database
        using (SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString))
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM student_computer_Math", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(studentComputerMath);

            cmd = new SqlCommand("SELECT * FROM enrollments_computer_math", sqlConnection);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(enrollmentComputerMath);


            cmd = new SqlCommand("SELECT * FROM enrollments_science", sqlConnection);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(enrollmentScience);
        }

        // mysql database
        using (MySqlConnection sqlConnection = new MySqlConnection(mySQLConnectionString))
        {
            sqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Student_Major_Science", sqlConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(studentMajorScience);

            cmd = new MySqlCommand("SELECT * FROM Courses_Credits", sqlConnection);
            adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(courseCredits);

            cmd = new MySqlCommand("SELECT * FROM Enrollments_Dates", sqlConnection);
            adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(enrollmentDates);

            cmd = new MySqlCommand("SELECT * FROM Professors_Basic_Details", sqlConnection);
            adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(professorBasicDetails);
        }

        // postgres database
        using (NpgsqlConnection sqlConnection = new NpgsqlConnection(postGresConnectionString))
        {
            sqlConnection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM student_general_info", sqlConnection);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(studentGeneralInfo);

            cmd = new NpgsqlCommand("SELECT * FROM courses_biology", sqlConnection);
            adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(courseBiology);

            cmd = new NpgsqlCommand("SELECT * FROM courses_physics", sqlConnection);
            adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(coursePhysics);

            cmd = new NpgsqlCommand("SELECT * FROM courses_chemistry", sqlConnection);
            adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(courseChemistry);

            cmd = new NpgsqlCommand("SELECT * FROM courses_computer_science", sqlConnection);
            adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(courseComputerScience);

            cmd = new NpgsqlCommand("SELECT * FROM courses_mathematics", sqlConnection);
            adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(courseMathematics);

            cmd = new NpgsqlCommand("SELECT * FROM professors_department", sqlConnection);
            adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(professorDepartment);
        }

        //Combine all fragments into a single table
        DataTable completeStudents = studentGeneralInfo.Clone();
        completeStudents.Columns.Add("DateOfBirth", typeof(DateTime));
        completeStudents.Columns.Add("Gender", typeof(string));
        completeStudents.Columns.Add("Major", typeof(string));

        MergeStudentDataTables(completeStudents, studentGeneralInfo, studentMajorScience);
        MergeStudentDataTables(completeStudents, studentGeneralInfo, studentComputerMath);

        //Display the reconstructed table
        Console.WriteLine("Reconstructed students table");
        foreach (DataRow row in completeStudents.Rows)
        {
            Console.WriteLine($"{row["StudentID"],-9} {row["FirstName"],-12} {row["LastName"],-12} {row["DateOfBirth"],-15} {row["Gender"],-2} {row["Major"]}");
        }

        DataTable completeCourses = courseCredits.Clone();
        completeCourses.Columns.Add("CourseName", typeof(string));
        completeCourses.Columns.Add("Department", typeof(string));

        MergeCoursesDataTables(completeCourses, courseCredits, courseComputerScience);
        MergeCoursesDataTables(completeCourses, courseCredits, courseBiology);
        MergeCoursesDataTables(completeCourses, courseCredits, coursePhysics);
        MergeCoursesDataTables(completeCourses, courseCredits, courseChemistry);
        MergeCoursesDataTables(completeCourses, courseCredits, courseMathematics);

        Console.WriteLine("\n\nReconstructed Courses table");
        foreach (DataRow row in completeCourses.Rows)
        {
            Console.WriteLine($"{row["CourseID"],-9} {row["CourseName"],-34} {row["Department"],-16} {row["Credits"]}");
        }


        DataTable completeProfessors = professorBasicDetails.Clone();
        completeProfessors.Columns.Add("Department", typeof(string));

        MergeProfessorDataTables(completeProfessors, professorBasicDetails, professorDepartment);

        Console.WriteLine("\n\nReconstructed Professors table");
        foreach (DataRow row in completeProfessors.Rows)
        {
            Console.WriteLine($"{row["ProfessorID"],-2} {row["FirstName"],-12} {row["LastName"],-12} {row["Department"]}");
        }

        DataTable completeEnrollments = enrollmentDates.Clone();
        completeEnrollments.Columns.Add("StudentID", typeof(int));
        completeEnrollments.Columns.Add("CourseID", typeof(int));
        completeEnrollments.Columns.Add("Grade", typeof(string));

        MergeEnrollmentDataTables(completeEnrollments, enrollmentDates, enrollmentScience);
        MergeEnrollmentDataTables(completeEnrollments, enrollmentDates, enrollmentComputerMath);

        Console.WriteLine("\n\nReconstructed Enrollments table");
        foreach (DataRow row in completeEnrollments.Rows)
        {
            Console.WriteLine($"{row["EnrollmentID"], -9} {row["StudentID"], -9} {row["CourseID"], -9} {row["EnrollmentDate"], -15} {row["Grade"], 2}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void MergeStudentDataTables(DataTable target, DataTable generalInfo, DataTable fragment)
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

static void MergeCoursesDataTables(DataTable target, DataTable coursesCredit, DataTable fragment)
{
    foreach (DataRow row in fragment.Rows)
    {
        DataRow[] coursesCreditRows = coursesCredit.Select($"CourseID = {row["CourseID"]}");
        if (coursesCreditRows.Length > 0)
        {
            DataRow newRow = target.NewRow();
            newRow["CourseID"] = row["CourseID"];
            newRow["Credits"] = coursesCreditRows[0]["Credits"];
            newRow["CourseName"] = row["CourseName"];
            newRow["Department"] = row["Department"];
            target.Rows.Add(newRow);
        }
    }
}

static void MergeProfessorDataTables(DataTable target, DataTable professorDetails, DataTable fragment)
{
    foreach (DataRow row in fragment.Rows)
    {
        DataRow[] professorDetailsRows = professorDetails.Select($"ProfessorID = {row["ProfessorID"]}");
        if (professorDetailsRows.Length > 0)
        {
            DataRow newRow = target.NewRow();
            newRow["ProfessorID"] = row["ProfessorID"];
            newRow["FirstName"] = professorDetailsRows[0]["FirstName"];
            newRow["LastName"] = professorDetailsRows[0]["LastName"];
            newRow["Department"] = row["Department"];
            target.Rows.Add(newRow);
        }
    }
}

static void MergeEnrollmentDataTables(DataTable target, DataTable enrollmentDates, DataTable fragment)
{
    foreach (DataRow row in fragment.Rows)
    {
        DataRow[] enrollmentDatesRows = enrollmentDates.Select($"EnrollmentID = {row["EnrollmentID"]}");
        if (enrollmentDatesRows.Length > 0)
        {
            DataRow newRow = target.NewRow();
            newRow["EnrollmentID"] = row["EnrollmentID"];
            newRow["StudentID"] = row["StudentID"];
            newRow["CourseID"] = row["CourseID"];
            newRow["EnrollmentDate"] = enrollmentDatesRows[0]["EnrollmentDate"];
            newRow["Grade"] = row["Grade"];
            target.Rows.Add(newRow);
        }
    }
}
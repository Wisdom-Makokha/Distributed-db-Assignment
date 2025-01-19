CREATE VIEW CompleteStudents AS
SELECT s1.StudentID, s1.FirstName, s1.LastName, s2.DateOfBirth, s2.Gender, s2.Major
FROM Student_General_Info s1
JOIN (SELECT * FROM Student_Major_Science
      UNION ALL
      SELECT * FROM Student_Computer_Math) s2
ON s1.StudentID = s2.StudentID;

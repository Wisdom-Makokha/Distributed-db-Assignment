CREATE VIEW CompleteEnrollments AS
SELECT e1.EnrollmentID, e1.StudentID, e1.CourseID, e2.EnrollmentDate, e1.Grade
FROM (
    SELECT * FROM Enrollments_Science
    UNION ALL
    SELECT * FROM Enrollments_Computer_Math
) e1
JOIN Enrollments_Dates e2 ON e1.EnrollmentID = e2.EnrollmentID;

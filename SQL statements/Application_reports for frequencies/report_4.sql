SELECT s.FirstName, s.LastName, c.CourseName, e.Grade
FROM Students s
JOIN Enrollments e ON s.StudentID = e.StudentID
JOIN Courses c ON e.CourseID = c.CourseID
ORDER BY s.LastName, c.CourseName;


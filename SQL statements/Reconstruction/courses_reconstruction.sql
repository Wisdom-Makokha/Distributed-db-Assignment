CREATE VIEW CompleteCourses AS
SELECT c1.CourseID, c1.CourseName, c1.Department, c2.Credits
FROM (
    SELECT * FROM Courses_Physics
    UNION ALL
    SELECT * FROM Courses_Computer_Science
    UNION ALL
    SELECT * FROM Courses_Biology
    UNION ALL
    SELECT * FROM Courses_Chemistry
    UNION ALL
    SELECT * FROM Courses_Mathematics
) c1
JOIN Courses_Credits c2 ON c1.CourseID = c2.CourseID;

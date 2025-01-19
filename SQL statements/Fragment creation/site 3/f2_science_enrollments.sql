CREATE TABLE Enrollments_Science AS
SELECT *
FROM Enrollments
WHERE CourseID IN (
    SELECT CourseID 
    FROM Courses 
    WHERE Department IN ('Biology', 'Chemistry', 'Physics'));

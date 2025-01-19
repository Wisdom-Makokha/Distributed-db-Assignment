CREATE TABLE Enrollments_Computer_Math AS
SELECT *
FROM Enrollments
WHERE CourseID IN (
    SELECT CourseID 
    FROM Courses 
    WHERE Department IN ('Computer Science', 'Mathematics'));

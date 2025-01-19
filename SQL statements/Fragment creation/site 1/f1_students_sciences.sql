CREATE TABLE Student_Major_Science AS
SELECT *
FROM Students
WHERE Major IN ('Biology', 'Chemistry', 'Physics');

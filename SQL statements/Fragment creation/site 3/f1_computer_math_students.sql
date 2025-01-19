CREATE TABLE Student_Computer_Math AS 
SELECT *
FROM Students
Where Major IN ('Computer Science', 'Mathematics');
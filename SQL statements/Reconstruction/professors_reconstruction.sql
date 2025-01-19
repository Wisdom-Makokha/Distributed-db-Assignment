CREATE VIEW CompleteProfessors AS
SELECT p1.ProfessorID, p1.FirstName, p1.LastName, p2.Department
FROM Professors_Basic_Details p1
JOIN Professors_Department p2 ON p1.ProfessorID = p2.ProfessorID;

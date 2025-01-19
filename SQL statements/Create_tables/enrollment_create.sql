CREATE TABLE Enrollments ( 
    EnrollmentID INT PRIMARY KEY, 
    StudentID INT, CourseID INT, 
    EnrollmentDate DATE, Grade VARCHAR(2), 
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID), 
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID) );
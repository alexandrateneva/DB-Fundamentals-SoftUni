-- Problem 1 - One-To-One Relationship

CREATE DATABASE People

USE People

CREATE TABLE Persons
(
PersonID int IDENTITY NOT NULL,
FirstName nvarchar(50),
Salary decimal(7, 2),
PassportID int UNIQUE NOT NULL
)

CREATE TABLE Passports
(
PassportID int IDENTITY(101, 1) NOT NULL,
PassportNumber nvarchar(50)
)

ALTER TABLE Persons
ADD CONSTRAINT PK_Persons PRIMARY KEY (PersonID)

ALTER TABLE Passports
ADD CONSTRAINT PK_Passports PRIMARY KEY (PassportID)

ALTER TABLE Persons
ADD CONSTRAINT FK_Persons_Passports FOREIGN KEY (PassportID) REFERENCES Passports(PassportID)

INSERT INTO Passports (PassportNumber)
VALUES ('N34FG21B'),
('K65LO4R7'),
('ZE657QP2')

INSERT INTO Persons (FirstName, Salary, PassportID)
VALUES ('Roberto', 43300.00, 102),
('Tom', 56100.00, 103),
('Yana', 60200.00, 101)

-- Problem 2 - One-To-Many Relationship

CREATE DATABASE Cars

USE Cars

CREATE TABLE Models
(
ModelID int IDENTITY(101, 1) NOT NULL,
Name nvarchar(50),
ManufacturerID int NOT NULL,
)

CREATE TABLE Manufacturers
(
ManufacturerID int IDENTITY NOT NULL,
Name nvarchar(50),
EstablishedOn date
)

ALTER TABLE Models
ADD CONSTRAINT PK_Models PRIMARY KEY (ModelID)

ALTER TABLE Manufacturers
ADD CONSTRAINT PK_Manufacturers PRIMARY KEY (ManufacturerID)

ALTER TABLE Models
ADD CONSTRAINT FK_Models_Manufacturers FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)

INSERT INTO Manufacturers(Name, EstablishedOn)
VALUES ('BMW', '07/03/1916'),
('Tesla', '01/01/2003'),
('Lada', '01/05/1966')

INSERT INTO Models (Name, ManufacturerID)
VALUES ('X1', 1),
('i6', 1),
('Model S', 2),
('Model X', 2),
('Model 3', 2),
('Nova', 3)

-- Problem 3 - Many-To-Many Relationship

CREATE DATABASE Students

USE Students

CREATE TABLE Students
(
StudentID int IDENTITY NOT NULL,
Name nvarchar(50)
)

CREATE TABLE Exams
(
ExamID Int IDENTITY(101, 1) NOT NULL,
Name nvarchar(50)
)

ALTER TABLE Students
ADD CONSTRAINT PK_Students PRIMARY KEY(StudentID)

ALTER TABLE Exams
ADD CONSTRAINT PK_Exams PRIMARY KEY(ExamID)

CREATE TABLE StudentsExams
(
StudentID int,
ExamID int,
CONSTRAINT PK_StudentsExams PRIMARY KEY (StudentID, ExamID),
CONSTRAINT FK_StudentsExams_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
CONSTRAINT FK_StudentsExams_Exams FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)
)

INSERT INTO Students (Name)
VALUES ('Mila'),
('Toni'),
('Ron')

INSERT INTO Exams (Name)
VALUES ('SpringMVC'),
('Neo4j'),
('Oracle 11g')

INSERT INTO StudentsExams (StudentID, ExamID)
VALUES (1, 101),
(1, 102),
(2, 101),
(3, 103),
(2, 102),
(2, 103)

-- Problem 4 - Self-Referencing 

CREATE DATABASE Teachers

USE Teachers

CREATE TABLE Teachers
(
TeacherID int IDENTITY(101, 1) NOT NULL,
Name nvarchar(50),
ManagerID int
)

ALTER TABLE Teachers
ADD CONSTRAINT PK_Teachers PRIMARY KEY (TeacherID)

ALTER TABLE Teachers
ADD CONSTRAINT FK_Teacher_Manager FOREIGN KEY (ManagerID) REFERENCES Teachers(TeacherID)

INSERT INTO Teachers (Name, ManagerID)
VALUES ('John', NULL),
('Maya', 106),
('Silvia', 106),
('Ted', 105),
('Mark', 101),
('Greta', 101)
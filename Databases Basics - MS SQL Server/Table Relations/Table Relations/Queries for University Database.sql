-- Problem 6

CREATE DATABASE University

USE University

CREATE TABLE Majors
(
MajorID int IDENTITY NOT NULL,
Name varchar(50),
CONSTRAINT PK_Majors PRIMARY KEY (MajorID)
)

CREATE TABLE Students
(
StudentID int IDENTITY NOT NULL,
StudentNumber int,
StudentName varchar(50),
MajorID int,
CONSTRAINT PK_Students PRIMARY KEY (StudentID),
CONSTRAINT FK_Students_Majors FOREIGN KEY (MajorID) REFERENCES Majors (MajorID)
)

CREATE TABLE Payments
(
PaymentID int IDENTITY NOT NULL,
PaymentDate date,
PaymentAmount decimal(7, 2),
StudentID int,
CONSTRAINT PK_Payments PRIMARY KEY (PaymentID),
CONSTRAINT FK_Paymnets_Students FOREIGN KEY (StudentID) REFERENCES Students (StudentID)
)

CREATE TABLE Subjects
(
SubjectID int IDENTITY NOT NULL,
SubjectName varchar(50),
CONSTRAINT PK_Subjects PRIMARY KEY (SubjectID)
)

CREATE TABLE Agenda
(
StudentID int,
SubjectID int,
CONSTRAINT PK_Agenda PRIMARY KEY (StudentID, SubjectID),
CONSTRAINT FK_Agenda_Students FOREIGN KEY (StudentID) REFERENCES Students (StudentID),
CONSTRAINT FK_Agenda_Subjects FOREIGN KEY (SubjectID) REFERENCES Subjects (SubjectID)
)
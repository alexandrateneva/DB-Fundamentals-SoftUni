-- Problem 16
CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns
(
Id int IDENTITY NOT NULL,
Name nvarchar(20) NOT NULL,
CONSTRAINT PK_IdTown PRIMARY KEY (Id)
)

CREATE TABLE Adresses
(
Id int IDENTITY NOT NULL,
AddressText nvarchar(50) NOT NULL,
TownId int FOREIGN KEY REFERENCES Towns(Id)
CONSTRAINT PK_IdAdress PRIMARY KEY (Id)
)

CREATE TABLE Departments
(
Id int IDENTITY NOT NULL,
Name nvarchar(20) NOT NULL,
CONSTRAINT PK_IdDepartment PRIMARY KEY (Id)
)

CREATE TABLE Employees 
(
Id int IDENTITY NOT NULL,
FirstName nvarchar(20) NOT NULL,
MiddleName nvarchar(20),
LastName nvarchar(20) NOT NULL,
JobTitle nvarchar(20) NOT NULL,
DepartmentId int FOREIGN KEY REFERENCES Departments(Id),
HireDate date,
Salary decimal(7, 2) NOT NULL,
AddressId int FOREIGN KEY REFERENCES Adresses(Id),
CONSTRAINT PK_IdEmployee PRIMARY KEY (Id)
)

-- Problem 17
BACKUP DATABASE SoftUni 
TO DISK = 'C:\Users\home PC\Desktop\softuni-backup.bak'

RESTORE DATABASE SoftUni
FROM DISK='C:\Users\home PC\Desktop\softuni-backup.bak'

-- Problem 18
INSERT INTO Towns(Name)
VALUES ('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas')

INSERT INTO Departments(Name)
VALUES ('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

INSERT INTO Employees(FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
VALUES('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-02-03', 4000.00),
('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25),
('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00),
('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88)

-- Problem 19
SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

-- Problem 20
SELECT * FROM Towns ORDER BY Name
SELECT * FROM Departments ORDER BY Name
SELECT * FROM Employees ORDER BY Salary DESC

-- Problem 21
SELECT Name FROM Towns ORDER BY Name
SELECT Name FROM Departments ORDER BY Name
SELECT FirstName, LastName, JobTitle, Salary FROM Employees ORDER BY Salary DESC

-- Problem 22
UPDATE Employees
SET Salary = Salary * 1.10
SELECT Salary FROM Employees 
USE SoftUni

-- Problem 2

SELECT * FROM Departments

-- Problem 3

SELECT Name FROM Departments

-- Problem 4

SELECT FirstName, LastName, Salary FROM Employees

-- Problem 5

SELECT FirstName, MiddleName, LastName FROM Employees

-- Problem 6

SELECT FirstName + '.' + LastName + '@softuni.bg' FROM Employees

-- Problem 7

SELECT DISTINCT Salary FROM Employees

-- Problem 8

SELECT * FROM Employees WHERE JobTitle = 'Sales Representative'

-- Problem 9

SELECT FirstName, LastName, JobTitle 
FROM Employees 
WHERE Salary BETWEEN 20000 AND 30000

-- Problem 10

SELECT FirstName + ' ' + MiddleName + ' ' + LastName AS [Full Name] 
FROM Employees 
WHERE Salary IN (25000, 14000, 12500, 23600)

-- Problem 11

SELECT FirstName, LastName 
FROM Employees
WHERE ManagerID IS NULL

-- Problem 12

SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary > 50000
ORDER BY Salary DESC

-- Problem 13

SELECT TOP(5) FirstName, LastName
FROM Employees
ORDER BY Salary DESC

-- Problem 14

SELECT FirstName, LastName
FROM Employees
WHERE NOT DepartmentID = 4

-- Problem 15

SELECT *
FROM Employees
ORDER BY Salary DESC, FirstName, LastName DESC, MiddleName

-- Problem 16

GO
CREATE VIEW V_EmployeesSalaries AS
SELECT FirstName, LastName, Salary
FROM Employees
GO

SELECT * FROM V_EmployeesSalaries

-- Problem 17

GO
CREATE VIEW V_EmployeeNameJobTitle AS
SELECT FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name],
JobTitle
FROM Employees
GO

SELECT * FROM V_EmployeeNameJobTitle

-- Problem 18

SELECT DISTINCT JobTitle FROM Employees

-- Problem 19

SELECT TOP(10) *
FROM Projects
ORDER BY StartDate, Name

-- Problem 20

SELECT TOP(7) FirstName, LastName, HireDate
FROM Employees
ORDER BY HireDate DESC

-- Problem 21

UPDATE Employees
SET Salary = Salary * 1.12
WHERE (DepartmentID = 1 OR DepartmentID = 2 OR DepartmentID = 4 OR DepartmentID = 11)
SELECT Salary
FROM Employees
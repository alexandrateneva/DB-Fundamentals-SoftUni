USE SoftUni

-- Problem 1

SELECT FirstName, LastName 
FROM Employees
WHERE (LEFT(FirstName, 2) = 'SA')

-- Problem 2

SELECT FirstName, LastName
FROM Employees
WHERE CHARINDEX('ei', LastName, 1) > 0

-- Problem 3

SELECT FirstName
FROM Employees
WHERE DepartmentID IN (3, 10) AND HireDate BETWEEN '1995-01-01' AND '2005-12-31'

-- Problem 4

SELECT FirstName, LastName
FROM Employees
WHERE NOT JobTitle LIKE '%engineer%'

-- Problem 5

SELECT Name
FROM Towns
WHERE LEN(Name) IN (5, 6)
ORDER BY Name 

-- Problem 6

SELECT TownID, Name
FROM Towns
WHERE LEFT(Name, 1) IN ('M', 'K', 'B', 'E')
ORDER BY Name

-- Problem 7

SELECT TownID, Name
FROM Towns
WHERE NOT LEFT(Name, 1) IN ('R', 'B', 'D')
ORDER BY Name

-- Problem 8

GO
CREATE VIEW V_EmployeesHiredAfter2000  AS
SELECT FirstName, LastName
FROM Employees
WHERE DATEPART(YEAR, HireDate) > 2000
GO

SELECT * FROM V_EmployeesHiredAfter2000

-- Problem 9

SELECT FirstName, LastName
FROM Employees
WHERE LEN(LastName) = 5
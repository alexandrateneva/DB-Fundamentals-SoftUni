USE SoftUni

-- Problem 1

GO
CREATE PROC usp_GetEmployeesSalaryAbove35000 
AS 
SELECT FirstName, LastName 
FROM Employees
WHERE Salary > 35000

EXEC usp_GetEmployeesSalaryAbove35000

-- Problem 2

GO
CREATE PROC usp_GetEmployeesSalaryAboveNumber (@number decimal(18, 4))
AS 
SELECT FirstName, LastName 
FROM Employees
WHERE Salary >= @number

EXEC usp_GetEmployeesSalaryAboveNumber @number = 48100

-- Problem 3

GO
CREATE PROC usp_GetTownsStartingWith (@startString nvarchar(10))
AS
SELECT Name
FROM Towns
WHERE LEFT(Name, LEN(@startString)) = @startString

EXEC usp_GetTownsStartingWith @startString = 'b'

-- Problem 4

GO
CREATE PROC usp_GetEmployeesFromTown (@town nvarchar(20))
AS
SELECT e.FirstName, e.LastName 
FROM Employees AS e
INNER JOIN Addresses AS a ON e.AddressID = a.AddressID
INNER JOIN Towns AS t ON a.TownID = t.TownID
WHERE t.Name = @town

EXEC usp_GetEmployeesFromTown @town = 'Sofia'

-- Problem 5

GO
CREATE FUNCTION ufn_GetSalaryLevel(@salary decimal(18,4)) 
RETURNS varchar(7)
AS
BEGIN
	DECLARE @result varchar(7)
	SET @result = CASE
	 	      WHEN @salary < 30000 THEN 'Low' 
	              WHEN @salary >= 30000 AND @salary <= 50000 THEN 'Average'
	              WHEN @salary > 50000 THEN 'High' 
		      END
	RETURN @result
END
GO

SELECT Salary,
       dbo.ufn_GetSalaryLevel(Salary) AS [Salary Level] 
FROM Employees

-- Problem 6

GO
CREATE PROC usp_EmployeesBySalaryLevel (@salaryLevel varchar(7))
AS
SELECT FirstName, LastName
FROM Employees
WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel

EXEC usp_EmployeesBySalaryLevel @salaryLevel = 'High'

-- Problem 7

GO
CREATE FUNCTION ufn_IsWordComprised(@setOfLetters nvarchar(15), @word nvarchar(15)) 
RETURNS bit
AS
BEGIN
	DECLARE @counter int = 1
	DECLARE @currentLetter char(1)
		WHILE (@counter <= LEN(@word))
		BEGIN
		SET @currentLetter = SUBSTRING(@word, @counter, 1)
		SET @counter = @counter + 1
			IF (NOT CHARINDEX(@currentLetter, @setOfLetters, 1) > 0)
			BEGIN  
			  RETURN 0 
			END 
		END	
	RETURN 1
END
GO

CREATE TABLE Words 
(
SetOfLetters nvarchar(20) NOT NULL,
Word nvarchar(20) NOT NULL
)

INSERT INTO Words 
VALUES ('oistmiahf', 'Sofia'),
('oistmiahf', 'halves'),
('bobr', 'Rob'),
('pppp','Guy')

SELECT SetOfLetters, Word, dbo.ufn_IsWordComprised(SetOfLetters, Word) AS Result 
FROM Words

-- Problem 8

GO
CREATE PROC usp_DeleteEmployeesFromDepartment (@departmentId INT)
AS
BEGIN TRAN

DECLARE @delTargets TABLE
(
Id int
)

INSERT INTO @delTargets
SELECT e.EmployeeID
FROM Employees AS e
WHERE e.DepartmentID = @departmentId

ALTER TABLE Departments
ALTER COLUMN ManagerID int NULL
Format the code
DELETE FROM EmployeesProjects
WHERE EmployeeID IN (SELECT Id FROM @delTargets)

UPDATE Employees
SET ManagerID = NULL
WHERE ManagerID IN (SELECT Id FROM @delTargets)

UPDATE Departments
SET ManagerID = NULL
WHERE ManagerID IN (SELECT Id FROM @delTargets)

DELETE FROM Employees
WHERE EmployeeID IN (SELECT Id FROM @delTargets)

DELETE FROM Departments
WHERE DepartmentID = @departmentId 

SELECT COUNT(*) AS [Employees Count] 
FROM Employees AS e
INNER JOIN Departments AS d
ON d.DepartmentID = e.DepartmentID
WHERE e.DepartmentID = @departmentId

ROLLBACK

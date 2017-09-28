USE SoftUni

-- Problem 13

SELECT DepartmentID, 
       SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID
ORDER BY DepartmentID

-- Problem 14

SELECT DepartmentID, 
       MIN(Salary) AS MinimumSalary
FROM Employees
WHERE HireDate > '2000-01-01' AND DepartmentID IN (2, 5, 7)
GROUP BY DepartmentID

-- Problem 15

SELECT * 
INTO EmployeesWithAverageSalaryOver30000
FROM Employees
WHERE Salary > 30000

DELETE FROM EmployeesWithAverageSalaryOver30000
WHERE ManagerID = 42

UPDATE EmployeesWithAverageSalaryOver30000
SET Salary = Salary + 5000
WHERE DepartmentID = 1

SELECT DepartmentID,
       AVG(Salary)
FROM EmployeesWithAverageSalaryOver30000
GROUP BY DepartmentID

-- Problem 16

SELECT DepartmentID,
       MAX(Salary) AS MaxSalary
FROM Employees
GROUP BY DepartmentID
HAVING NOT MAX(Salary) BETWEEN 30000 AND 70000

-- Problem 17

SELECT COUNT(*) AS Count
FROM Employees
WHERE ManagerID IS NULL

-- Problem 18

SELECT salaries.DepartmentID,
       salaries.MaxSalary
FROM (
	SELECT DepartmentID,
		   MAX(Salary) AS MaxSalary,
		   DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS Rank
	FROM Employees
	GROUP BY DepartmentID, Salary
) AS salaries
WHERE Rank = 3

-- Problem 19

SELECT TOP(10) e.FirstName, e.LastName, e.DepartmentID
FROM Employees AS e
WHERE Salary > (
				SELECT AVG(Salary) 
				FROM Employees AS emps
				WHERE e.DepartmentID = emps.DepartmentID
				GROUP BY DepartmentID
				)
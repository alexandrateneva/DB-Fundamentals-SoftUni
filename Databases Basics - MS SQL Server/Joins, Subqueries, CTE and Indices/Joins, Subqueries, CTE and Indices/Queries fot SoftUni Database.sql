USE SoftUni

-- Problem 1

SELECT TOP(5) e.EmployeeID, e.JobTitle, e.AddressID, a.AddressText 
FROM Employees AS e
INNER JOIN Addresses as a
ON e.AddressID = a.AddressID
ORDER BY e.AddressID 

-- Problem 2

SELECT TOP(50) e.FirstName, e.LastName, t.Name, a.AddressText 
FROM Employees AS e
INNER JOIN Addresses AS a
ON e.AddressID = a.AddressID
INNER JOIN Towns AS t
ON a.TownID = t.TownID
ORDER BY FirstName, LastName

-- Problem 3

SELECT e.EmployeeID, e.FirstName, e.LastName, d.Name
FROM Employees AS e
INNER JOIN Departments AS d
ON d.DepartmentID = e.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY e.EmployeeID

-- Problem 4

SELECT TOP(5) e.EmployeeID, e.FirstName, e.Salary, d.Name
FROM Employees AS e
INNER JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE Salary > 15000
ORDER BY e.DepartmentID

-- Problem 5

SELECT TOP(3) e.EmployeeID, e.FirstName 
FROM Employees AS e
LEFT OUTER JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
WHERE ep.ProjectID IS NULL
ORDER BY EmployeeID

-- Project 6

SELECT e.FirstName, e.LastName, e.HireDate, d.Name FROM Employees AS e
INNER JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE (d.Name = 'Sales' OR d.Name = 'Finance')
	  AND e.HireDate > '1/1/1999'
ORDER BY HireDate

-- Project 7

SELECT TOP(5) e.EmployeeID, e.FirstName, p.Name 
FROM Employees AS e
INNER JOIN EmployeesProjects AS ep 
ON e.EmployeeID = ep.EmployeeID
INNER JOIN Projects AS p
ON ep.ProjectID = p.ProjectID
WHERE p.StartDate > '2002-08-13 00:00:00' 
	  AND p.EndDate IS NULL
ORDER BY e.EmployeeID

-- Project 8

SELECT e.EmployeeID,
	   e.FirstName,
	   ProjectName = CASE WHEN p.StartDate > '2005' THEN NULL ELSE p.Name END
FROM Employees AS e
INNER JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
INNER JOIN Projects AS p
ON ep.ProjectID = p.ProjectID
WHERE e.EmployeeID = 24

-- Project 9

SELECT e.EmployeeID, e.FirstName, e.ManagerID, e2.FirstName
FROM Employees AS e
INNER JOIN Employees AS e2
ON e.ManagerID = e2.EmployeeID
WHERE e.ManagerID IN (3, 7)
ORDER BY e.EmployeeID

-- Project 10

SELECT TOP(50) e.EmployeeID,
			   e.FirstName + ' ' + e.LastName AS EmployeeName,
			   emp.FirstName + ' ' + emp.LastName AS ManagerName, 
			   d.Name AS DepartmentName
FROM Employees AS e
INNER JOIN Employees AS emp
ON e.ManagerID = emp.EmployeeID
INNER JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
ORDER BY e.EmployeeID

-- Project 11

SELECT MIN(AverageSalaries.Salary) AS MinAverageSalary
FROM 
	(SELECT AVG(e.Salary) AS Salary,
		    e.DepartmentId 
	FROM Employees AS e 
	GROUP BY DepartmentID) AS AverageSalaries

USE SoftUni

-- Problem 7

GO
CREATE PROC usp_AssignProject(@emloyeeId int, @projectID int)
AS
BEGIN TRAN
    INSERT INTO EmployeesProjects
    VALUES (@emloyeeId, @projectID)
    IF((SELECT COUNT(*) 
    		FROM EmployeesProjects 
    		GROUP BY EmployeeID 
    		HAVING EmployeeID = @emloyeeId) > 3)
    BEGIN
       ROLLBACK
       RAISERROR('The employee has too many projects!', 16, 1)
       RETURN
    END
COMMIT 

-- Problem 9

GO
CREATE TABLE Deleted_Employees
(
EmployeeId int PRIMARY KEY IDENTITY NOT NULL,
FirstName varchar(20) NOT NULL,
LastName varchar(20) NOT NULL,
MiddleName varchar(20),
JobTitle varchar(20), 
DepartmentId int NOT NULL,
Salary money
) 

GO
CREATE TRIGGER tr_EmployeesDelete ON Employees FOR DELETE
AS
INSERT INTO Deleted_Employees (FirstName, LastName, MiddleName, JobTitle, DepartmentId, Salary)
SELECT d.FirstName, d.LastName, d.MiddleName, d.JobTitle, d.DepartmentId, d.Salary 
FROM deleted AS d

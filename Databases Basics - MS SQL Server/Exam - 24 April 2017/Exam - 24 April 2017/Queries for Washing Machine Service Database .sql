-- 1. Database design

CREATE DATABASE WMS

USE WMS

CREATE TABLE Clients
(
ClientId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
FirstName nvarchar(50) NOT NULL,
LastName nvarchar(50) NOT NULL,
Phone char(12) CHECK(LEN(Phone) = 12) NOT NULL
)

CREATE TABLE Mechanics
(
MechanicId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
FirstName nvarchar(50) NOT NULL,
LastName nvarchar(50) NOT NULL,
Address nvarchar(255) NOT NULL
)

CREATE TABLE Models
(
ModelId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
Name nvarchar(50) UNIQUE NOT NULL
)

CREATE TABLE Jobs
(
JobId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
ModelId int FOREIGN KEY REFERENCES Models(ModelId) NOT NULL,
Status nvarchar(11) CHECK(Status IN ('Pending', 'In Progress', 'Finished')) DEFAULT 'Pending' NOT NULL,
ClientId int FOREIGN KEY REFERENCES Clients(ClientId) NOT NULL,
MechanicId int FOREIGN KEY REFERENCES Mechanics(MechanicId),
IssueDate date NOT NULL,
FinishDate date
)

CREATE TABLE Orders
(
OrderId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
JobId int FOREIGN KEY REFERENCES Jobs(JobId) NOT NULL,
IssueDate date,
Delivered bit DEFAULT 0 NOT NULL
)

CREATE TABLE Vendors
(
VendorId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
Name nvarchar(50) UNIQUE NOT NULL
)

CREATE TABLE Parts
(
PartId int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
SerialNumber nvarchar(50) UNIQUE NOT NULL,
Description nvarchar(255),
Price decimal(8, 2) CHECK (Price > 0) NOT NULL,
VendorId int FOREIGN KEY REFERENCES Vendors(VendorId) NOT NULL,
StockQty int CHECK(StockQty >= 0) DEFAULT 0
)

CREATE TABLE OrderParts
(
OrderId int,
PartId int,
Quantity int CHECK(Quantity > 0) DEFAULT 1,
CONSTRAINT PK_OrderParts PRIMARY KEY(OrderId, PartId),
CONSTRAINT FK_OrderParts_Orders FOREIGN KEY(OrderId) REFERENCES Orders(OrderId),
CONSTRAINT FK_OrderParts_Parts FOREIGN KEY(PartId) REFERENCES Parts(PartId)
)

CREATE TABLE PartsNeeded
(
JobId int,
PartId int,
Quantity int CHECK(Quantity > 0) DEFAULT 1,
CONSTRAINT PK_PartsNeeded PRIMARY KEY(JobId, PartId),
CONSTRAINT FK_PartsNeeded_Jobs FOREIGN KEY(JobId) REFERENCES Jobs(JobId),
CONSTRAINT FK_PartsNeeded_Parts FOREIGN KEY(PartId) REFERENCES Parts(PartId)
)

-- 2. Insert

INSERT INTO Clients (FirstName, LastName, Phone)
VALUES ('Teri', 'Ennaco', '570-889-5187'),
('Merlyn', 'Lawler', '201-588-7810'),
('Georgene', 'Montezuma', '925-615-5185'),
('Jettie', 'Mconnell', '908-802-3564'),
('Lemuel', 'Latzke', '631-748-6479'),
('Melodie', 'Knipp', '805-690-1682'),
('Candida', 'Corbley', '908-275-8357')

INSERT INTO Parts (SerialNumber, Description, Price, VendorId)
VALUES ('WP8182119', 'Door Boot Seal', 117.86, (SELECT VendorId 
                                                FROM Vendors 
                                                WHERE Name = 'Suzhou Precision Products')),
('W10780048', 'Suspension Rod', 42.81, (SELECT VendorId 
                                         FROM Vendors 
                                         WHERE Name = 'Shenzhen Ltd.')),
('W10841140', 'Silicone Adhesive', 6.77, (SELECT VendorId 
                                          FROM Vendors 
                                          WHERE Name = 'Fenghua Import Export')),
('WPY055980', 'High Temperature Adhesive', 13.94, (SELECT VendorId 
                                                   FROM Vendors 
                                                   WHERE Name = 'Qingdao Technology')

-- 3. Update

UPDATE Jobs
SET Status = 'In Progress',
MechanicId = (SELECT MechanicId FROM Mechanics WHERE FirstName = 'Ryan' AND LastName = 'Harnos')
WHERE Status = 'Pending'

-- 4. Delete

DELETE OrderParts
WHERE OrderId = 19

DELETE Orders
WHERE OrderId = 19

-- 5. Clients by Name

SELECT FirstName, LastName, Phone 
FROM Clients
ORDER BY LastName, ClientId

-- 6. Job Status

SELECT Status, IssueDate 
FROM Jobs
WHERE FinishDate IS NULL
ORDER BY IssueDate, JobId

-- 7. Mechanic Assignments

SELECT CONCAT(m.FirstName, ' ', m.LastName) AS Mechanic,
	  j.Status,
	  j.IssueDate 
FROM Mechanics AS m
LEFT OUTER JOIN Jobs AS j ON m.MechanicId = j.MechanicId
ORDER BY m.MechanicId, j.IssueDate, j.JobId

-- 8. Current Clients

SELECT CONCAT(c.FirstName, ' ', c.LastName) AS Client,
	  DATEDIFF(DAY, j.IssueDate, '2017-04-24') AS [Days going],
	  j.Status
FROM Clients AS c
LEFT OUTER JOIN Jobs AS j ON c.ClientId = j.ClientId
WHERE j.Status <> 'Finished'
ORDER BY [Days going] DESC, c.ClientId 

-- 9. Mechanic Performance

SELECT CONCAT(m.FirstName, ' ', m.LastName) AS Mechanic,
       AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)) AS [Average Days]
FROM Mechanics AS m
INNER JOIN Jobs AS j ON m.MechanicId = j.MechanicId
GROUP BY m.FirstName, m.LastName, m.MechanicId
ORDER BY m.MechanicId

-- 10. Hard Earners

SELECT TOP (3) CONCAT(m.FirstName, ' ', m.LastName) AS Mechanic,
               COUNT(CASE Status WHEN 'Finished' THEN NULL ELSE 1 END) AS Jobs
FROM Mechanics AS m
INNER JOIN Jobs AS j ON m.MechanicId = j.MechanicId
GROUP BY m.FirstName, m.LastName, m.MechanicId
HAVING COUNT(CASE Status WHEN 'Finished' THEN NULL ELSE 1 END) > 1
ORDER BY Jobs DESC, m.MechanicId

-- 11. Available Mechanics

SELECT CONCAT(m.FirstName, ' ', m.LastName) AS [Mechanic Full Name] 
FROM Mechanics AS m
LEFT OUTER JOIN Jobs AS j ON m.MechanicId = j.MechanicId
GROUP BY CONCAT(m.FirstName, ' ', m.LastName), m.MechanicId
HAVING COUNT(CASE WHEN Status = 'Finished' THEN NULL 
			   WHEN  Status IS NULL THEN NULL 
                  ELSE 1 END) = 0
ORDER BY m.MechanicId

-- 12. Parts Cost

SELECT ISNULL(SUM(p.Price * op.Quantity), 0) AS [Parts Total Cost]
FROM Orders AS o
INNER JOIN OrderParts AS op ON o.OrderId = op.OrderId
INNER JOIN Parts AS p ON op.PartId = p.PartId
WHERE o.IssueDate > DATEADD(WEEK, -3, '2017-04-24')

-- 13. Past Expenses

SELECT j.JobId,
       ISNULL(SUM(p.Price * op.Quantity), 0) AS Total
FROM Jobs AS j
LEFT OUTER JOIN Orders AS o ON j.JobId = o.JobId
LEFT OUTER JOIN OrderParts AS op ON o.OrderId = op.OrderId
LEFT OUTER JOIN Parts AS p ON op.PartId = p.PartId
GROUP BY j.JobId, j.Status 
HAVING j.Status = 'Finished'
ORDER BY Total DESC, j.JobId 

-- 14. Model Repair Time

SELECT m.ModelId,
       m.Name,
       CONCAT( AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)), ' ', 'days') AS [Average Service Time]
FROM Models AS m
INNER JOIN Jobs AS j ON m.ModelId = j.ModelId
GROUP BY m.ModelId, m.Name
ORDER BY AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate))

-- 15. Faultiest Model

SELECT TOP (1) WITH TIES m.Name,
                         COUNT(DISTINCT j.JobId) AS [Times Serviced],
                         ISNULL(SUM(p.Price * op.Quantity), 0) AS [Parts Total]
FROM Models AS m
LEFT OUTER JOIN Jobs AS j ON m.ModelId = j.ModelId
LEFT OUTER JOIN Orders AS o ON j.JobId = o.JobId
LEFT OUTER JOIN OrderParts AS op ON o.OrderId = op.OrderId
LEFT OUTER JOIN Parts AS p ON op.PartId = p.PartId
GROUP BY m.Name
ORDER BY [Times Serviced] DESC

-- 16. Missing Parts

SELECT p.PartId,
	  p.Description,
	  SUM(pn.Quantity) AS Required,
	  AVG(p.StockQty) AS [In Stock],
	  ISNULL(SUM(op.Quantity),0) AS Ordered
FROM Parts AS p
INNER JOIN PartsNeeded AS pn ON pn.PartId = p.PartId
INNER JOIN Jobs AS j ON j.JobId = pn.JobId
LEFT OUTER JOIN Orders AS o ON o.JobId = j.JobId
LEFT OUTER JOIN OrderParts AS op ON op.OrderId = o.OrderId
WHERE j.Status <> 'Finished'
GROUP BY p.PartId, p.Description
HAVING SUM(pn.Quantity) > AVG(p.StockQty) + ISNULL(SUM(op.Quantity),0)
ORDER BY p.PartId

-- 17. Cost Of Order

GO
CREATE FUNCTION udf_GetCost (@jobId int)
RETURNS decimal(7,2)
AS
BEGIN

DECLARE @result decimal(7,2)

SET @result = (SELECT ISNULL(SUM(p.Price * op.Quantity),0) 
              FROM Jobs AS j
              LEFT OUTER JOIN Orders AS o ON j.JobId = o.JobId
              LEFT OUTER JOIN OrderParts AS op ON o.OrderId = op.OrderId
              LEFT JOIN Parts AS p ON op.PartId = p.PartId
              GROUP BY j.JobId
              HAVING j.JobId = @jobId)

RETURN @result

END
GO

SELECT dbo.udf_GetCost(3)

-- 18. Place Order

GO
CREATE PROC usp_PlaceOrder @JobId int, @SerialNumber varchar(50), @Quantity int
AS
BEGIN
  IF(@Quantity <=0)
  BEGIN
  	RAISERROR('Part quantity must be more than zero!', 16, 1)
  	RETURN;
  END
  
  DECLARE @JobIdSelect int = (SELECT JobId FROM Jobs WHERE JobId = @JobId)
  IF(@JobIdSelect IS NULL)
  BEGIN
  	RAISERROR('Job not found!', 16, 1)
  END
  
  DECLARE @JobStatus varchar(50) = (SELECT Status FROM Jobs WHERE JobId = @JobId)
  IF(@JobStatus = 'Finished')
  BEGIN
  	RAISERROR('This job is not active!', 16, 1)
  END
  
  DECLARE @PartId int = (SELECT PartId FROM Parts WHERE SerialNumber = @SerialNumber)
  IF(@PartId IS NULL)
  BEGIN
  	RAISERROR('Part not found!', 16, 1)
  	RETURN;
  END
  
  DECLARE @OrderId int = (SELECT o.OrderId FROM Orders AS o
  				      JOIN OrderParts AS op ON op.OrderId = o.OrderId
  				      JOIN Parts AS p ON p.PartId = op.PartId
  				      WHERE JobId = @JobId AND p.PartId = @PartId AND IssueDate IS NULL)
  
  IF(@OrderId IS NULL)
  BEGIN
  INSERT INTO Orders(JobId, IssueDate) 
  VALUES (@JobId, NULL)
  
  INSERT INTO OrderParts(OrderId, PartId, Quantity) 
  VALUES (IDENT_CURRENT('Orders'), @PartId, @Quantity)
  END
  
  ELSE
  BEGIN
  	DECLARE @PartExistanceOrder int = (SELECT @@ROWCOUNT FROM OrderParts WHERE OrderId = @OrderId AND PartId = @PartId)
  
  	IF(@PartExistanceOrder IS NULL)
  	BEGIN
  		INSERT INTO OrderParts(OrderId, PartId, Quantity) 
  		VALUES (@OrderId, @PartId, @Quantity)
  	END
  
  	ELSE
  	BEGIN
  		UPDATE OrderParts
  		SET Quantity += @Quantity
  		WHERE OrderId = @OrderId AND PartId = @PartId
  	END
  END
END

-- 19. Detect Delivery

GO
CREATE TRIGGER  tr_PartsDelivery ON Orders FOR UPDATE
AS
DECLARE @OldStatus int = (SELECT Delivered FROM deleted)
DECLARE @NewStatus int = (SELECT Delivered FROM inserted)

IF(@OldStatus = 0 AND @NewStatus = 1)
BEGIN
  UPDATE Parts
  SET StockQty += op.Quantity
  FROM Parts AS p
  INNER JOIN OrderParts AS op ON op.PartId = p.PartId
  INNER JOIN Orders AS o ON o.OrderId = op.OrderId
  INNER JOIN inserted AS i ON i.OrderId = o.OrderId
  INNER JOIN deleted AS d ON d.OrderId = i.OrderId
END

UPDATE Orders
SET Delivered = 1
WHERE OrderId = 21

-- 20. Vendor Preference

WITH CTE_Parts
AS
(
  SELECT m.MechanicId,
  	   v.VendorId,
  	   SUM(op.Quantity) AS VendorItems
  FROM Mechanics AS m
  JOIN Jobs AS j ON j.MechanicId = m.MechanicId
  JOIN Orders AS o ON o.JobId = j.JobId
  JOIN OrderParts AS op ON op.OrderId = o.OrderId
  JOIN Parts AS p ON p.PartId = op.PartId
  JOIN Vendors AS v ON v.VendorId = P.VendorId
  GROUP BY m.MechanicId, v.VendorId
)

SELECT CONCAT(m.FirstName, ' ', m.LastName) AS Mechanic,
	   v.Name AS Vendor,
	   c.VendorItems AS Parts,
	   CAST(CAST(CAST(VendorItems AS decimal(6,2)) / (SELECT SUM(VendorItems) 
	                                                 FROM CTE_Parts 
										    WHERE MechanicId = c.MechanicId) * 100 AS int) 
	                                                 AS varchar(MAX)) + '%' AS Preference
FROM CTE_Parts AS c
JOIN Mechanics AS m ON m.MechanicId = c.MechanicId
JOIN Vendors AS v ON v.VendorId = c.VendorId
ORDER BY Mechanic, Parts DESC, Vendor
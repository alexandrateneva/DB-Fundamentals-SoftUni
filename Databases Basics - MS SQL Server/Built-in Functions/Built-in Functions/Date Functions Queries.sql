USE Orders

-- Problem 16

SELECT  ProductName, 
	OrderDate, 
	DATEADD(DAY, 3, OrderDate) AS [Pay Due],
	DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
FROM Orders

-- Problem 17

CREATE TABLE People
(
Id int PRIMARY KEY IDENTITY,
Name nvarchar(50) NOT NULL,
Birthdate date NOT NULL
)

INSERT INTO People (Name, Birthdate)
VALUES ('Victor', '2000-12-07'),
('Steven','1992-09-10'),
('Stephen','1991-09-19'),
('John','2010-01-06')

SELECT  Name,
	DATEDIFF(YEAR, Birthdate, GETDATE()) AS [Age in Years],
	DATEDIFF(MONTH, Birthdate, GETDATE()) AS [Age in Months],
	DATEDIFF(DAY, Birthdate, GETDATE()) AS [Age in Days],
	DATEDIFF(MINUTE, Birthdate, GETDATE()) AS [Age in Minutes]
FROM People

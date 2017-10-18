-- 1. Database design

CREATE DATABASE Bakery;

USE Bakery;

CREATE TABLE Products
(Id          int IDENTITY,
 Name        nvarchar(25) UNIQUE,
 Description nvarchar(250),
 Recipe      nvarchar(max),
 Price       money CHECK(Price >= 0),
 CONSTRAINT PK_Products PRIMARY KEY (Id)
);

CREATE TABLE Countries
(Id   int IDENTITY,
 Name nvarchar(50) UNIQUE,
 CONSTRAINT PK_Countries PRIMARY KEY (Id)
);

CREATE TABLE Distributors
(Id          int IDENTITY,
 Name        nvarchar(25) UNIQUE,
 AddressText nvarchar(30),
 Summary     nvarchar(200),
 CountryId   int,
 CONSTRAINT PK_Distributors PRIMARY KEY (Id),
 CONSTRAINT FK_Distributor_Countries FOREIGN KEY(CountryId) REFERENCES Countries(Id)
);

CREATE TABLE Ingredients
(Id              int IDENTITY,
 Name            nvarchar(30),
 Description     nvarchar(200),
 OriginCountryId int,
 DistributorId   int,
 CONSTRAINT PK_Ingredients PRIMARY KEY (Id),
 CONSTRAINT FK_Ingredients_Countries FOREIGN KEY(OriginCountryId) REFERENCES Countries(Id),
 CONSTRAINT FK_Ingredients_Distributors FOREIGN KEY(DistributorId) REFERENCES Distributors(Id)
);

CREATE TABLE Customers
(Id          int IDENTITY,
 FirstName   nvarchar(25),
 LastName    nvarchar(25),
 Gender      char(1) CHECK(Gender IN ('F', 'M')),
 Age         int,
 PhoneNumber char(10) CHECK(LEN(PhoneNumber) = 10),
 CountryId   int,
 CONSTRAINT PK_Customers PRIMARY KEY (Id),
 CONSTRAINT FK_Customers_Countries FOREIGN KEY(CountryId) REFERENCES Countries(Id)
);

CREATE TABLE Feedbacks
(Id          int IDENTITY,
 Description nvarchar(255),
 Rate        decimal(4, 2) CHECK(Rate BETWEEN 0 AND 10),
 ProductId   int,
 CustomerId  int,
 CONSTRAINT PK_Feedbacks PRIMARY KEY (Id),
 CONSTRAINT FK_Feedbacks_Products FOREIGN KEY(ProductId) REFERENCES Products(Id),
 CONSTRAINT FK_Feedbacks_Customers FOREIGN KEY(CustomerId) REFERENCES Customers(Id)
);
 
CREATE TABLE ProductsIngredients
(ProductId INT, 
 IngredientId INT,
 CONSTRAINT PK_ProductsIngredients PRIMARY KEY(ProductId,IngredientId),
 CONSTRAINT FK_ProductsIngredients_Products FOREIGN KEY(ProductId) REFERENCES Products(Id),
 CONSTRAINT FK_ProductsIngredients_Ingredients FOREIGN KEY(IngredientId) REFERENCES Ingredients(Id)
);

-- 2. Insert

INSERT INTO Distributors (Name, CountryId, AddressText, Summary)
VALUES ('Deloitte & Touche', 2,'6 Arch St #9757',	'Customizable neutral traveling'),
('Congress Title', 13, '58 Hancock St',	'Customer loyalty'),
('Kitchen People', 1, '3 E 31st St #77', 'Triple-buffered stable delivery'),
('General Color Co Inc',	21, '6185 Bohn St #72', 'Focus group'),
('Beck Corporation', 23,	'21 E 64th Ave', 'Quality-focused 4th generation hardware')

INSERT INTO Customers (FirstName,	LastName,	Age,	Gender,	PhoneNumber,	CountryId)
VALUES('Francoise',	'Rautenstrauch', 15, 'M', '0195698399',	5),
('Kendra', 'Loud', 22, 'F', '0063631526', 11),
('Lourdes', 'Bauswell', 50, 'M', '0139037043', 8),
('Hannah', 'Edmison', 18, 'F', '0043343686',	1),
('Tom', 'Loeza', 31, 'M', '0144876096',	23),
('Queenie', 'Kramarczyk', 30,	'F',	'0064215793', 29),
('Hiu', 'Portaro', 25, 'M', '0068277755', 16),
('Josefa', 'Opitz',	43, 'F', '0197887645', 17)

-- 3. Update

UPDATE Ingredients
SET DistributorId = 35
WHERE Name IN ('Bay Leaf', 'Paprika', 'Poppy') 

UPDATE Ingredients
SET OriginCountryId = 14
WHERE OriginCountryId = 8

-- 4. Delete

DELETE Feedbacks
WHERE CustomerId = 14 OR ProductId = 5

-- 5. Products by Price

SELECT name, Price, Description 
FROM Products 
ORDER BY Price DESC, Name

-- 6. Ingredients

SELECT Name, Description, OriginCountryId
FROM Ingredients
WHERE OriginCountryId IN (1, 10, 20)
ORDER BY Id

-- 7. Ingredients from Bulgaria and Greece

SELECT TOP (15) i.Name, i.Description, c.Name AS CountryName
FROM Ingredients AS i
INNER JOIN Countries AS c ON i.OriginCountryId = c.Id
WHERE c.Name IN ('Bulgaria', 'Greece')
ORDER BY i.Name, c.Name

-- 8. Best Rated Products

SELECT TOP (10) p.Name, p.Description, AVG(f.Rate) AS AverageRate, COUNT(f.Id) AS FeedbacksAmount
FROM Products AS p
INNER JOIN Feedbacks AS f ON p.Id = f.ProductId
GROUP BY p.Name, p.Description
ORDER BY AverageRate DESC, FeedbacksAmount DESC

-- 9. Negative Feedback

SELECT f.ProductId, f.Rate, f.Description, f.CustomerId, c.Age, c.Gender
FROM Feedbacks AS f
LEFT OUTER JOIN Customers AS c ON f.CustomerId = c.Id
WHERE f.Rate < 5.0
ORDER BY f.ProductId DESC, f.Rate

-- 10. Customers without Feedback

SELECT CONCAT(c.FirstName, ' ', c.LastName) AS CustomerName, c.PhoneNumber, c.Gender
FROM Customers AS c
LEFT OUTER JOIN Feedbacks AS f ON f.CustomerId = c.Id
WHERE f.Id IS NULL
ORDER BY c.Id ASC

-- 11. Honorable Mentions

SELECT f.ProductId,
       CONCAT(c.FirstName, ' ', c.LastName) AS CustomerName,
       f.Description AS FeedbackDescription
FROM Feedbacks AS f
INNER JOIN Customers AS c ON f.CustomerId = c.Id
WHERE c.Id IN (SELECT c.Id
	       FROM Feedbacks AS f
	       INNER JOIN Customers AS c ON f.CustomerId = c.Id
	       GROUP BY c.Id
	       HAVING COUNT(f.Id) >= 3)
ORDER BY ProductId, CustomerName, f.Id

-- 12. Customers by Criteria

SELECT cust.FirstName, cust.Age, cust.PhoneNumber
FROM Customers AS cust
INNER JOIN Countries AS c ON cust.CountryId = c.Id
WHERE (cust.Age >=21 AND cust.FirstName LIKE '%an%') 
OR (RIGHT(cust.PhoneNumber, 2) = '38' AND c.Name <> 'Greece')
ORDER BY cust.FirstName, cust.Age DESC

-- 13. Middle Range Distributors

SELECT d.Name AS DistributorName, 
       i.Name AS IngredientName,
       p.Name AS ProductName,
       AVG(f.Rate) AS AverageRate
FROM Distributors AS d
INNER JOIN Ingredients AS i ON d.Id = i.DistributorId
INNER JOIN ProductsIngredients AS pi ON i.Id = pi.IngredientId
INNER JOIN Products AS p ON pi.ProductId = p.Id
INNER JOIN Feedbacks AS f ON p.Id = f.ProductId
WHERE p.Id IN (SELECT p.Id 
	       FROM Products AS p
	       INNER JOIN Feedbacks AS f ON p.Id = f.ProductId
	       GROUP BY p.Id
	       HAVING AVG(f.Rate) BETWEEN 5 AND 8)
GROUP BY d.Name, i.Name, p.Name 
ORDER BY DistributorName, IngredientName, ProductName

-- 14. The Most Positive Country

SELECT TOP (1) WITH TIES c.Name AS CountryName, 
			 AVG(f.Rate) AS FeedbackRate
FROM Countries AS c
INNER JOIN Customers AS cust ON c.Id = cust.CountryId
INNER JOIN Feedbacks AS f ON cust.Id = f.CustomerId
GROUP BY c.Name
ORDER BY FeedbackRate DESC

-- 15. Country Representative 

SELECT CountryName,
       DisributorName
FROM
(
    SELECT c.Name AS CountryName,
           d.Name AS DisributorName,
           COUNT(i.DistributorId) AS IngredientsByDistributor,
           DENSE_RANK() OVER(PARTITION BY c.Name ORDER BY COUNT(i.DistributorId) DESC) AS Rank
    FROM Countries AS c
    LEFT OUTER JOIN Distributors AS d ON d.CountryId = c.Id
    LEFT OUTER JOIN Ingredients AS i ON i.DistributorId = d.Id
    GROUP BY c.Name,
             d.Name
) AS ranked
WHERE Rank = 1
ORDER BY CountryName,
         DisributorName

-- 16. Customers with Countries

GO
CREATE VIEW v_UserWithCountries 
AS
SELECT CONCAT(cust.FirstName, ' ', cust.LastName) AS CustomerName,
       cust.Age, 
       cust.Gender, 
       c.Name AS CountryName
FROM Customers AS cust
LEFT OUTER JOIN Countries AS c ON cust.CountryId = c.Id
GO

SELECT TOP 5 *
FROM v_UserWithCountries
ORDER BY Age

-- 17. Feedback by Product Name

GO
CREATE FUNCTION udf_GetRating (@productName varchar(50))
RETURNS varchar(10)
AS
BEGIN

DECLARE @resultInNumber decimal(4,2)
DECLARE @resultAsWord varchar(10)

DECLARE @productsRates TABLE
(
NameOfProduct varchar(50),
Rate decimal(4,2)
)

INSERT INTO @productsRates (NameOfProduct, Rate)
SELECT p.Name, AVG(f.Rate) FROM Products AS p
LEFT OUTER JOIN Feedbacks AS f ON p.Id = f.ProductId
GROUP BY p.Name

SET @resultInNumber = (SELECT TOP (1) Rate FROM @productsRates WHERE NameOfProduct = @productName)

SET @resultAsWord = CASE   
		        WHEN @resultInNumber < 5 THEN 'Bad'   
		        WHEN @resultInNumber >= 5 AND @resultInNumber <= 8 THEN 'Average' 
		        WHEN @resultInNumber > 8 THEN 'Good'
		        WHEN @resultInNumber IS NULL THEN 'No rating'
		    END

RETURN @resultAsWord
END
GO

SELECT TOP 5 Id, Name, dbo.udf_GetRating(Name)
FROM Products
ORDER BY Id

-- 18. Send Feedback

GO
CREATE PROC usp_SendFeedback (@customerId int,
			      @productId int,
			      @rate decimal(4,2),
			      @description varchar(255))
AS
BEGIN TRANSACTION

INSERT INTO Feedbacks (CustomerId, ProductId, Rate, Description)
VALUES (@customerId, @productId, @rate, @description)

IF((SELECT COUNT(*) FROM Feedbacks WHERE CustomerId = @customerId) >= 3)
BEGIN
   ROLLBACK
   RAISERROR ('You are limited to only 3 feedbacks per product!', 16, 1)
   RETURN
END

COMMIT

EXEC usp_SendFeedback 1, 5, 7.50, 'Average experience';
SELECT COUNT(*) FROM Feedbacks WHERE CustomerId = 1 AND ProductId = 5;

-- 19. Delete Products

GO
CREATE TRIGGER tr_DeleteProducts ON Products INSTEAD OF DELETE
AS
BEGIN
    DELETE FROM Feedbacks 
    WHERE ProductId = (SELECT Id FROM deleted)
    
    DELETE FROM ProductsIngredients 
    WHERE ProductId = (SELECT Id FROM deleted)
    
    DELETE FROM Products
    WHERE Id = (SELECT Id FROM deleted)
END

DELETE FROM Products WHERE Id = 7

-- 20. Products by One Distributor

SELECT p.Name AS ProductName,
       AVG(f.Rate) AS ProductAverageRate,
       d.Name AS DistributorName,
       c.Name AS DistributorCountry
FROM Products AS p
INNER JOIN Feedbacks AS f ON f.ProductId = p.Id
INNER JOIN ProductsIngredients AS pi ON pi.ProductId = p.Id
INNER JOIN Ingredients AS i ON i.Id = pi.IngredientId
INNER JOIN Distributors AS d ON d.Id = i.DistributorId
INNER JOIN Countries AS c ON c.Id = d.CountryId
GROUP BY p.Name,
         p.Id,
         d.Name,
         c.Name
HAVING p.Id IN
(
    SELECT p.Id
    FROM Products AS p
    INNER JOIN ProductsIngredients AS pi ON pi.ProductId = p.Id
    INNER JOIN Ingredients AS i ON i.Id = pi.IngredientId
    GROUP BY p.Name,
             p.Id
    HAVING COUNT(DISTINCT i.DistributorId) = 1
)
ORDER BY p.Id

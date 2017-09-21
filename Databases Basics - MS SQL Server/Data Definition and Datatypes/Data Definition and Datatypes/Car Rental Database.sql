-- Problem 14
CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories
(
Id int IDENTITY PRIMARY KEY,
CategoryName nvarchar(50) NOT NULL,
DailyRate decimal(7,2) NOT NULL,
WeeklyRate decimal(7,2) NOT NULL,
MonthlyRate decimal(7,2) NOT NULL,
WeekendRate decimal(7,2) NOT NULL
)

CREATE TABLE Cars
(
Id int IDENTITY PRIMARY KEY,
PlateNumber varchar(10) NOT NULL,
Manufacturer varchar(20) NOT NULL,
Model varchar(20) NOT NULL,
CarYear int NOT NULL,
CategoryId int,
Doors int,
Picture varbinary(max), 
Condition nvarchar(max),
Available bit NOT NULL
)

CREATE TABLE Employees
(
Id int IDENTITY PRIMARY KEY,
FirstName nvarchar(20) NOT NULL,
LastName nvarchar(20) NOT NULL,
Title nvarchar(50),
Notes nvarchar(max)
)

CREATE TABLE Customers
(
Id int IDENTITY PRIMARY KEY,
DriverLicenceNumber int NOT NULL,
FullName nvarchar(50) NOT NULL,
Address nvarchar(50),
City nvarchar(20),
ZIPCode nvarchar(20),
Notes nvarchar(max)
)

CREATE TABLE RentalOrders
(
Id int IDENTITY PRIMARY KEY,
EmployeeId int FOREIGN KEY REFERENCES Employees(Id),
CustomerId int FOREIGN KEY REFERENCES Customers(Id),
CarId int FOREIGN KEY REFERENCES Cars(Id),
TankLevel decimal (7,2) NOT NULL,
KilometrageStart decimal (15,2) NOT NULL,
KilometrageEnd decimal (15,2) NOT NULL,
TotalKilometrage AS KilometrageEnd - KilometrageStart,
StartDate date,
EndDate date,
TotalDays AS datediff(day, StartDate, EndDate),
RateApplied decimal (7,2),
TaxRate decimal (7,2),
OrderStatus nvarchar(20),
Notes nvarchar(max)
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES ('Family Cars', 25.50, 140.20, 500.90, 50.60),
('Luxury Cars', 64.50, 430.50, 1520.40, 140.15),
('Sports Cars', 50.50, 350.20, 1050.60, 100.15)

INSERT INTO Cars(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES
('CB5673PA', 'Opel', 'Astra', 2006, 1, 4, 123, 'Very safety!', 0),
('CB7091CT', 'Maserati', 'Quattroporte', 2016, 2, 4, 456, 'Very expensive and luxury car!', 0),
('CB0079RH', 'Bugatti', 'Chiron', 2017, 3, 2, 789, 'Very fast!', 1)

INSERT INTO Employees (FirstName, LastName, Title, Notes)
VALUES ('Georgi', 'Petrov', 'Manager', 'Very strict!'),
('Ivan', 'Angelov', 'Salesman', 'Very polite!'),
('Maria', 'Ivanova', 'Salesman', 'Very kind!')

INSERT INTO Customers(DriverLicenceNumber, FullName, Address, City, ZIPCode)
VALUES ('37507178', 'Kristian Ivanov','Boyana, str.Boyanska', 'Sofia', '1518'),
('90158506', 'Ivan Penev','Beli brezi, str.Vorino', 'Sofia', '1680'),
('68017435', 'Iva Stoyanova','Mladost, str.Alexander Malinov', 'Sofia', '7856')

INSERT INTO RentalOrders(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, StartDate, EndDate)
VALUES (1, 1, 1, 34.8, 115067.49, 11879.80, '2017-07-20', '2017-07-27'),
(2, 2, 2, 40.9, 11001.60, 11300.50, '2017-09-03', '2017-09-23'),
(3, 3, 3, 45.6, 5070.87, 5600.78, '2017-03-08', '2016-03-15')
-- Problem 15
CREATE DATABASE Hotel

USE Hotel

CREATE TABLE Employees 
(
Id int PRIMARY KEY IDENTITY,
FirstName nvarchar(20) NOT NULL,
LastName nvarchar(20) NOT NULL,
Title nvarchar(20) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Customers 
(
AccountNumber int PRIMARY KEY IDENTITY,
FirstName nvarchar(20) NOT NULL,
LastName nvarchar(20) NOT NULL,
PhoneNumber varchar(20) NOT NULL,
EmergencyName nvarchar(20),
EmergencyNumber varchar(15) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE RoomStatus
(
RoomStatus varchar(20) PRIMARY KEY NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE RoomTypes
(
RoomType varchar(20) PRIMARY KEY NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE BedTypes
(
BedType varchar(20) PRIMARY KEY NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Rooms
(
RoomNumber int PRIMARY KEY IDENTITY,
RoomType varchar(20) FOREIGN KEY REFERENCES RoomTypes(RoomType) NOT NULL,
BedType varchar(20) FOREIGN KEY REFERENCES BedTypes(BedType) NOT NULL,
Rate decimal(7,2) NOT NULL,
RoomStatus varchar(20) FOREIGN KEY REFERENCES RoomStatus(RoomStatus) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Payments
(
Id int PRIMARY KEY IDENTITY,
EmployeeId int FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
PaymentDate date NOT NULL,
AccountNumber int FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
FirstDateOccupied date,
LastDateOccupied date,
TotalDays AS datediff(day, FirstDateOccupied, LastDateOccupied),
AmmountCharged decimal (7,2) NOT NULL,
TaxRate decimal (7,2),
TaxAmount decimal (7,2),
PaymentTotal decimal (7,2),
Notes nvarchar(max)
)

CREATE TABLE Occupancies 
(
Id int PRIMARY KEY IDENTITY,
EmployeeId int FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
DateOccupied date,
AccountNumber int FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
RoomNumber int FOREIGN KEY REFERENCES Rooms(RoomNumber),
RateApplied decimal (7,2) NOT NULL,
PhoneCharge decimal (7,2),
Notes nvarchar(max)
)

INSERT INTO Employees(FirstName, LastName, Title)
VALUES ('Georgi', 'Petrov', 'Manager'),
('Maria', 'Ivanova', 'Housemaid'),
('Ivan', 'Dimitrov', 'Receptionist')

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyNumber)
VALUES ('Petyr', 'Petrov', '0889657905', '0889123905'),
('Vladimir', 'Atanasov', '0883567908', '0888214975'),
('Angel', 'Ivanov', '0889678098', '0889453128')

INSERT INTO RoomStatus (RoomStatus)
VALUES ('Free'),
('Occupied'),
('Unavailable')

INSERT INTO RoomTypes (RoomType)
VALUES ('Regular'),
('With View'),
('Luxury')

INSERT INTO BedTypes (BedType)
VALUES ('Single'),
('Double'),
('King-size')

INSERT INTO Rooms (RoomType, BedType, Rate, RoomStatus)
VALUES ('With View', 'Double', 15.10, 'Free'),
('Luxury', 'King-size', 19.50, 'Occupied'),
('Regular', 'Single', 10.60, 'Unavailable')

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, AmmountCharged)
VALUES (1, '2017-08-18', 1,'2017-08-10', '2016-08-18', 326.40),
(2, '2017-07-10', 2, '2017-07-03', '2017-07-10', 420.00),
(3, '2017-03-20', 3, '2017-03-10', '2017-03-20', 550.20)

INSERT INTO Occupancies (EmployeeId, AccountNumber, RoomNumber, RateApplied)
VALUES (1, 1, 1, 32.64),
(2, 2, 2, 42.00),
(3, 3, 3, 55.00)

-- Problem 23
UPDATE Payments
SET TaxRate -= TaxRate * 0.03
SELECT TaxRate FROM Payments 

-- Problem 24
TRUNCATE TABLE Occupancies
-- Problem 5

CREATE DATABASE OnlineStore

USE OnlineStore

CREATE TABLE Cities
(
CityID int IDENTITY NOT NULL,
Name varchar(50),
CONSTRAINT PK_Cities PRIMARY KEY (CityID)
)

CREATE TABLE Customers
(
CustomerID int IDENTITY NOT NULL,
Name varchar(50),
BirthDay date,
CityID int,
CONSTRAINT PK_Customers PRIMARY KEY (CustomerID),
CONSTRAINT FK_Customers_Cities FOREIGN KEY (CityID) REFERENCES Cities (CityID)
)

CREATE TABLE ItemTypes
(
ItemTypeID int IDENTITY NOT NULL,
Name varchar(50),
CONSTRAINT PK_ItemTypes PRIMARY KEY (ItemTypeID)
)

CREATE TABLE Items
(
ItemID int IDENTITY NOT NULL,
Name varchar(50),
ItemTypeID int,
CONSTRAINT PK_Items PRIMARY KEY (ItemID),
CONSTRAINT FK_Items_ItemTypes FOREIGN KEY (ItemTypeID) REFERENCES ItemTypes (ItemTypeID)
)

CREATE TABLE Orders
(
OrderID int IDENTITY NOT NULL,
CustomerID int,
CONSTRAINT PK_Orders PRIMARY KEY (OrderID),
CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerID) REFERENCES Customers (CustomerID)
)

CREATE TABLE OrderItems
(
OrderID int,
ItemID int,
CONSTRAINT PK_OrderItems PRIMARY KEY (OrderID, ItemID),
CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderID) REFERENCES Orders (OrderID),
CONSTRAINT FK_OrderItems_Items FOREIGN KEY (ItemID) REFERENCES Items (ItemID)
)
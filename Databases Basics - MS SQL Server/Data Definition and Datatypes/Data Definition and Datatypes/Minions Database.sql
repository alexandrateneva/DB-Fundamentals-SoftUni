-- Problem 1
CREATE DATABASE Minions

USE Minions

-- Problem 2
CREATE TABLE Minions
(
Id int PRIMARY KEY,
Name nvarchar(50) NOT NULL,
Age int
)

CREATE TABLE Towns
(
Id int PRIMARY KEY,
Name nvarchar(50) NOT NULL
)

-- Problem 3
ALTER TABLE Minions
ADD TownId int NOT NULL,
FOREIGN KEY(TownId) REFERENCES Towns(Id);

-- Problem 4
INSERT INTO Towns(Id, Name)
VALUES (1, 'Sofia');

INSERT INTO Towns(Id, Name)
VALUES (2, 'Plovdiv');

INSERT INTO Towns(Id, Name)
VALUES (3, 'Varna');

INSERT INTO Minions (Id, Name, Age, TownId)
VALUES (1, 'Kevin', 22, 1);

INSERT INTO Minions (Id, Name, Age, TownId)
VALUES (2, 'Bob', 15, 3);

INSERT INTO Minions (Id, Name, Age, TownId)
VALUES (3, 'Steward', NULL, 2);

-- Problem 5
TRUNCATE TABLE Minions

-- Problem 6
DROP TABLE Minions

DROP TABLE Towns

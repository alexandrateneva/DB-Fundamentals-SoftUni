CREATE DATABASE MinionsDB

CREATE TABLE Countries
(
CountryId int PRIMARY KEY IDENTITY,
Name nvarchar(50) NOT NULL
)

CREATE TABLE Towns
(
TownId int PRIMARY KEY IDENTITY,
Name nvarchar(50) NOT NULL,
CountryId int FOREIGN KEY REFERENCES Countries (CountryId)
)

CREATE TABLE Minions
(
MinionId int PRIMARY KEY IDENTITY,
Name nvarchar(50) NOT NULL,
Age int,
TownId int FOREIGN KEY REFERENCES Towns (TownId)
)

CREATE TABLE Villains
(
VillainId int PRIMARY KEY IDENTITY,
Name nvarchar(50) NOT NULL,
EvilnessFactor varchar(15) CHECK(EvilnessFactor IN ('good', 'bad', 'evil', 'super evil'))
)

CREATE TABLE MinionsVillains
(
MinionId int,
VillainId int,
CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId),
CONSTRAINT FK_MinionsVillains_Minions FOREIGN KEY (MinionId) REFERENCES Minions (MinionId),
CONSTRAINT FK_MinionsVillains_Villains FOREIGN KEY (VillainId) REFERENCES Villains (VillainId)
)

INSERT INTO Countries (Name) 
VALUES ('Germany'),
('France'),
('England'),
('Bulgaria'),
('USA')

INSERT INTO Towns (Name, CountryId)
VALUES ('Berlin', 1),
('Paris', 2),
('London', 3),
('Sofia', 4),
('New York', 5)

INSERT INTO Minions (Name, Age, TownId)
VALUES ('Kevin', 11, 2),
('Bob', 12, 1),
('Steward', 10, 5),
('Pesho', 14, 4),
('Ani', 13, 2)

INSERT INTO Villains (Name, EvilnessFactor)
VALUES ('Gru', 'good'),
('Vector', 'evil'),
('Shannon', 'bad'),
('Ivan', 'super evil'),
('Gosho', 'good')

INSERT INTO MinionsVillains (MinionId, VillainId)
VALUES (1, 5),
(2, 4),
(3, 3),
(4, 2),
(5, 1)
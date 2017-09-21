-- Problem 7
CREATE DATABASE People

USE People

CREATE TABLE People
(
Id int IDENTITY PRIMARY KEY,
Name nvarchar(200) NOT NULL,
Picture varbinary CHECK (DATALENGTH(Picture) <= 2097152),
Height decimal(10,2) CHECK(Height > 0),
Weight decimal(10,2) CHECK(Weight > 0),
Gender varchar(1) CHECK(Gender = 'm' OR Gender = 'f') NOT NULL,
Birthdate date NOT NULL,
Biography varchar(max) 
)

INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Ivan', 12345, 1.75, 75, 'm', '1992-10-10', 'Very cool!')

INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Gosho', 23451, 1.90, 86.5, 'm', '1993-11-14', 'Very smart!')

INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Alexandra', 45123, 1.68, 52.2, 'f', '1998-11-14', 'Very positive!')

INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Pesho', 51234, 1.86, 80.22, 'm', '1987-04-18', 'Very funny!')

INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Maria', 34512, 1.61, 55.2, 'f', '1995-02-09', 'Very beautiful!')

USE Geography

-- Problem 8

SELECT p.PeakName, m.MountainRange AS Mountain, p.Elevation
FROM Peaks AS p
INNER JOIN Mountains AS m ON p.MountainId = m.Id
ORDER BY Elevation DESC, PeakName

-- Problem 9

SELECT p.PeakName, m.MountainRange AS Mountain, c.CountryName, cont.ContinentName
FROM Peaks AS p
INNER JOIN Mountains AS m ON p.MountainId = m.Id
INNER JOIN MountainsCountries AS mc ON mc.MountainId = m.Id
INNER JOIN Countries AS c ON mc.CountryCode = c.CountryCode
INNER JOIN Continents AS cont ON cont.ContinentCode = c.ContinentCode
ORDER BY PeakName, CountryName

-- Problem 10

SELECT  CountryName,
	ContinentName, 
	ISNULL((COUNT(r.Id)), 0) AS RiversCount,
	ISNULL(SUM(r.Length), 0) AS TotalLength
FROM Countries AS c
INNER JOIN Continents AS cont ON c.ContinentCode = cont.ContinentCode
LEFT OUTER JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
LEFT OUTER JOIN Rivers AS r ON cr.RiverId = r.Id
GROUP BY CountryName, ContinentName
ORDER BY RiversCount DESC, TotalLength DESC, CountryName

-- Problem 11

SELECT  curr.CurrencyCode, 
	curr.Description AS Currency, 
	COUNT(c.CountryCode) AS NumberOfCountries
FROM Currencies AS curr
LEFT OUTER JOIN Countries AS c ON c.CurrencyCode = curr.CurrencyCode
GROUP BY curr.CurrencyCode, curr.Description
ORDER BY NumberOfCountries DESC, curr.Description 

-- Problem 12

SELECT  cont.ContinentName,
	SUM(CAST(c.AreaInSqKm AS BIGINT)) AS CountriesArea,
	SUM(CAST(c.Population AS BIGINT)) AS CountriesPopulation
FROM Continents AS cont
LEFT OUTER JOIN Countries AS c ON cont.ContinentCode = c.ContinentCode
GROUP BY cont.ContinentName
ORDER BY CountriesPopulation DESC

-- Problem 13

CREATE TABLE Monasteries
(
Id int PRIMARY KEY IDENTITY,
Name varchar(max),
CountryCode char(2) FOREIGN KEY REFERENCES Countries(CountryCode)
)

INSERT INTO Monasteries(Name, CountryCode) VALUES
('Rila Monastery “St. Ivan of Rila”', 'BG'), 
('Bachkovo Monastery “Virgin Mary”', 'BG'),
('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
('Kopan Monastery', 'NP'),
('Thrangu Tashi Yangtse Monastery', 'NP'),
('Shechen Tennyi Dargyeling Monastery', 'NP'),
('Benchen Monastery', 'NP'),
('Southern Shaolin Monastery', 'CN'),
('Dabei Monastery', 'CN'),
('Wa Sau Toi', 'CN'),
('Lhunshigyia Monastery', 'CN'),
('Rakya Monastery', 'CN'),
('Monasteries of Meteora', 'GR'),
('The Holy Monastery of Stavronikita', 'GR'),
('Taung Kalat Monastery', 'MM'),
('Pa-Auk Forest Monastery', 'MM'),
('Taktsang Palphug Monastery', 'BT'),
('Sumela Monastery', 'TR')

ALTER TABLE Countries 
ADD IsDeleted bit DEFAULT 0 NOT NULL

UPDATE Countries
SET IsDeleted = 1
WHERE CountryCode IN (SELECT c.CountryCode 
		     FROM Countries AS c
		     INNER JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
		     GROUP BY c.CountryCode
		     HAVING COUNT(cr.RiverId) > 3)

SELECT m.Name AS Monastery, c.CountryName AS Country
FROM Monasteries AS m
LEFT OUTER JOIN Countries AS c ON m.CountryCode = c.CountryCode
WHERE c.IsDeleted = 0
ORDER BY Monastery

-- Problem 14

INSERT INTO Monasteries
VALUES  ('Hanga Abbey', (SELECT CountryCode 
			 FROM Countries 
			 WHERE CountryName = 'Tanzania')),
	('Myin-Tin-Daik',(SELECT CountryCode 
			  FROM Countries 
			  WHERE CountryName = 'Myanmar'))

UPDATE Countries
SET CountryName = 'Burma'
WHERE CountryCode = (SELECT CountryCode 
		     FROM Countries 
		     WHERE CountryName = 'Myanmar')

SELECT  cont.ContinentName, 
	c.CountryName, 
	COUNT(m.Name) AS MonasteriesCount
FROM Continents AS cont
INNER JOIN Countries AS c ON cont.ContinentCode = c.ContinentCode
LEFT OUTER JOIN Monasteries AS m ON c.CountryCode = m.CountryCode
WHERE c.IsDeleted = 0
GROUP BY cont.ContinentName, c.CountryName
ORDER BY MonasteriesCount DESC, c.CountryName

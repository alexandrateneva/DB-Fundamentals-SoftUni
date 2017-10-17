USE Geography

-- Problem 12

SELECT mc.CountryCode,
       m.MountainRange,
       p.PeakName,
       p.Elevation
FROM MountainsCountries AS mc
INNER JOIN Mountains AS m
ON mc.MountainId = m.Id
INNER JOIN Peaks AS p
ON m.Id = p.MountainId
WHERE mc.CountryCode = 'BG' AND p.Elevation > 2835
ORDER BY p.Elevation DESC

-- Problem 13

SELECT mc.CountryCode,
       COUNT(*)
FROM MountainsCountries AS mc
INNER JOIN Mountains AS m
ON mc.MountainId = m.Id
WHERE mc.CountryCode IN ('BG', 'RU', 'US')
GROUP BY mc.CountryCode

-- Problem 14

SELECT TOP(5) c.CountryName, 
	      r.RiverName
FROM Countries AS c
LEFT OUTER JOIN CountriesRivers AS cr
ON c.CountryCode = cr.CountryCode
LEFT OUTER JOIN Rivers AS r
ON cr.RiverId = r.Id
WHERE c.ContinentCode = 
	   (SELECT cont.ContinentCode 
	    FROM Continents AS cont 
	    WHERE cont.ContinentName = 'Africa')
ORDER BY c.CountryName

-- Problem 15 - 1

SELECT usages.ContinentCode, usages.CurrencyCode, usages.CurrancyUsage
FROM (
      SELECT ContinentCode, CurrencyCode, COUNT(*) AS CurrancyUsage
      FROM Countries AS c
      GROUP BY ContinentCode, CurrencyCode
      HAVING COUNT(*) > 1
) AS usages
INNER JOIN (
SELECT usages.ContinentCode, MAX(usages.Usage) AS MaxUsage 
FROM (
      SELECT ContinentCode, CurrencyCode, COUNT(*) AS Usage 
      FROM Countries AS c
      GROUP BY ContinentCode, CurrencyCode
      ) AS usages
      GROUP BY usages.ContinentCode
 )AS maxUsages
ON usages.ContinentCode = maxUsages.ContinentCode AND maxUsages.MaxUsage = usages.CurrancyUsage

-- Problem 15 - 2

SELECT usage.ContinentCode,
       usage.CurrencyCode AS CurrencyCode,
       usage.CurrencyUsage AS CurrencyUsage
FROM (SELECT c.ContinentCode,
             cr.CurrencyCode,
             COUNT(cr.Description) AS CurrencyUsage,
             DENSE_RANK() OVER (PARTITION BY c.ContinentCode ORDER BY COUNT(cr.CurrencyCode) DESC) AS Rank
       FROM Currencies AS cr
       INNER JOIN Countries c ON cr.CurrencyCode = c.CurrencyCode
       GROUP BY c.ContinentCode, cr.CurrencyCode
       HAVING  COUNT(cr.Description) > 1) AS usage
WHERE usage.Rank = 1 
ORDER BY usage.ContinentCode

-- Problem 16

SELECT COUNT(c.CountryCode) AS CountryCode
FROM Countries AS c
LEFT OUTER JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
WHERE mc.CountryCode IS NULL

-- Problem 17

SELECT TOP(5) c.CountryName,
	      MAX(p.Elevation) AS HighestPeakElevation,	   
	      MAX(r.Length) AS LongestRiverLength
FROM Countries AS c
LEFT OUTER JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
LEFT OUTER JOIN Mountains AS m
ON mc.MountainId = m.Id
LEFT OUTER JOIN Peaks AS p
ON m.Id = p.MountainId
LEFT OUTER JOIN CountriesRivers AS cr
ON c.CountryCode = cr.CountryCode
LEFT OUTER JOIN Rivers AS r
ON cr.RiverId = r.Id
GROUP BY c.CountryName
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, c.CountryName 

-- Problem 18

SELECT TOP (5) WITH TIES c.CountryName, 
			 ISNULL(p.PeakName, '(no highest peak)') AS HighestPeakName,
			 ISNULL(MAX(p.Elevation), 0) AS HighestPeakElevation,
			 ISNULL(m.MountainRange, '(no mountain)') AS Mountain
FROM Countries AS c
LEFT OUTER JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
LEFT OUTER JOIN Mountains AS m
ON mc.MountainId = m.Id
LEFT OUTER JOIN Peaks AS p
ON m.Id = p.MountainId
GROUP BY c.CountryName, p.PeakName, m.MountainRange
ORDER BY c.CountryName, p.PeakName

USE Diablo

-- Problem 1

SELECT  SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email)) AS [Email Provider],
	COUNT(*) AS [Number Of Users]
FROM Users
GROUP BY SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email))
ORDER BY [Number Of Users] DESC, [Email Provider] ASC

-- Problem 2

SELECT  g.Name AS [Game],
	gt.Name AS [Game Type],
	u.Username,
	ug.Level,
	ug.Cash,
	ch.Name AS [Character]
FROM Users AS u
INNER JOIN UsersGames AS ug ON u.Id = ug.UserId
INNER JOIN Games AS g ON ug.GameId = g.Id
INNER JOIN GameTypes AS gt ON g.GameTypeId = gt.Id
INNER JOIN Characters AS ch ON ug.CharacterId = ch.Id
ORDER BY ug.Level DESC, U.Username, g.Name

-- Problem 3

SELECT  u.Username,
	g.Name AS [Game],
	COUNT(ugi.ItemId) AS [Items Count],
	SUM(i.Price) AS [Items Price]
FROM Users AS u
INNER JOIN UsersGames AS ug ON u.Id = ug.UserId
INNER JOIN UserGameItems AS ugi ON ug.Id = ugi.UserGameId
INNER JOIN Items AS i ON ugi.ItemId = i.Id
INNER JOIN Games AS g ON ug.GameId = g.Id
GROUP BY u.Username, g.Name
HAVING COUNT(ugi.ItemId) >= 10
ORDER BY [Items Count] DESC, [Items Price] DESC, u.Username

-- Problem 4

SELECT  u.Username,
	g.Name AS [Game],
	MAX(ch.Name) AS [Character],
	(SUM(s2.Strength) + MAX(s1.Strength) + MAX(s.Strength)) AS [Strength],
	(SUM(s2.Defence) + MAX(s1.Defence) + MAX(s.Defence)) AS [Defence],
	(SUM(s2.Speed) + MAX(s1.Speed) + MAX(s.Speed)) AS [Speed],
	(SUM(s2.Mind) + MAX(s1.Mind) + MAX(s.Mind)) AS [Mind],
	(SUM(s2.Luck) + MAX(s1.Luck) + MAX(s.Luck)) AS [Luck]
FROM UseAS u
INNER JOIN UsersGames AS ug ON u.Id = ug.UserId
INNER JOIN Games AS g ON ug.GameId = g.Id
INNER JOIN GameTypes AS gt ON g.GameTypeId = gt.Id
INNER JOIN "Statistics" AS s ON gt.BonusStatsId = s.Id
INNER JOIN Characters AS ch ON ug.CharacterId = ch.Id
INNER JOIN "Statistics" AS s1 ON s1.Id = ch.StatisticId
INNER JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
INNER JOIN Items AS i ON ugi.ItemId = i.Id
INNER JOIN "Statistics" AS s2 ON i.StatisticId = s2.Id 
GROUP BY u.Username, g.Name
ORDER BY [Strength] DESC, [Defence] DESC, [Speed] DESC, [Mind] DESC, [Luck] DESC

-- Problem 5

DECLARE @avgMind decimal = (SELECT AVG(Mind) FROM "Statistics")
DECLARE @avgLuck decimal = (SELECT AVG(Luck) FROM "Statistics")
DECLARE @avgSpeed decimal = (SELECT AVG(Speed) FROM "Statistics")

SELECT  i.Name, i.Price, i.MinLevel, s.Strength, s.Defence, s.Speed, s.Luck, s.Mind
FROM Items AS i
INNER JOIN "Statistics" AS s ON i.StatisticId = s.Id
WHERE s.Mind > @avgMind AND	s.Luck > @avgLuck AND s.Speed > @avgSpeed
ORDER BY i.Name

-- Problem 6

SELECT i.Name AS [Item], i.Price, i.MinLevel, gt.Name AS [Forbidden Game Type]
FROM Items AS i
LEFT OUTER JOIN GameTypeForbiddenItems AS gfi ON i.Id = gfi.ItemId
LEFT OUTER JOIN GameTypes AS gt ON gfi.GameTypeId = gt.Id
ORDER BY [Forbidden Game Type] DESC, [Item]

-- Problem 7

BEGIN TRAN

DECLARE @userID int = (SELECT Id FROM Users WHERE Username = 'Alex')
DECLARE @gameID int = (SELECT Id FROM Games WHERE Name = 'Edinburgh')
DECLARE @usersGameID int = (SELECT Id FROM UsersGames WHERE UserId = @userID AND GameId = @gameID)
DECLARE @neededMoney money = (SELECT SUM(Price) 
			      FROM Items AS i
			      WHERE i.Name IN ('Blackguard', 'Bottomless Potion of Amplification',
			      		       'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin',
			      		       'Golden Gorget of Leoric', 'Hellfire Amulet'))

INSERT INTO UserGameItems 
SELECT i.Id, @usersGameID
FROM Items AS i
WHERE i.Name IN ('Blackguard', 'Bottomless Potion of Amplification',
	   	 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin',
	   	 'Golden Gorget of Leoric', 'Hellfire Amulet')

UPDATE UsersGames
SET Cash = Cash - @neededMoney
WHERE Id = @usersGameID

IF(@neededMoney > (SELECT Cash FROM UsersGames WHERE Id = @usersGameID))
BEGIN
   RAISERROR('There is not enough cash to buy these items!', 16, 1)
   ROLLBACK
   RETURN
END

SELECT u.Username, g.Name, ug.Cash, i.Name AS [Item Name]
FROM Users AS u
INNER JOIN UsersGames AS ug ON u.Id = ug.UserId
INNER JOIN Games AS g ON ug.GameId = g.Id
INNER JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
INNER JOIN Items AS i ON ugi.ItemId = i.Id
WHERE g.Id = @gameID
ORDER BY [Item Name]

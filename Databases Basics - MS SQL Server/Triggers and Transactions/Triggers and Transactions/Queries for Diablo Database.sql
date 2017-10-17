USE Diablo

-- Problem 6

DECLARE @userID int = (SELECT Id FROM Users WHERE Username = 'Stamat')
DECLARE @gameID int = (SELECT Id FROM Games WHERE Name = 'Safflower')
DECLARE @userMoney money = (SELECT Cash FROM UsersGames WHERE UserId = @userID AND GameId = @gameID)
DECLARE @itemsTotalPrice money
DECLARE @userGameID int = (SELECT Id FROM UsersGames WHERE UserId = @userID AND GameId = @gameID)

BEGIN TRANSACTION
	SET @itemsTotalPrice = (SELECT SUM(Price) FROM Items WHERE MinLevel IN (11, 12))

	IF(@userMoney - @itemsTotalPrice > 0)
	BEGIN
		INSERT INTO UserGameItems
		SELECT i.Id, @userGameID FROM Items AS i
		WHERE i.Id IN (SELECT Id FROM Items WHERE MinLevel IN (11, 12))

		UPDATE UsersGames
		SET Cash -= @itemsTotalPrice
		WHERE GameId = @gameID AND UserId = @userID
		COMMIT
	END
	ELSE
	BEGIN
		ROLLBACK
	END

SET @userMoney = (SELECT Cash FROM UsersGames WHERE UserId = @userID AND GameId = @gameID)
BEGIN TRANSACTION
	SET @itemsTotalPrice = (SELECT SUM(Price) FROM Items WHERE MinLevel IN (19, 20, 21))

	IF(@userMoney - @itemsTotalPrice > 0)
	BEGIN
		INSERT INTO UserGameItems
		SELECT i.Id, @userGameID FROM Items AS i
		WHERE i.Id IN (SELECT Id FROM Items WHERE MinLevel IN (19, 20, 21))

		UPDATE UsersGames
		SET Cash -= @itemsTotalPrice
		WHERE GameId = @gameID AND UserId = @userID
		COMMIT
	END
	ELSE
	BEGIN
		ROLLBACK
	END

SELECT Name AS [Item Name]
FROM Items
WHERE Id IN (SELECT ItemId FROM UserGameItems WHERE UserGameId = @userGameID)
ORDER BY [Item Name]

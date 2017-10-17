USE Diablo

-- Problem 13

GO
CREATE FUNCTION ufn_CashInUsersGames (@gameName varchar(max))
RETURNS TABLE
AS
RETURN SELECT SUM(Cash) AS SumCash
		FROM (
			SELECT ug.Cash, ROW_NUMBER() OVER (ORDER BY Cash DESC) AS RowNumber
			FROM UsersGames AS ug
			INNER JOIN Games AS g ON g.Id = ug.GameId
			WHERE g.Name = @gameName
			) AS CashList
		WHERE RowNumber % 2 = 1
GO

SELECT * FROM ufn_CashInUsersGames('Lily Stargazer')
SELECT * FROM ufn_CashInUsersGames('Love in a mist')
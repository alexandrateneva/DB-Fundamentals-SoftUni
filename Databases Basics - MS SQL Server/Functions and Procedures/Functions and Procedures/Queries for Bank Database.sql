USE Bank

-- Problem 9

GO
CREATE PROC usp_GetHoldersFullName 
AS
SELECT FirstName + ' ' + LastName AS [Full Name]
FROM AccountHolders

EXEC usp_GetHoldersFullName

-- Problem 10 - 1

GO
CREATE PROC usp_GetHoldersWithBalanceHigherThan (@number decimal(18, 4))
AS
SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name]
FROM AccountHolders AS ah
INNER JOIN  Accounts AS a 
ON ah.Id = a.AccountHolderId
GROUP BY ah.FirstName, ah.LastName
HAVING SUM(a.Balance) > @number

-- Problem 10 - 2

GO
CREATE PROC usp_GetHoldersWithBalanceHigherThan_2 (@number decimal(18, 4))
AS
SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name]
FROM AccountHolders AS ah
INNER JOIN (SELECT AccountHolderId, SUM(Balance) AS TotalBalance
			FROM Accounts 
			GROUP BY AccountHolderId) AS a
ON ah.Id = a.AccountHolderId
WHERE a.TotalBalance > @number

-- Problem 11

GO
CREATE FUNCTION ufn_CalculateFutureValue (@sum money, @yearlyInterestRate float, @years int)
RETURNS money
AS
BEGIN
	DECLARE @result money
	SET @result = @sum * POWER((1 + @yearlyInterestRate), @years)
	RETURN @result
END
GO

DECLARE @ret money
EXEC @ret = dbo.ufn_CalculateFutureValue
			@sum = 1000,
			@yearlyInterestRate = 0.1,
			@years = 5
SELECT @ret AS [Future Value]

-- Problem 12

GO
CREATE PROC usp_CalculateFutureValueForAccount (@accountID int, @yearlyInterestRate float)
AS
SELECT  a.Id AS [Account Id],
		ah.FirstName AS [First Name],
		ah.LastName AS [Last Name],
		a.Balance AS [Current Balance],
		dbo.ufn_CalculateFutureValue (a.Balance, @yearlyInterestRate, 5) AS [Balance in 5 years]
FROM AccountHolders AS ah
INNER JOIN  Accounts AS a 
ON ah.Id = a.AccountHolderId
WHERE a.Id = @accountID

EXEC usp_CalculateFutureValueForAccount @accountID = 1,
										@yearlyInterestRate = 0.1

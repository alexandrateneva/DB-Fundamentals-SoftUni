USE Gringotts

-- Problem 1

SELECT COUNT(Id) FROM WizzardDeposits

-- Problem 2

SELECT MAX(MagicWandSize) AS [Longest Magic Wand] FROM WizzardDeposits 

-- Problem 3

SELECT DepositGroup,
	   MAX(MagicWandSize) AS [Longest Magic Wand]
FROM WizzardDeposits
GROUP BY DepositGroup

-- Problem 4

SELECT TOP 1 WITH TIES DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)

-- Problem 5

SELECT DepositGroup,
	   SUM(DepositAmount) AS [Total Sum]
FROM WizzardDeposits
GROUP BY DepositGroup

-- Problem 6

SELECT DepositGroup,
	   SUM(DepositAmount) AS [Total Sum]
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

-- Problem 7

SELECT DepositGroup,
	   SUM(DepositAmount) AS [Total Sum]
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY [Total Sum] DESC

-- Problem 8

SELECT DepositGroup,
	   MagicWandCreator,
	   MIN(DepositCharge)  
FROM WizzardDeposits
GROUP BY DepositGroup, MagicWandCreator
ORDER BY MagicWandCreator, DepositGroup

-- Problem 9

SELECT 
  CASE
	  WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
	  WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
	  WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
	  WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
	  WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
	  WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
	  WHEN Age >= 61 THEN '[61+]'
  END AS [Age Group],
COUNT(*) AS [Wizard Count]
FROM WizzardDeposits 
GROUP BY 
  CASE
	  WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
	  WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
	  WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
	  WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
	  WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
	  WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
	  WHEN Age >= 61 THEN '[61+]'
  END

-- Problem 10

SELECT LEFT(FirstName, 1) AS [First Letter]
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY LEFT(FirstName, 1)

-- Problem 11

SELECT DepositGroup, 
       IsDepositExpired, 
       AVG(DepositInterest) AS [AverageInterest]
FROM WizzardDeposits
WHERE DepositStartDate > '1985-01-01'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired 

-- Problem 12

SELECT SUM(wizardDep.Difference) 
FROM (
	SELECT FirstName,
	       DepositAmount,
		   LEAD(FirstName) OVER (ORDER BY Id) AS GuestWizard,
		   LEAD(DepositAmount) OVER (ORDER BY Id) AS GuestDeposit,
		   DepositAmount - LEAD(DepositAmount) OVER (ORDER BY Id) AS Difference
	FROM WizzardDeposits
) AS wizardDep
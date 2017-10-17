USE Bank

-- Problem 1

CREATE TABLE Logs
(
LogId INT
PRIMARY KEY IDENTITY NOT NULL,
AccountId INT FOREIGN KEY REFERENCES Accounts(Id) NOT NULL,
OldSum MONEY,
NewSum MONEY
)

GO
CREATE TRIGGER tr_BalanceUpdate ON Accounts
FOR UPDATE
AS
INSERT INTO Logs (AccountId, OldSum, NewSum)
SELECT i.Id,
       d.Balance,
       i.Balance
FROM inserted AS i
INNER JOIN deleted AS d ON i.Id = d.Id

-- Problem 2

CREATE TABLE NotificationEmails
(
Id INT
PRIMARY KEY IDENTITY NOT NULL,
Recipient INT FOREIGN KEY REFERENCES Accounts(Id) NOT NULL,
Subject VARCHAR(MAX),
Body VARCHAR(MAX)
)

GO
CREATE TRIGGER tr_UpdateBalanceNotification ON Accounts
FOR UPDATE
AS
INSERT INTO NotificationEmails (Recipient, Subject, Body)
SELECT i.Id,
       CONCAT('Balance change for account: ', i.Id),
       CONCAT('On ', GETDATE(), ' your balance was changed from ', d.Balance, ' to ', i.Balance, '.')
FROM inserted AS i
INNER JOIN deleted AS d ON i.Id = d.Id

-- Problem 3

GO
CREATE PROC usp_DepositMoney
(@accountId   INT,
 @moneyAmount MONEY
)
AS
BEGIN TRAN
    UPDATE Accounts
    SET Balance = Balance + @moneyAmount
    WHERE Id = @accountId
COMMIT

-- Problem 4

GO
CREATE PROC usp_WithdrawMoney (@accountId INT, @moneyAmount MONEY)
AS
BEGIN TRAN
    UPDATE Accounts
    SET Balance = Balance - @moneyAmount
    WHERE Id = @accountId
COMMIT

-- Problem 5

GO
CREATE PROC usp_TransferMoney (@senderId INT, @receiverId  INT, @moneyAmount MONEY)
AS
BEGIN TRAN
    IF(@moneyAmount < 0)
        BEGIN
            RAISERROR('Amount should be positite number!', 16, 1);
            ROLLBACK;
            RETURN;
    END;
    EXEC usp_DepositMoney
         @receiverId,
         @moneyAmount;
    EXEC usp_WithdrawMoney
         @senderId,
         @moneyAmount;
COMMIT


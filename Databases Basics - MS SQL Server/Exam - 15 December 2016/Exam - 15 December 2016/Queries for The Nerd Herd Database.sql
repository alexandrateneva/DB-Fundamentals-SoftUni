CREATE DATABASE TheNerdHerd

USE TheNerdHerd

-- 1. Database design 

CREATE TABLE Credentials
(
Id int PRIMARY KEY IDENTITY,
Email varchar(30),
Password varchar(20)
)

CREATE TABLE Locations
(
Id int PRIMARY KEY IDENTITY,
Latitude float,
Longitude float
)

CREATE TABLE Users
(
Id int PRIMARY KEY IDENTITY,
Nickname varchar(25),
Gender char(1),
Age int,
LocationId int,
CredentialId int UNIQUE,
CONSTRAINT FK_Users_Locations FOREIGN KEY (LocationId) REFERENCES Locations (Id),
CONSTRAINT FK_Users_Credentials FOREIGN KEY (CredentialId) REFERENCES Credentials (Id)
)

CREATE TABLE Chats
(
Id int PRIMARY KEY IDENTITY,
Title varchar(32),
StartDate date,
IsActive bit
)

CREATE TABLE Messages
(
Id int PRIMARY KEY IDENTITY,
Content varchar(200),
SentOn date,
ChatId int,
UserId int,
CONSTRAINT FK_Messages_Chats FOREIGN KEY (ChatId) REFERENCES Chats (Id),
CONSTRAINT FK_Messages_Users FOREIGN KEY (UserId) REFERENCES Users (Id)
)

CREATE TABLE UsersChats
(
UserId int,
ChatId int,
CONSTRAINT PK_UsersChats PRIMARY KEY (ChatId, UserId),
CONSTRAINT FK_UsersChats_Users FOREIGN KEY (UserId) REFERENCES Users (Id),
CONSTRAINT FK_UsersChats_Chats FOREIGN KEY (ChatId) REFERENCES Chats (Id)
)

-- 2. Insert

INSERT Messages (Content, SentOn, ChatId, UserId)
SELECT CONCAT(us.Age, '-', us.Gender, '-', l.Latitude, '-', l.Longitude),
	  GETDATE(),
	  ChatId = CASE
	             WHEN us.Gender = 'F' THEN CEILING(SQRT((us.Age * 2)))
	             WHEN us.Gender = 'M' THEN CEILING(POWER((us.Age / 18), 3))
	           END,
	  us.Id
FROM Users AS us
INNER JOIN Locations AS l
ON us.LocationId = l.Id 
WHERE us.Id BETWEEN 10 AND 20

-- 3. Update

UPDATE Chats
SET StartDate = t.FirstMsgDate
FROM
Chats AS ch
INNER JOIN (SELECT c.Id AS ChatId, MIN(m.SentOn) AS FirstMsgDate
	       FROM Chats AS c
	       INNER JOIN Messages AS m
	       ON m.ChatId = c.Id
	       GROUP BY c.Id
		  ) AS t
ON t.ChatId = ch.Id
WHERE ch.StartDate > t.FirstMsgDate

-- 4. Delete

DELETE FROM Locations
WHERE Id IN (SELECT l.Id FROM Locations AS l
             LEFT OUTER JOIN Users AS u ON u.LocationId = l.Id
             WHERE u.Id IS NULL)

-- 5. Age Range

SELECT Nickname, Gender, Age 
FROM Users
WHERE Age BETWEEN 22 AND 37

-- 6. Messages

SELECT Content, SentOn
FROM Messages
WHERE (Content LIKE '%just%') AND (SentOn > '2014-05-12')
ORDER BY Id DESC

-- 7. Chats

SELECT Title, IsActive FROM Chats
WHERE IsActive = 0 AND LEN(Title) < 5 OR SUBSTRING(Title, 3, 2) = 'tl'
ORDER BY Title DESC 

-- 8. Chat Messages

SELECT c.Id, c.Title, m.Id
FROM Chats AS c
INNER JOIN Messages AS m ON c.Id = m.ChatId
WHERE m.SentOn < '2012-03-26' AND RIGHT(c.Title, 1) = 'x'
ORDER BY c.Id, m.Id

-- 9. Message Count

SELECT TOP (5) c.Id, 
               COUNT(m.Id) AS TotalMessages
FROM Chats AS c
RIGHT OUTER JOIN Messages AS m ON c.Id = m.ChatId
WHERE m.Id < 90
GROUP BY c.Id
ORDER BY TotalMessages DESC, c.Id

-- 10. Credentials

SELECT Nickname, Email, Password
FROM Users AS u
INNER JOIN Credentials AS c ON u.CredentialId = c.Id
WHERE Email LIKE '%co.uk'
ORDER BY Email

-- 11. Locations

SELECT u.Id, u.Nickname, u.Age 
FROM Users AS u
LEFT OUTER JOIN Locations AS l ON u.LocationId = l.Id
WHERE l.Id IS NULL

-- 12. Left Users

SELECT m.Id, m.ChatId, m.UserId
FROM Messages AS m
WHERE (m.UserId NOT IN (SELECT uc.UserId 
				    FROM UsersChats AS uc 
				    INNER JOIN Messages AS m 
				    ON uc.ChatId = m.ChatId 
				    WHERE uc.UserId = m.UserId) 
       OR m.UserId IS NULL) 
AND m.ChatId = 17
ORDER BY m.Id DESC

-- 13. Users in Bulgaria

SELECT u.Nickname, c.Title, l.Latitude, l.Longitude
FROM Users AS u
INNER JOIN Locations AS l ON u.LocationId = l.Id
LEFT OUTER JOIN UsersChats AS uc ON u.Id = uc.UserId
LEFT OUTER JOIN Chats AS c ON uc.ChatId = c.Id
WHERE (Latitude BETWEEN 41.14 AND CAST(44.13 AS NUMERIC(18,0))) 
  AND (Longitude BETWEEN 22.21 AND CAST(28.36 AS NUMERIC(18,0))) 
ORDER BY c.Title

-- 14. Last Chat

SELECT TOP (1) WITH TIES c.Title, m.Content
FROM Chats AS c
LEFT OUTER JOIN Messages AS m ON c.Id = m.ChatId
ORDER BY StartDate DESC

-- 15. Radians

GO
CREATE FUNCTION udf_GetRadians (@degrees float)
RETURNS float
BEGIN
   RETURN @degrees * PI() / 180
END
GO

SELECT dbo.udf_GetRadians(22.12) AS Radians

-- 16. Change Password

GO
CREATE PROCEDURE udp_ChangePassword(@Email varchar(30), @Password varchar(20))
AS
BEGIN
	IF EXISTS (SELECT * FROM Credentials WHERE Email = @Email)
	BEGIN
		UPDATE Credentials
		SET Password = @Password
		WHERE Email = @Email
	END
	ELSE
	BEGIN
		RAISERROR('The email does''t exist!', 16, 1)
	END
END

EXEC udp_ChangePassword 'abarnes0@sogou.com','new_pass'

-- 17. Send Message

GO 
CREATE PROC udp_SendMessage (@userId int, @chatId int, @content varchar(255))
AS
BEGIN
     IF EXISTS (SELECT * FROM UsersChats WHERE UserId = @userId AND ChatId = @chatId)
     BEGIN
     	INSERT INTO Messages (UserId, ChatId, Content, SentOn)
		VALUES (@userId, @chatId, @content, GETDATE())
     END
     ELSE
     BEGIN
     	RAISERROR('There is no chat with that user!', 16, 1)
     END
END

EXEC dbo.udp_SendMessage 19, 17, 'Awesome'

-- 18. Log Messages

CREATE TABLE MessageLogs
(
Id int PRIMARY KEY IDENTITY,
Content varchar(200),
SentOn date,
ChatId int,
UserId int
)

GO
CREATE TRIGGER tr_DeleteMsg ON Messages FOR DELETE
AS
BEGIN
     INSERT INTO MessageLogs (Id, Content, SentOn, ChatId, UserId)
     SELECT Id, Content, SentOn, ChatId, UserId
     FROM deleted 
END

DELETE FROM Messages
WHERE Messages.Id = 1

-- 19. Delete users

GO
CREATE TRIGGER tr_DeleteUser ON Users INSTEAD OF DELETE
AS
BEGIN
     DELETE FROM Messages
     WHERE UserId = (SELECT Id FROM deleted)
     
     DELETE FROM UsersChats
     WHERE UserId = (SELECT Id FROM deleted)
     
     DELETE FROM Users
     WHERE Id = (SELECT Id FROM deleted)
END

DELETE FROM Users
WHERE Users.Id = 1

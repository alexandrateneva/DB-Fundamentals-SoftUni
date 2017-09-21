-- Problem 8
CREATE DATABASE Users

USE Users

CREATE TABLE Users
(
Id bigint IDENTITY NOT NULL,
Username nvarchar(30) NOT NULL,
Password nvarchar(26) NOT NULL,
ProfilePicture varbinary CHECK(DATALENGTH(ProfilePicture) <= 921600),
LastLoginTime datetime,
IsDeleted bit,
CONSTRAINT PK_Id PRIMARY KEY (Id)
)

INSERT INTO Users(Username, Password, ProfilePicture, LastLoginTime, IsDeleted)
VALUES('ivan_123', '716z5G7h', 12345, '2017-06-18 10:34:09', 1)

INSERT INTO Users(Username, Password, ProfilePicture, LastLoginTime, IsDeleted)
VALUES('gosho_1994', '45gZ%hoa', 23451, '2017-09-21 22:56:17', 0)

INSERT INTO Users(Username, Password, ProfilePicture, LastLoginTime, IsDeleted)
VALUES('martin_ivanov', 'htY78$goal', 34512, '2017-05-11 15:20:19', 0)

INSERT INTO Users(Username, Password, ProfilePicture, LastLoginTime, IsDeleted)
VALUES('maria87', '123_mimi', 45123, '2017-03-01 08:20:48', 0)

INSERT INTO Users(Username, Password, ProfilePicture, LastLoginTime, IsDeleted)
VALUES('ani-maria', 'wee1989hZts', 51234, '2017-09-19 23:45:06', 0)

-- Problem 9
ALTER TABLE Users
DROP CONSTRAINT PK_Id;

ALTER TABLE Users
ADD CONSTRAINT PK_Person PRIMARY KEY (Id, Username);

-- Problem 10
ALTER TABLE Users
ADD CONSTRAINT SufficientLengthOfPassword
CHECK(LEN(Password) > 5)

-- Problem 11
ALTER TABLE Users
ADD DEFAULT GETDATE()
FOR LastLoginTime

-- Problem 12
ALTER TABLE Users
DROP CONSTRAINT PK_Person;

ALTER TABLE Users
ADD CONSTRAINT PK_Id PRIMARY KEY (Id);

ALTER TABLE Users
ADD CONSTRAINT SufficientLengthOfUsername
CHECK(LEN(Username) > 3)

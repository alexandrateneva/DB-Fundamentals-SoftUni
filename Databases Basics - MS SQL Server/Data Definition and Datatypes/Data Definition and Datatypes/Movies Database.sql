-- Problem 13
CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors
(
Id int IDENTITY PRIMARY KEY,
DirectorName nvarchar(50) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Genres
(
Id int IDENTITY PRIMARY KEY,
GenreName nvarchar(50) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Categories
(
Id int IDENTITY PRIMARY KEY,
CategoryName nvarchar(50) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Movies
(
Id int IDENTITY PRIMARY KEY,
Title nvarchar(50) NOT NULL,
DirectorId int FOREIGN KEY REFERENCES Directors(Id),
CopyrightYear int,
Length decimal(3,2),
GenreId int FOREIGN KEY REFERENCES Genres(Id),
CategoryId int FOREIGN KEY REFERENCES Categories(Id),
Rating int,
Notes nvarchar(max)
)

INSERT INTO Directors(DirectorName, Notes)
Values('Christopher Nolan', 'Best known for his cerebral, often nonlinear storytelling, acclaimed writer-director Christopher Nolan was born on July 30, 1970 in London, England.'),
('Quentin Tarantino','Quentin Jerome Tarantino was born in Knoxville, Tennessee.'),
('Emir Kusturica','A Serbian film director. Born in 1954 in Sarajevo.'),
('Martin Scorsese','Martin Charles Scorsese was born on November 17, 1942 in Queens, New York City.'),
('Steven Spielberg','Steven Allan Spielberg was born in 1946 in Cincinnati, Ohio.')

INSERT INTO Genres(GenreName, Notes)
VALUES('Action', 'An action story is similar to adventure, and the protagonist usually takes a risky turn, which leads to desperate situations (including explosions, fight scenes, daring escapes, etc.).'),
('Comedy','Comedy is a story that tells about a series of funny, or comical events, intended to make the audience laugh.'),
('Drama', 'Within film, television and radio (but not theatre), drama is a genre of narrative fiction (or semi-fiction) intended to be more serious than humorous in tone,focusing on in-depth development of realistic characters who must deal with realistic emotional struggles.'),
('Horror', 'A horror story is told to deliberately scare or frighten the audience, through suspense, violence or shock.'),
('Romance','Most often a romance is understood to be "love stories", emotion-driven stories that are primarily focused on the relationship between the main characters of the story.')

INSERT INTO Categories(CategoryName, Notes)
VALUES('Great', 'Ìovies that are rated as great'),
('Good', 'Ìovies that are rated as good'),
('Fine', 'Ìovies that are rated as fine'),
('Bad', 'Ìovies that are rated as bad'),
('Awful', 'Ìovies that are rated as awful')

INSERT INTO Movies(Title, DirectorId, CopyrightYear, Length, GenreId, CategoryId, Rating, Notes)
VALUES('Saving Private Ryan', 5, 1998, 2.49, 3, 1, 9, 'Following the Normandy Landings, a group of U.S. soldiers go behind enemy lines to retrieve a paratrooper whose brothers have been killed in action.'),
('Interstellar', 1, 2014, 2.49, 3, 2, 8, 'A team of explorers travel through a wormhole in space in an attempt to ensure humanity''s survival.'),
('Django Unchained', 2, 2012, 2.45, 1, 2, 8, 'With the help of a German bounty hunter, a freed slave sets out to rescue his wife from a brutal Mississippi plantation owner.'),
('Crna macka, beli macor', 3, 1998, 2.7, 2, 4, 8, 'Matko is a small time hustler, living by the river Danube with his 17 year old son Zare...'),
('The Wolf of Wall Street', 4, 2013, 3.0, 2, 3, 8, 'Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.')
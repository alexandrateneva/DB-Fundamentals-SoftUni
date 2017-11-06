namespace _1.Initial_Setup
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            SqlConnection defaultConnection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated security=true");
            defaultConnection.Open();
            using (defaultConnection)
            {
                string query = "CREATE DATABASE MinionsDB";
                SqlCommand command = new SqlCommand(query, defaultConnection);
                command.ExecuteNonQuery();
            }

            SqlConnection connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated security=true");
            connection.Open();
            using (connection)
            {
                string createTableCountriesQuery = @"CREATE TABLE Countries
                                                 (
                                                 CountryId int PRIMARY KEY IDENTITY,
                                                 Name nvarchar(50) NOT NULL
                                                 )";
                SqlCommand createTableCountriesCmd = new SqlCommand(createTableCountriesQuery, connection);
                createTableCountriesCmd.ExecuteNonQuery();

                string createTableTownsQuery = @"CREATE TABLE Towns
                                                (
                                                TownId int PRIMARY KEY IDENTITY,
                                                Name nvarchar(50) NOT NULL,
                                                CountryId int FOREIGN KEY REFERENCES Countries (CountryId)
                                                )";
                SqlCommand createTableTownsCmd = new SqlCommand(createTableTownsQuery, connection);
                createTableTownsCmd.ExecuteNonQuery();

                string createTableMinionsQuery = @"CREATE TABLE Minions
                                                (
                                                MinionId int PRIMARY KEY IDENTITY,
                                                Name nvarchar(50) NOT NULL,
                                                Age int NOT NULL,
                                                TownId int FOREIGN KEY REFERENCES Towns (TownId)
                                                )";
                SqlCommand createTableMinionsCmd = new SqlCommand(createTableMinionsQuery, connection);
                createTableMinionsCmd.ExecuteNonQuery();

                string createTableVillainsQuery = @"CREATE TABLE Villains
                                                 (
                                                 VillainId int PRIMARY KEY IDENTITY,
                                                 Name nvarchar(50) NOT NULL,
                                                 EvilnessFactor varchar(15) CHECK(EvilnessFactor IN ('good', 'bad', 'evil', 'super evil')) NOT NULL
                                                 )";
                SqlCommand createTableVillainsCmd = new SqlCommand(createTableVillainsQuery, connection);
                createTableVillainsCmd.ExecuteNonQuery();

                string createTableMinionsVillainsQuery = @"CREATE TABLE MinionsVillains
                                                        (
                                                        MinionId int,
                                                        VillainId int,
                                                        CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId),
                                                        CONSTRAINT FK_MinionsVillains_Minions FOREIGN KEY (MinionId) REFERENCES Minions (MinionId),
                                                        CONSTRAINT FK_MinionsVillains_Villains FOREIGN KEY (VillainId) REFERENCES Villains (VillainId)
                                                        )";
                SqlCommand createTableMinionsVillainsCmd = new SqlCommand(createTableMinionsVillainsQuery, connection);
                createTableMinionsVillainsCmd.ExecuteNonQuery();

                //Insert Statements

                string insertIntoCountriesQuery = @"INSERT INTO Countries (Name) 
                                                VALUES ('Germany'),
                                                ('France'),
                                                ('England'),
                                                ('Bulgaria'),
                                                ('USA')";
                SqlCommand insertIntoCountriesCmd = new SqlCommand(insertIntoCountriesQuery, connection);
                insertIntoCountriesCmd.ExecuteNonQuery();

                string insertIntoTownsQuery = @"INSERT INTO Towns (Name, CountryId)
                                            VALUES ('Berlin', 1),
                                            ('Paris', 2),
                                            ('London', 3),
                                            ('Sofia', 4),
                                            ('New York', 5)";
                SqlCommand insertIntoTownsCmd = new SqlCommand(insertIntoTownsQuery, connection);
                insertIntoTownsCmd.ExecuteNonQuery();

                string insertIntoMinionsQuery = @"INSERT INTO Minions (Name, Age, TownId)
                                              VALUES ('Kevin', 11, 2),
                                              ('Bob', 12, 1),
                                              ('Steward', 10, 5),
                                              ('Pesho', 14, 4),
                                              ('Ani', 13, 2)";
                SqlCommand insertIntoMinionsCmd = new SqlCommand(insertIntoMinionsQuery, connection);
                insertIntoMinionsCmd.ExecuteNonQuery();

                string insertIntoVillainsQuery = @"INSERT INTO Villains (Name, EvilnessFactor)
                                               VALUES ('Gru', 'good'),
                                               ('Vector', 'evil'),
                                               ('Shannon', 'bad'),
                                               ('Ivan', 'super evil'),
                                               ('Gosho', 'good')";
                SqlCommand insertIntoVillainsCmd = new SqlCommand(insertIntoVillainsQuery, connection);
                insertIntoVillainsCmd.ExecuteNonQuery();

                string insertIntoMinionsVillainsQuery = @"INSERT INTO MinionsVillains (MinionId, VillainId)
                                                         VALUES (1, 5),
                                                         (2, 4),
                                                         (3, 3),
                                                         (4, 2),
                                                         (5, 1)";
                SqlCommand insertIntoMinionsVillainsCmd = new SqlCommand(insertIntoMinionsVillainsQuery, connection);
                insertIntoMinionsVillainsCmd.ExecuteNonQuery();

                Console.WriteLine("Database MinionsDb and all necessary tables has been created successfully \nand all records have been inserted correctly!");
            }
        }
    }
}

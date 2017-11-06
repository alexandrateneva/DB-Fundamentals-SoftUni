namespace _4.Add_Minion
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Minion: ");
            string[] minionInfo = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string minionName = minionInfo[0];
            int minionAge = int.Parse(minionInfo[1]);
            string townName = minionInfo[2];

            Console.Write("Villain: ");
            string[] villainInfo = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string villainName = villainInfo[0];

            SqlConnection connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated security=true");
            connection.Open();
            using (connection)
            {
                string queryToCheckTown = @"SELECT TownId FROM Towns WHERE Name = @TownName";
                SqlCommand commandToCheckIdOfTown = new SqlCommand(queryToCheckTown, connection);
                commandToCheckIdOfTown.Parameters.AddWithValue("@TownName", townName);
                object townId = commandToCheckIdOfTown.ExecuteScalar();

                if (townId == null)
                {
                    string insertIntoTownsQuery = @"INSERT INTO Towns (Name, CountryId)
                                                    VALUES (@TownName, NULL)";
                    SqlCommand insertIntoTownsCmd = new SqlCommand(insertIntoTownsQuery, connection);
                    insertIntoTownsCmd.Parameters.AddWithValue("@TownName", townName);
                    insertIntoTownsCmd.ExecuteNonQuery();
                    Console.WriteLine($"Town {townName} was added to the database.");
                }

                townId = (int)commandToCheckIdOfTown.ExecuteScalar();

                string queryToCheckMinionId = @"SELECT MinionId FROM Minions WHERE Name = @MinionName AND Age = @MinionAge AND TownId = @TownId";
                SqlCommand commandToCheckMinionId = new SqlCommand(queryToCheckMinionId, connection);
                commandToCheckMinionId.Parameters.AddWithValue("@MinionName", minionName);
                commandToCheckMinionId.Parameters.AddWithValue("@MinionAge", minionAge);
                commandToCheckMinionId.Parameters.AddWithValue("@TownId", townId);

                if (commandToCheckMinionId.ExecuteScalar() == null)
                {
                    string queryToCheckVillain = @"SELECT VillainId FROM Villains WHERE Name = @VillainName";
                    SqlCommand commandToCheckVillain = new SqlCommand(queryToCheckVillain, connection);
                    commandToCheckVillain.Parameters.AddWithValue("@VillainName", villainName);
                    object villainId = commandToCheckVillain.ExecuteScalar();

                    if (villainId == null)
                    {
                        string insertIntoVillainsQuery = @"INSERT INTO Villains (Name, EvilnessFactor)
                                                       VALUES (@VillainName, 'evil')";
                        SqlCommand insertIntoVillainsCmd = new SqlCommand(insertIntoVillainsQuery, connection);
                        insertIntoVillainsCmd.Parameters.AddWithValue("@VillainName", villainName);
                        insertIntoVillainsCmd.ExecuteNonQuery();
                        Console.WriteLine($"Villain {villainName} was added to the database.");
                    }

                    string insertIntoMinionsQuery = @"INSERT INTO Minions (Name, Age, TownId)
                                                  VALUES (@MinionName, @MinionAge, @TownId);
                                                  SELECT SCOPE_IDENTITY()";
                    SqlCommand insertIntoMinionsCmd = new SqlCommand(insertIntoMinionsQuery, connection);
                    insertIntoMinionsCmd.Parameters.AddWithValue("@MinionName", minionName);
                    insertIntoMinionsCmd.Parameters.AddWithValue("@MinionAge", minionAge);
                    insertIntoMinionsCmd.Parameters.AddWithValue("@TownId", townId);
                    int minionId = (int)insertIntoMinionsCmd.ExecuteScalar();

                    villainId = (int)commandToCheckVillain.ExecuteScalar();
                    string insertIntoMinionsVillainsQuery = $@"INSERT INTO MinionsVillains (MinionId, VillainId)
                                                         VALUES ({minionId}, {villainId})";
                    SqlCommand insertIntoMinionsVillainsCmd = new SqlCommand(insertIntoMinionsVillainsQuery, connection);
                    insertIntoMinionsVillainsCmd.ExecuteNonQuery();

                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                }
                else
                {
                    Console.WriteLine("That minion already exists in the database.");
                }
            }
        }
    }
}

namespace _3.Get_Minion_Names_By_Villain_Id
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Please enter Villain Id:");
            var villainId = int.Parse(Console.ReadLine());

            SqlConnection connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated security=true");
            connection.Open();
            using (connection)
            {
                string queryToCheckId = "SELECT Name FROM Villains WHERE VillainId = @VillainId";
                SqlCommand commandToCheckId = new SqlCommand(queryToCheckId, connection);
                commandToCheckId.Parameters.AddWithValue("@VillainId", villainId);
                string nameOfVillain = (string)commandToCheckId.ExecuteScalar();

                if (nameOfVillain == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                    Environment.Exit(0);
                }
                Console.WriteLine($"Villain: {nameOfVillain}");

                string queryToGetInfoForMinions = @"SELECT m.Name, m.Age
                                                  FROM Villains AS v
                                                  INNER JOIN MinionsVillains AS mv ON v.VillainId = mv.VillainId
                                                  LEFT OUTER JOIN Minions AS m ON mv.MinionId = m.MinionId
                                                  WHERE v.VillainId = @VillainId";
                SqlCommand commandToGetInfoForMinions = new SqlCommand(queryToGetInfoForMinions, connection);
                commandToGetInfoForMinions.Parameters.AddWithValue("@VillainId", villainId);
                var counter = 1;
                SqlDataReader reader = commandToGetInfoForMinions.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("(no minions)");
                    Environment.Exit(0);
                }

                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        int age = (int)reader["Age"];
                        Console.WriteLine($"{counter}. {name}  {age}");
                        counter++;
                    }
                }
            }
        }
    }
}

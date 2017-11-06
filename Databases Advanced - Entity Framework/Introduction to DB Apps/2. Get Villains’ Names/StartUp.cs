namespace _2.Get_Villains__Names
{
    using System;
    using System.Data.SqlClient;

    public class StarUp
    {
        public static void Main()
        {
            SqlConnection connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated security=true");
            connection.Open();
            using (connection)
            {
                string query = @"SELECT v.Name, COUNT(mv.MinionId) AS CountOfMinions
                                 FROM Villains AS v
                                 INNER JOIN MinionsVillains AS mv ON v.VillainId = mv.VillainId
                                 GROUP BY v.Name
                                 HAVING COUNT(mv.MinionId) > 3
                                 ORDER BY COUNT(mv.MinionId)";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string) reader["Name"];
                        int number = (int) reader["CountOfMinions"];
                        Console.WriteLine(name + "   " + number);
                    }
                }
            }
        }
    }
}

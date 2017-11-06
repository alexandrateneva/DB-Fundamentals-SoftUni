namespace _8.Increase__Age_And_Change_Name_Title_Case
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Please, enter valid Minions Ids: ");
            int[] minionsIds = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            SqlConnection connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated security=true");
            connection.Open();
            using (connection)
            {
                StringBuilder sb = new StringBuilder();

                for (int j = 0; j < minionsIds.Length; j++)
                {
                    // IN clause
                    sb.Append("@MinionId" + (j + 1) + ",");
                }
                sb.Length -= 1;

                string query = $@"UPDATE Minions
                                 SET Name = CONCAT(UPPER(LEFT(Name, 1)), LOWER(RIGHT(Name, (LEN(Name) - 1)))),
                                 Age = Age + 1
                                 WHERE MinionId IN ({sb})";
                SqlCommand command = new SqlCommand(query, connection);

                for (int i = 0; i < minionsIds.Length; i++)
                {
                    // Parameter
                    command.Parameters.AddWithValue("@MinionId" + (i + 1), minionsIds[i]);
                }

                command.ExecuteNonQuery();

                string queryForReader = @"SELECT Name, Age FROM Minions";
                SqlCommand commandForReader = new SqlCommand(queryForReader, connection);
                SqlDataReader reader = commandForReader.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        int age = (int)reader["Age"];
                        Console.WriteLine(name + "  " + age);
                    }
                }
            }
        }
    }
}

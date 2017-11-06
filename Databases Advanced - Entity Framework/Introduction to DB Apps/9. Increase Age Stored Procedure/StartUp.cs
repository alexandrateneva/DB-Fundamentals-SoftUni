namespace _9.Increase_Age_Stored_Procedure
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            //PROC in SSMS:

            //string query = @"CREATE PROC usp_GetOlder (@MinionId int)
            //                     AS
            //                     BEGIN
            //                          UPDATE Minions
            //                          SET Age = Age + 1
            //                          WHERE MinionId = @MinionId
            //                     END";

            Console.Write("Please, enter valid Minion Id: ");
            int minionId = int.Parse(Console.ReadLine().Trim());

            SqlConnection connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated security=true");
            connection.Open();
            using (connection)
            {
                SqlCommand command = new SqlCommand("usp_GetOlder", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@MinionId", minionId);
                command.ExecuteNonQuery();

                string queryForReader = @"SELECT Name, Age FROM Minions WHERE MinionId = @MinionId";
                SqlCommand commandForReader = new SqlCommand(queryForReader, connection);
                commandForReader.Parameters.AddWithValue("@MinionId", minionId);
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

namespace _7.Print_All_Minion_Names
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            SqlConnection connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated security=true");
            connection.Open();
            using (connection)
            {
                string queryForReader = @"SELECT Name FROM Minions";
                SqlCommand commandForReader = new SqlCommand(queryForReader, connection);
                SqlDataReader reader = commandForReader.ExecuteReader();
                List<string> minionsNames = new List<string>();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        minionsNames.Add(name);
                    }
                }

                int counter = 1;
                int counterForward = 0;
                int counterBackward = minionsNames.Count - 1;

                while (counterBackward >= counterForward)
                {
                    Console.WriteLine(counter + ". " + minionsNames[counterForward]);
                    counter++;
                    if (counterForward == counterBackward)
                    {
                        break;
                    }
                    Console.WriteLine(counter + ". " + minionsNames[counterBackward]);
                    counter++;

                    counterForward++;
                    counterBackward--;
                }
            }
        }
    }
}

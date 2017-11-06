namespace _5.Change_Town_Names_Casing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Please, enter a country name: ");
            string country = Console.ReadLine().Trim();

            SqlConnection connection =
                new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated security=true");
            connection.Open();
            using (connection)
            {
                string query = @"UPDATE Towns
                                 SET Name = UPPER(Name)
                                 WHERE Name IN (
                                                SELECT t.Name 
                                                FROM Countries AS c
                                                LEFT OUTER JOIN Towns AS t ON c.CountryId = t.CountryId
                                 			    WHERE c.Name = @CountryName);
                                 SELECT @@ROWCOUNT";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CountryName", country);
                int numberOfTowns = (int)command.ExecuteScalar();
                if (numberOfTowns == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    string queryForReader = @"SELECT t.Name 
                                              FROM Countries AS c
                                              LEFT OUTER JOIN Towns AS t ON c.CountryId = t.CountryId
                                              WHERE c.Name = @CountryName";
                    SqlCommand commandForReader = new SqlCommand(queryForReader, connection);
                    commandForReader.Parameters.AddWithValue("@CountryName", country);
                    SqlDataReader reader = commandForReader.ExecuteReader();
                    List<string> towns = new List<string>();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            towns.Add(name);
                        }
                    }

                    Console.WriteLine($"{numberOfTowns} town names were affected.");
                    Console.WriteLine($"[{string.Join(", ", towns)}]");
                }
            }
        }
    }
}
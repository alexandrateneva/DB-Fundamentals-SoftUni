namespace _6.Remove_Villain
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Please enter Villain Id: ");
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
                    Console.WriteLine("No such villain was found.");
                    Environment.Exit(0);
                }

                string queryToDeleteMinions = @"DELETE FROM MinionsVillains
                                               WHERE VillainId = @VillainId;
                                               SELECT @@ROWCOUNT;";
                SqlCommand commandToDeleteMinions = new SqlCommand(queryToDeleteMinions, connection);
                commandToDeleteMinions.Parameters.AddWithValue("@VillainId", villainId);
                int numberOfMinions = (int) commandToDeleteMinions.ExecuteScalar();

                string queryToDeleteVillain = "DELETE FROM Villains WHERE VillainId = @VillainId";
                SqlCommand commandToDeleteVillain = new SqlCommand(queryToDeleteVillain, connection);
                commandToDeleteVillain.Parameters.AddWithValue("@VillainId", villainId);
                commandToDeleteVillain.ExecuteNonQuery();

                Console.WriteLine($"{nameOfVillain} was deleted.");
                Console.WriteLine($"{numberOfMinions} minions released.");
            }
        }
    }
}

namespace _5.Football_Team_Generator
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            var teams = new Dictionary<string, Team>();
            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    var tokens = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    switch (tokens[0])
                    {
                        case "Team":
                            var team = new Team(tokens[1]);
                            teams.Add(team.Name, team);
                            break;

                        case "Add":
                            if (!teams.ContainsKey(tokens[1]))
                            {
                                throw new InvalidOperationException($"Team {tokens[1]} does not exist.");
                            }
                            var player = new Player(tokens[2], int.Parse(tokens[3]), int.Parse(tokens[4]),
                                int.Parse(tokens[5]), int.Parse(tokens[6]), int.Parse(tokens[7]));
                            teams[tokens[1]].AddPlayer(player);
                            break;

                        case "Remove":
                            teams[tokens[1]].RemovePlayer(tokens[2]);
                            break;

                        case "Rating":
                            if (!teams.ContainsKey(tokens[1]))
                            {
                                throw new InvalidOperationException($"Team {tokens[1]} does not exist.");
                            }
                            var rating = teams[tokens[1]].GetRate();
                            Console.WriteLine($"{tokens[1]} - {rating:F0}");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}

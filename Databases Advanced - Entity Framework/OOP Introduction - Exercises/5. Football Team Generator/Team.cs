namespace _5.Football_Team_Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Team
    {
        private string name;
        private List<Player> players;

        public Team(string name)
        {
            this.Name = name;
            this.players = new List<Player>();
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                this.name = value;
            }
        }

        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void RemovePlayer(string playersName)
        {
            var player = this.players.SingleOrDefault(p => p.Name == playersName);
            if (player == null)
            {
                throw new InvalidOperationException($"Player {playersName} is not in {this.Name} team. ");
            }

            this.players.Remove(player);
        }

        public double GetRate()
        {
            if (this.players.Count == 0)
            {
                return 0;
            }

            return this.players.Average(p => p.GetSkill());
        }
    }
}
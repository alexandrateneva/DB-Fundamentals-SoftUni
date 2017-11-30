namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Models;
    using Data;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;

    [LogState(Log.Login)]
    public class AddTownCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public AddTownCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 2)
            {
                throw new ArgumentException("Invalid command!");
            }

            var townName = data[0];
            var country = data[1];

            var alreadyExistTowns = this.context.Towns.Select(u => u.Name).ToArray();

            if (alreadyExistTowns.Contains(townName))
            {
                throw new InvalidOperationException($"Town {townName} was already added!");
            }

            var town = new Town
            {
                Name = townName,
                Country = country
            };

            this.context.Towns.Add(town);
            this.context.SaveChanges();

            return $"Town {townName} was added successfully!";
        }
    }
}

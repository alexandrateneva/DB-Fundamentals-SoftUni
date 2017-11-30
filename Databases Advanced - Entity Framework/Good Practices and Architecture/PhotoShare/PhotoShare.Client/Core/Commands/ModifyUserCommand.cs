namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Models;

    [LogState(Log.Login, Role = Role.Owner)]
    public class ModifyUserCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public ModifyUserCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 3)
            {
                throw new ArgumentException("Invalid command!");
            }

            var username = data[0];
            var property = data[1];
            var newValue = data[2];

            var user = this.context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (Session.User.Username != user.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            switch (property)
            {
                case "Password":
                    if (!(newValue.Any(char.IsDigit) && newValue.Any(char.IsLower)))
                    {
                        throw new ArgumentException("Invalid Password!");
                    }
                    user.Password = newValue;
                    break;

                case "BornTown":
                    var bornTown = this.context.Towns.FirstOrDefault(u => u.Name == newValue);
                    if (bornTown == null)
                    {
                        throw new ArgumentException($"Town {newValue} not found!");
                    }
                    user.BornTown = bornTown;
                    break;

                case "CurrentTown":
                    var currentTown = this.context.Towns.FirstOrDefault(u => u.Name == newValue);
                    if (currentTown == null)
                    {
                        throw new ArgumentException($"Town {newValue} not found!");
                    }
                    user.CurrentTown = currentTown;
                    break;

                default:
                    throw new ArgumentException($"Property {property} not supported!");
            }
            
            this.context.SaveChanges();

            return $"User {username} {property} is {newValue}.";
        }
    }
}

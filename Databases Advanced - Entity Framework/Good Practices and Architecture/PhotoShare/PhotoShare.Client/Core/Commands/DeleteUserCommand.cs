namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Models;

    [LogState(Log.Login, Role = Role.Owner)]
    public class DeleteUserCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public DeleteUserCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 1)
            {
                throw new ArgumentException("Invalid command!");
            }

            var username = data[0];
            var user = this.context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new InvalidOperationException($"User {username} was not found!");
            }

            if (Session.User.Username != user.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (user.IsDeleted == true)
            {
                throw new InvalidOperationException($"User {username} is already deleted!");
            }

            user.IsDeleted = true;
            this.context.SaveChanges();

            return $"User {username} was deleted successfully!";
        }
    }
}

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;

    [LogState(Log.Logout)]
    public class LoginCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public LoginCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 2)
            {
                throw new ArgumentException("Invalid command!");
            }

            var username = data[0];
            var password = data[1];

            var user = this.context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            Session.User = user ?? throw new ArgumentException("Invalid username or password!");

            if (Session.GetLogStateOfUser() == Log.Login)
            {
                throw new ArgumentException("You should logout first!");
            }

            return $"User {username} successfully logged in!";
        }
    }
}

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Models;
    using Data;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;

    [LogState(Log.Logout)]
    public class RegisterUserCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public RegisterUserCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 4)
            {
                throw new ArgumentException("Invalid command!");
            }

            var username = data[0];
            var password = data[1];
            var repeatPassword = data[2];
            var email = data[3];

            var alreadyUsedUsernames = this.context.Users.Select(u => u.Username).ToArray();

            if (alreadyUsedUsernames.Contains(username))
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

           var user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();
            
            return $"User {username} was registered successfully!";
        }
    }
}

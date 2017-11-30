namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Models;

    [LogState(Log.Login)]
    public class LogoutCommand : ICommand
    {
        public string Execute(string[] data)
        {
            if (Session.GetLogStateOfUser() == Log.Logout)
            {
                throw new ArgumentException("You should log in first in order to logout.");
            }

            var result = $"User {Session.User.Username} successfully logged out!";

            Session.User = null;

            return result;
        }
    }
}

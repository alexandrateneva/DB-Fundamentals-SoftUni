namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Core.Commands.Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    
    public class ListFriendsCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public ListFriendsCommand(PhotoShareContext context)
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

            var user = this.context.Users
                .Include(u => u.FriendsAdded)
                .ThenInclude(fa => fa.Friend)
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var friends = user.FriendsAdded.Select(f => f.Friend.Username).OrderBy(n => n).ToArray();

            if (friends.Length == 0)
            {
                return "No friends for this user. :(";
            }

            return $"Friends: {Environment.NewLine + "---"}{string.Join(Environment.NewLine + "---", value: friends)}";
        }
    }
}

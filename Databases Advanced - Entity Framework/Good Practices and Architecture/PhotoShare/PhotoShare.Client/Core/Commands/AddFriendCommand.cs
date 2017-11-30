namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;

    [LogState(Log.Login, Role = Role.Owner)]
    public class AddFriendCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public AddFriendCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 2)
            {
                throw new ArgumentException("Invalid command!");
            }

            var firstUsername = data[0];
            var secondUsername = data[1];

            var requestingUser = this.context.Users
                .Include(u => u.FriendsAdded)
                .ThenInclude(fa => fa.Friend)
                .FirstOrDefault(u => u.Username == firstUsername); 

            var addedFriend = this.context.Users
                .Include(u => u.FriendsAdded)
                .ThenInclude(fa => fa.Friend)
                .FirstOrDefault(u => u.Username == secondUsername); 

            if (requestingUser == null)
            {
                throw new ArgumentException($"{firstUsername} not found!");
            }
            if (addedFriend == null)
            {
                throw new ArgumentException($"{secondUsername} not found!");
            }

            if (Session.User.Username != requestingUser.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var alreadyAdded = requestingUser.FriendsAdded.Any(u => u.Friend == addedFriend);
            var accepted = addedFriend.FriendsAdded.Any(u => u.Friend == requestingUser);
        
            if (alreadyAdded && !accepted)
            {
                throw new InvalidOperationException("Friend request has already sent!");
            }

            if (alreadyAdded && accepted)
            {
                throw new InvalidOperationException($"{secondUsername} is already a friend to {firstUsername}");
            }

            requestingUser.FriendsAdded
                .Add(new Friendship
                {
                    UserId = requestingUser.Id,
                    FriendId = addedFriend.Id
                });

            this.context.SaveChanges();

            return $"Friend {secondUsername} added to {firstUsername}";
        }
    }
}

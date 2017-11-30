namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;

    [LogState(Log.Login, Role = Role.Owner)]
    public class ShareAlbumCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public ShareAlbumCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 3)
            {
                throw new ArgumentException("Invalid command!");
            }

            var albumId = int.Parse(data[0]);
            var username = data[1];
            var permission = data[2];

            var user = this.context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (Session.User.Username != user.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var album = this.context.Albums.FirstOrDefault(a => a.Id == albumId);
            if (album == null)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            if (!Enum.TryParse(permission, out Role role))
            {
                throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
            }

            var albumRole = new AlbumRole()
            {
                UserId = user.Id,
                AlbumId = album.Id,
                Role = role
            };

            album.AlbumRoles.Add(albumRole);
            this.context.SaveChanges();

            return $"Username {username} added to album {albumId} ({permission})";
        }
    }
}

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;
    using Models;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Client.Utilities;

    [LogState(Log.Login, Role = Role.Owner)]
    public class CreateAlbumCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public CreateAlbumCommand(PhotoShareContext context)
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
            var albumTitle = data[1];
            var colorName = data[2];
            var tags = data.Skip(3).Select(t => t.ValidateOrTransform()).ToArray();

            var user = this.context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (Session.User.Username != user.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var albums = this.context.Albums.Select(a => a.Name).ToArray();
            if (albums.Contains(albumTitle))
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            var tagsInDatabase = this.context.Tags.Select(t => t.Name).ToArray();
            if (!tags.All(value => tagsInDatabase.Contains(value)))
            {
                throw new ArgumentException("Invalid tags!");
            }

            if (!Enum.TryParse(colorName, out Color color))
            {
                throw new ArgumentException($"Color {colorName} not found!");
            }

            var album = new Album
            {
                Name = albumTitle,
                BackgroundColor = color
            };

            album.AlbumRoles.Add(new AlbumRole { UserId = user.Id, Role = Role.Owner, AlbumId = album.Id });

            foreach (var tag in tags)
            {
                var currentTag = this.context.Tags.Single(t => t.Name == tag);

                var albumTag = new AlbumTag
                {
                    TagId = currentTag.Id,
                    AlbumId = album.Id
                };

                album.AlbumTags.Add(albumTag);
            }

            this.context.Albums.Add(album);
            this.context.SaveChanges();

            return $"Album {albumTitle} successfully created!";
        }
    }
}

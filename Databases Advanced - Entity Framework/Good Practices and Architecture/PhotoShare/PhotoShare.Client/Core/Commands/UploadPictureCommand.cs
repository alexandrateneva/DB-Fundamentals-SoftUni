namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Data;
    using Models;
    using PhotoShare.Client.Core.Commands.Attributes;

     [LogState(Log.Login, Role = Role.Owner)]
    public class UploadPictureCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public UploadPictureCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 3)
            {
                throw new ArgumentException("Invalid command!");
            } 
            var albumName = data[0];
            var pictureTitle = data[1];
            var pictureFilePath = data[2];

            var album = this.context.Albums.FirstOrDefault(a => a.Name == albumName);
            if (album == null)
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            var albumRoles = this.context.AlbumRoles.Where(a => a.AlbumId == album.Id).ToList();

            var isTheOwnerLogin = albumRoles.First(ar => ar.Role == Role.Owner && ar.UserId == Session.User.Id);

            if (isTheOwnerLogin == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var picture = new Picture
            {
                Title = pictureTitle,
                Path = pictureFilePath
            };

            album.Pictures.Add(picture);
            this.context.SaveChanges();

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}

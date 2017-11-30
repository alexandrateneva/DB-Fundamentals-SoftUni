namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;
    using Models;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Client.Utilities;

    [LogState(Log.Login)]
    public class AddTagToCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public AddTagToCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 2)
            {
                throw new ArgumentException("Invalid command!");
            }

            var albumName = data[0];
            var tagName = data[1];

            var album = this.context.Albums.FirstOrDefault(a => a.Name == albumName);
            var tag = this.context.Tags.FirstOrDefault(a => a.Name == tagName.ValidateOrTransform());

            if (tag == null || album == null)
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            var albumTag = new AlbumTag
            {
                TagId = tag.Id,
                AlbumId = album.Id
            };

            album.AlbumTags.Add(albumTag);
            this.context.SaveChanges();

            return $"Tag {tagName} added to {albumName} successfully!";
        }
    }
}

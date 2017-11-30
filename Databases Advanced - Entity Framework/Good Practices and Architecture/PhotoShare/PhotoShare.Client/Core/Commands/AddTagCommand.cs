namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Models;
    using Data;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;
    using Utilities;

    [LogState(Log.Login)]
    public class AddTagCommand : ICommand
    {
        private readonly PhotoShareContext context;

        public AddTagCommand(PhotoShareContext context)
        {
            this.context = context;
        }

        public string Execute(string[] data)
        {
            if (data.Length < 1)
            {
                throw new ArgumentException("Invalid command!");
            }

            var tag = data[0].ValidateOrTransform();

            var tags = this.context.Tags.Select(t => t.Name).ToArray();

            if (tags.Contains(tag))
            {
                throw new ArgumentException($"Tag {tag} exists!");
            }

            this.context.Tags.Add(new Tag
            {
                Name = tag
            });

            this.context.SaveChanges();

            return $"Tag {tag} was added successfully!";
        }
    }
}

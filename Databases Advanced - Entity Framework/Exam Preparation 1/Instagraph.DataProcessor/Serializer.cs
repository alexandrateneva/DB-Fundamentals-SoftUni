using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using Instagraph.Data;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var posts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .OrderBy(p => p.Id)
                .Select(e => new
                {
                    e.Id,
                    User = e.User.Username,
                    Picture = e.Picture.Path
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(posts, Formatting.Indented);

            return jsonString;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var users = context.Users
                .Where(u => u.Posts
                    .Any(p => p.Comments
                        .Any(c => u.Followers
                            .Any(f => f.FollowerId == c.UserId))))
                .OrderBy(u => u.Id)
                .Select(e => new
                {
                    e.Username,
                    Followers = e.Followers.Count
                })
                .ToArray();

            var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);

            return jsonString;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
                .Select(e => new
                {
                    e.Username,
                    MostComments = (e.Posts.Any())
                    ? e.Posts.Select(p => p.Comments.Count).OrderByDescending(c => c).First() : 0
                })
                .OrderByDescending(e => e.MostComments)
                .ThenBy(e => e.Username)
                .ToArray();

            XDocument xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("users",
                from u in users
                select
                new XElement("user",
                    new XElement("Username", u.Username),
                    new XElement("MostComments", u.MostComments)
                )));

            return xmlDoc.ToString();
        }
    }
}

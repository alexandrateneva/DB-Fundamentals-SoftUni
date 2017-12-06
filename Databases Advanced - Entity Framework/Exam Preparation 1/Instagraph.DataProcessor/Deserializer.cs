using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Instagraph.Data;
using Instagraph.Models;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var picturesToAdd = new List<Picture>();
            var result = new StringBuilder();

            Picture[] pictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            foreach (var picture in pictures)
            {
                try
                {
                    if (string.IsNullOrEmpty(picture.Path) || picture.Size <= 0 || picturesToAdd.Any(p => p.Path == picture.Path))
                    {
                        throw new ArgumentException("Error: Invalid data.");
                    }

                    picturesToAdd.Add(picture);
                    result.AppendLine($"Successfully imported Picture {picture.Path}.");
                }
                catch (Exception e)
                {
                    result.AppendLine(e.Message);
                }
            }

            context.Pictures.AddRange(picturesToAdd);

            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var usersToAdd = new List<User>();
            var result = new StringBuilder();

            JObject[] jObjects = JsonConvert.DeserializeObject<JObject[]>(jsonString);

            foreach (JObject jObject in jObjects)
            {
                try
                {
                    if (jObject["ProfilePicture"] == null || jObject["Username"] == null || jObject["Password"] == null)
                    {
                        throw new ArgumentException("Error: Invalid data.");
                    }

                    var picture = context.Pictures.FirstOrDefault(p => p.Path == jObject["ProfilePicture"].ToString());

                    if (picture == null)
                    {
                        throw new ArgumentException("Error: Invalid data.");
                    }

                    var user = new User
                    {
                        Username = jObject["Username"].ToString(),
                        Password = jObject["Password"].ToString(),
                        ProfilePicture = picture
                    };

                    usersToAdd.Add(user);
                    result.AppendLine($"Successfully imported User {user.Username}.");
                }
                catch (Exception e)
                {
                    result.AppendLine(e.Message);
                }
            }


            context.Users.AddRange(usersToAdd);

            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var userFollowersToAdd = new List<UserFollower>();
            var result = new StringBuilder();

            JObject[] jObjects = JsonConvert.DeserializeObject<JObject[]>(jsonString);

            foreach (JObject jObject in jObjects)
            {
                try
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == jObject["User"].ToString());
                    var follower = context.Users.FirstOrDefault(u => u.Username == jObject["Follower"].ToString());

                    if (user == null || follower == null)
                    {
                        throw new ArgumentException("Error: Invalid data.");
                    }

                    if (userFollowersToAdd.Any(uf => uf.User.Id == user.Id && uf.Follower.Id == follower.Id))
                    {
                        throw new ArgumentException("Error: Invalid data.");
                    }

                    var userFollower = new UserFollower
                    {
                        User = user,
                        Follower = follower
                    };

                    userFollowersToAdd.Add(userFollower);
                    result.AppendLine($"Successfully imported Follower {follower.Username} to User {user.Username}.");
                }
                catch (Exception e)
                {
                    result.AppendLine(e.Message);
                }
            }
            context.UsersFollowers.AddRange(userFollowersToAdd);

            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xml = XDocument.Parse(xmlString);

            var root = xml.Root.Elements();

            var postsToAdd = new List<Post>();

            var result = new StringBuilder();

            foreach (var xElement in root)
            {
                try
                {
                    var caption = xElement.Element("caption")?.Value;
                    var username = xElement.Element("user")?.Value;
                    var picturePath = xElement.Element("picture")?.Value;

                    var user = context.Users.FirstOrDefault(u => u.Username == username);
                    var picture = context.Pictures.FirstOrDefault(p => p.Path == picturePath);

                    if (user == null || picture == null || caption == null)
                    {
                        throw new ArgumentException("Error: Invalid data.");
                    }

                    var post = new Post
                    {
                        Caption = caption,
                        User = user,
                        Picture = picture
                    };

                    postsToAdd.Add(post);
                    result.AppendLine($"Successfully imported Post {caption}.");
                }
                catch (Exception e)
                {
                    result.AppendLine(e.Message);
                }
            }

            context.Posts.AddRange(postsToAdd);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xml = XDocument.Parse(xmlString);

            var root = xml.Root.Elements();

            var commentsToAdd = new List<Comment>();

            var result = new StringBuilder();

            foreach (var xElement in root)
            {
                try
                {
                    var content = xElement.Element("content")?.Value;
                    var username = xElement.Element("user")?.Value;
                    var postIdAsStr = xElement.Element("post")?.Attribute("id")?.Value;
                    int.TryParse(postIdAsStr, out int postIdAsInt);

                    var user = context.Users.FirstOrDefault(u => u.Username == username);
                    var post = context.Posts.FirstOrDefault(p => p.Id == postIdAsInt);

                    if (user == null || post == null)
                    {
                        throw new ArgumentException("Error: Invalid data.");
                    }

                    var comment = new Comment
                    {
                        Content = content,
                        Post = post,
                        User = user
                    };

                    commentsToAdd.Add(comment);
                    result.AppendLine($"Successfully imported Comment {content}.");
                }
                catch (Exception e)
                {
                    result.AppendLine(e.Message);
                }
            }

            context.Comments.AddRange(commentsToAdd);
            context.SaveChanges();

            return result.ToString().Trim();
        }
    }
}

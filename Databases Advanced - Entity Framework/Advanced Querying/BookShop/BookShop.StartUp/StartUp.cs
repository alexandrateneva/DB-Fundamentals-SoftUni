namespace BookShop
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Text;
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var bookAgeRestriction = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command, true);

            var books = context.Books
                .Where(b => b.AgeRestriction == bookAgeRestriction)
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:F2}")
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(c => c.ToLower()).ToArray();

            var booksByCategory = context.Books
                .Where(b => b.BookCategories.Select(c => c.Category.Name.ToLower()).Intersect(categories).Any())
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            var result = string.Join(Environment.NewLine, booksByCategory);

            return result;
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType:G} - ${b.Price:F2}")
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(a => a)
                .ToArray();

            var result = string.Join(Environment.NewLine, authors);

            return result;
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Author.LastName.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books
                .Count(b => b.Title.Length > lengthCheck);

            return booksCount;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    BookCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.BookCopies)
                .Select(a => $"{a.Name} - {a.BookCopies}")
                .ToArray();

            var result = string.Join(Environment.NewLine, authors);

            return result;
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .Select(c => $"{c.CategoryName} ${c.TotalProfit:F2}")
                .ToArray();

            var result = string.Join(Environment.NewLine, categories);

            return result;
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    RecentBooks = c.CategoryBooks
                                   .Select(e => new
                                   {
                                       e.Book.Title,
                                       e.Book.ReleaseDate
                                   })
                                   .OrderByDescending(b => b.ReleaseDate).Take(3).ToArray()
                })
                .OrderBy(c => c.Name)
                .ToArray();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");

                foreach (var book in category.RecentBooks)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().Trim();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                 .Where(b => b.ReleaseDate.Value.Year < 2010)
                 .ToList();

            books.ForEach(b => b.Price += 5);

            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4000)
                .ToList();

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return books.Count;
        }
    }
}

namespace _2.Book_Shop
{
    using System;
    using System.Text;

    public class Book
    {
        private string title;
        private string author;
        private decimal price;

        public Book(string author, string title, decimal price)
        {
            this.Title = title;
            this.Author = author;
            this.Price = price;
        }

        protected string Title
        {
            get { return this.title; }
            set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("Title not valid!");
                }
                this.title = value;
            }
        }

        protected string Author
        {
            get { return this.author; }
            set
            {
                var twoNames = value.Split(' ');
                if (twoNames.Length > 1 && char.IsDigit(twoNames[1][0]))
                {
                    throw new ArgumentException("Author not valid!");
                }
                this.author = value;
            }
        }

        protected virtual decimal Price
        {
            get { return this.price; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price not valid!");
                }
                this.price = value;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Type: ").AppendLine(this.GetType().Name)
                .Append("Title: ").AppendLine(this.Title)
                .Append("Author: ").AppendLine(this.Author)
                .Append("Price: ").Append($"{this.Price:F2}")
                .AppendLine();

            return sb.ToString();
        }

    }
}

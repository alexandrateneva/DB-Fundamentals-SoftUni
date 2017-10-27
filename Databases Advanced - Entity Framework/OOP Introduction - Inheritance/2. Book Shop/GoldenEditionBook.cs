namespace _2.Book_Shop
{
    public class GoldenEditionBook : Book
    {
        public GoldenEditionBook(string author, string title, decimal price)
            : base(author, title, price)
        {
        }

        protected override decimal Price
        {
            get
            {
                return base.Price * 1.3m;
            }
        }
    }
}
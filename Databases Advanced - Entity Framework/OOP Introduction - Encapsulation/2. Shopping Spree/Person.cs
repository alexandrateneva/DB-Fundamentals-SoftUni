namespace _2.Shopping_Spree
{
    using System;
    using System.Collections.Generic;

    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> bag;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.bag = new List<Product>();
        }

        public IList<Product> GetProducts()
        {
            return this.bag.AsReadOnly();
        }

        public decimal Money
        {
            get { return this.money; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                this.money = value;
            }
        }

        public string Name
        {
            get { return this.name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                this.name = value;
            }
        }

        public void BuyProduct(Product product)
        {
            if (product.Cost > this.Money)
            {
                throw new ArgumentException($"{this.Name} can't afford {product.Name}");
            }
            this.bag.Add(product);
            this.Money -= product.Cost;
        }
    }
}

﻿namespace _2.Shopping_Spree
{
    using System;

    public class Product
    {
        private string name;
        private decimal cost;

        public Product(string name, decimal cost)
        {
            this.Name = name;
            this.Cost = cost;
        }

        public decimal Cost
        {
            get { return this.cost; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price cannot be zero or negative");
                }
                this.cost = value;
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
    }
}

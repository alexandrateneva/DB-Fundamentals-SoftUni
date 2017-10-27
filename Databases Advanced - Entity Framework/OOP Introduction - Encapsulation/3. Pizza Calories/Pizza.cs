namespace _3.Pizza_Calories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Pizza
    {
        private string name;
        private Dough dough;
        public ICollection<Topping> Toppings { get; }

        public Pizza()
        {
            this.Toppings = new List<Topping>();
        }

        public Pizza(string name) : this()
        {
            this.Name = name;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (value.Length < 1 || value.Length > 15)
                {
                    throw new Exception("Pizza name should be between 1 and 15 symbols.");
                }
                this.name = value;
            }
        }

        public Dough Dough
        {
            get { return this.dough; }
            set { this.dough = value; }
        }

        public void Add(Topping topping)
        {
            if (this.Toppings.Count >= 10)
            {
                throw new Exception("Number of toppings should be in range [0..10].");
            }
            this.Toppings.Add(topping);
        }

        public double TotalCalories()
        {
            return this.dough.Calories() + this.Toppings.Select(t => t.Calories()).Sum();
        }
    }

}
namespace _3.Pizza_Calories
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            Pizza pizza = new Pizza();
            try
            {
                var name = Console.ReadLine().Substring(6);
                pizza.Name = name;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            string type;
            double quantity;
            string[] input;

            try
            {
                input = Console.ReadLine().Split();
                type = input[1].ToLower();
                var technique = input[2].ToLower();
                quantity = Convert.ToDouble(input[3]);
                var dough = new Dough(type, technique, quantity);
                pizza.Dough = dough;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            input = Console.ReadLine().ToLower().Split();

            while (input[0] != "end")
            {
                try
                {
                    type = input[1];
                    quantity = Convert.ToDouble(input[2]);
                    var topping = new Topping(type, quantity);
                    pizza.Add(topping);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                input = Console.ReadLine().ToLower().Split();
            }

            Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories():F2} Calories.");
        }
    }
}


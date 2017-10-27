namespace _2.Shopping_Spree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var people = new List<Person>();
            var products = new List<Product>();


            var peopleInfo = Console.ReadLine().Trim().Split(';');
            var productsInfo = Console.ReadLine().Trim().Split(';');

            try
            {
                for (int i = 0; i < peopleInfo.Length; i++)
                {
                    var personalInfo = peopleInfo[i].Split('=');
                    var name = personalInfo[0];
                    var money = decimal.Parse(personalInfo[1]);

                    var person = new Person(name, money);

                    people.Add(person);
                }

                for (int i = 0; i < productsInfo.Length; i++)
                {
                    var currentProductInfo = productsInfo[i].Split('=');
                    var name = currentProductInfo[0];
                    var cost = decimal.Parse(currentProductInfo[1]);

                    var product = new Product(name, cost);

                    products.Add(product);
                }

                var input = Console.ReadLine().Trim();
                while (input != "END")
                {
                    var args = input.Split(' ');
                    var personName = args[0];
                    var productName = args[1];

                    try
                    {
                        var person = people.FirstOrDefault(p => p.Name == personName);
                        var product = products.FirstOrDefault(p => p.Name == productName);

                        person.BuyProduct(product);
                        Console.WriteLine($"{personName} bought {productName}");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    input = Console.ReadLine().Trim();
                }

                foreach (var person in people)
                {
                    Console.WriteLine(person.GetProducts().Count == 0
                        ? $"{person.Name} - Nothing bought"
                        : $"{person.Name} - {string.Join(", ", person.GetProducts().Select(p => p.Name).ToList())}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

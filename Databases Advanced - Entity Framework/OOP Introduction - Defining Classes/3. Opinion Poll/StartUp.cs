namespace _3.Opinion_Poll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());

            var personsOverAge30 = new List<Person>();

            for (int i = 0; i < n; i++)
            {
                var info = Console.ReadLine().Split(' ');
                var name = info[0];
                var age = int.Parse(info[1]);

                if (age > 30)
                {
                    var person = new Person(name, age);
                    personsOverAge30.Add(person);
                }
            }

            foreach (var person in personsOverAge30.OrderBy(p => p.Name))
            {
                Console.WriteLine(person.Name + " - " + person.Age);
            }
        }
    }
}

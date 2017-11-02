namespace _7.Animals
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            var input = Console.ReadLine().Trim();

            while (input != "Beast!")
            {
                var type = input;
                var info = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int age;
                try
                {
                    if (info.Length != 3 || !int.TryParse(info[1], out age))
                    {
                        throw new Exception();
                    }

                    var name = info[0];
                    var gender = info[2];

                    Type t = Assembly.GetEntryAssembly().GetTypes().First(c => c.Name == type);

                    var animal = (Animal)Activator.CreateInstance(t, name, age, gender);

                    Console.WriteLine(type);
                    Console.WriteLine(animal);

                    MethodInfo method
                        = t.GetMethod("ProduceSound", BindingFlags.Instance | BindingFlags.Public);

                    method.Invoke(animal, new object[0]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input!");
                }

                input = Console.ReadLine().Trim();
            }
        }
    }
}


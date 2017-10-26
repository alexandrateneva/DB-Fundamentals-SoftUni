namespace _1.Define_a_class_Person
{
    using System;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            Type personType = typeof(Person);
            PropertyInfo[] properties = personType.GetProperties
                (BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine(properties.Length);

            Person firstPerson = new Person("Pesho", 20);
            Person secondPerson = new Person("Gosho", 17);
            Person thirdPerson = new Person("Stamat", 43);
        }
    }
}

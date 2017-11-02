namespace _7.Animals
{
    using System;

    public class Dog : Animal
    {
        public const string DogSound = "Woof!";

        public Dog(string name, int age, string gender) : base(name, age, gender)
        {
        }

        public override void ProduceSound()
        {
            Console.WriteLine(DogSound);
        }
    }
}

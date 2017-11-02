namespace _7.Animals
{
    using System;

    public class Kitten : Cat
    {
        public const string KittenSound = "Meow";

        public Kitten(string name, int age, string gender) : base(name, age, gender)
        {
            this.Gender = "Female";
        }

        public override void ProduceSound()
        {
            Console.WriteLine(KittenSound);
        }
    }
}

namespace _7.Animals
{
    using System;

    public class Frog : Animal
    {
        public const string FrogSound = "Ribbit";

        public Frog(string name, int age, string gender) : base(name, age, gender)
        {
        }

        public override void ProduceSound()
        {
            Console.WriteLine(FrogSound);
        }
    }
}

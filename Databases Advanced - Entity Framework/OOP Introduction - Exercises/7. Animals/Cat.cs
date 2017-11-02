namespace _7.Animals
{
    using System;

    public class Cat : Animal
    {
        public const string CatSound = "Meow meow";

        public Cat(string name, int age, string gender) : base(name, age, gender)
        {
        }

        public override void ProduceSound()
        {
            Console.WriteLine(CatSound);
        }
    }
}

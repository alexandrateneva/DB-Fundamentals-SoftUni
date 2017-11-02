namespace _7.Animals
{
    using System;

    public class Tomcat : Cat
    {
        public const string TomcatSound = "MEOW";

        public Tomcat(string name, int age, string gender) : base(name, age, gender)
        {
            this.Gender = "Male";
        }

        public override void ProduceSound()
        {
            Console.WriteLine(TomcatSound);
        }
    }
}

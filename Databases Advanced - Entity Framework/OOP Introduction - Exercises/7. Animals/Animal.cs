namespace _7.Animals
{
    using System;

    public abstract class Animal : ISoundProducable
    {
        private string name;
        private int age;
        private string gender;

        protected Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }
        
        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException();
                }
                this.name = value;
            }
        }

        public int Age
        {
            get { return this.age; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }
                this.age = value;
            }
        }

        public string Gender
        {
            get { return this.gender; }
            set
            {
                if (value != "Male" & value != "Female")
                {
                    throw new ArgumentException("Gender must be Female or Male!");
                }
                this.gender = value;
            }
        }

        public abstract void ProduceSound();

        public override string ToString()
        {
            return $"{this.Name} {this.Age} {this.Gender}";
        }
    }
}

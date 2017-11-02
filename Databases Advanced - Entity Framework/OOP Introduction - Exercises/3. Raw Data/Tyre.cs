namespace _3.Raw_Data
{
    public class Tyre
    {
        public int Age { get; set; }
        public double Presure { get; set; }

        public Tyre(double presure, int age)
        {
            this.Age = age;
            this.Presure = presure;
        }
    }
}
namespace _3.Raw_Data
{
    public class Engine
    {
        public int Power { get; set; }
        public int Speed { get; set; }

        public Engine(int power, int speed)
        {
            this.Power = power;
            this.Speed = speed;
        }
    }
}
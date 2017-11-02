namespace _7.Animals
{
    public interface ISoundProducable
    {
        string Name { get; }
        int Age { get; }
        string Gender { get; set; }

        void ProduceSound();
    }
}

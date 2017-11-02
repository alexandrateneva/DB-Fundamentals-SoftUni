namespace _3.Raw_Data
{
    using System.Collections.Generic;

    public class Car
    {
        public string Model { get; set; }
        public Cargo Cargo { get; set; }
        public Engine Engine { get; set; }
        public List<Tyre> Tyres { get; set; }

        public Car(string model, Cargo cargo, Engine engine, List<Tyre> tyres)
        {
            this.Model = model;
            this.Cargo = cargo;
            this.Engine = engine;
            this.Tyres = tyres;
        }
    }
}

namespace _4.Speed_Racing
{
    using System;

    public class Car
    {
        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumptionFor1km { get; set; }
        public double DistanceTraveled { get; set; }

        public Car(string model, double fuelAmount, double fuelConsumptionFor1Km)
        {
            this.Model = model;
            this.FuelAmount = fuelAmount;
            this.FuelConsumptionFor1km = fuelConsumptionFor1Km;
            this.DistanceTraveled = 0;
        }

        public void Move(double amountOfKm)
        {
            if (amountOfKm * this.FuelConsumptionFor1km > this.FuelAmount )
            {
                throw new ArgumentException("Insufficient fuel for the drive");
            }

            this.FuelAmount -= amountOfKm * this.FuelConsumptionFor1km;
            this.DistanceTraveled += amountOfKm;
        }

        public override string ToString()
        {
            return $"{this.Model} {this.FuelAmount:F2} {this.DistanceTraveled:F0}";
        }
    }
}

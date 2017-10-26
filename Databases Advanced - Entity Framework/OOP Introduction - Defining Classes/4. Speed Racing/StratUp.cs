namespace _4.Speed_Racing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StratUp
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());

            var cars = new List<Car>();

            for (int i = 0; i < n; i++)
            {
                var info = Console.ReadLine().Split(' ');
                var model = info[0];
                var fuelAmount = double.Parse(info[1]);
                var fuelConsumptionFor1Km = double.Parse(info[2]);

                var car = new Car(model, fuelAmount, fuelConsumptionFor1Km);
                cars.Add(car);
            }

            var input = Console.ReadLine();
            while (input != "End")
            {
                try
                {
                    var args = input.Split(' ');
                    var currentModel = args[1];
                    var currentDistance = double.Parse(args[2]);
                    Car currentCar = cars.FirstOrDefault(c => c.Model == currentModel);
                    currentCar.Move(currentDistance);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                input = Console.ReadLine();
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}

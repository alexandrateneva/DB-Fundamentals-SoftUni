namespace _3.Pizza_Calories
{
    using System;

    public class Dough
    {
        private const double FlourTypeWhite = 1.5;
        private const double FlourTypeWholegrain = 1;
        private const double BakingTechniqueCrispy = 0.9;
        private const double BakingTechniqueChewy = 1.1;
        private const double BakingTechniqueHomemade = 1;
        private const double CommonCalories = 2;

        private string flourType;
        private string brakingTechnique;
        private double weight;

        public Dough(string flourType, string brakingTechnique, double weight)
        {
            this.FlourType = flourType;
            this.BrakingTechnique = brakingTechnique;
            this.Weight = weight;
        }

        public string FlourType
        {
            get { return this.flourType; }

            private set
            {
                if (value != "white" && value != "wholegrain")
                {
                    throw new Exception("Invalid type of dough.");
                }

                this.flourType = value;
            }
        }

        public string BrakingTechnique
        {
            get { return this.brakingTechnique; }

            private set
            {
                if (value != "crispy" && value != "chewy" && value != "homemade")
                {
                    throw new Exception("Invalid type of dough.");
                }

                this.brakingTechnique = value;
            }
        }

        public double Weight
        {
            get { return this.weight; }

            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new Exception("Dough weight should be in the range [1..200].");
                }

                this.weight = value;
            }
        }

        public double Calories()
        {
            return (CommonCalories * this.weight) * FlourTypeCalories() * BrakingTechniqueCalories();
        }

        private double FlourTypeCalories()
        {
            if (this.flourType == "white")
            {
                return FlourTypeWhite;
            }
            return FlourTypeWholegrain;
        }

        private double BrakingTechniqueCalories()
        {
            if (this.brakingTechnique == "crispy")
            {
                return BakingTechniqueCrispy;
            }
            if (this.brakingTechnique == "chewy")
            {
                return BakingTechniqueChewy;
            }
            return BakingTechniqueHomemade;
        }
    }
}

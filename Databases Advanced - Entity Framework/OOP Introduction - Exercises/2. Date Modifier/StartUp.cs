namespace _2.Date_Modifier
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var firstDate = DateTime.ParseExact(Console.ReadLine(), "yyyy MM dd", null);
            var lastDate = DateTime.ParseExact(Console.ReadLine(), "yyyy MM dd", null);

            var dateModifier = new DateModifier(firstDate, lastDate);
            Console.WriteLine(dateModifier.GetDifference());
        }
    }
}

namespace _2.Date_Modifier
{
    using System;

    public class DateModifier
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateModifier(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public int GetDifference()
        {
            return Math.Abs((int)(this.EndDate - this.StartDate).TotalDays);
        }
    }
}

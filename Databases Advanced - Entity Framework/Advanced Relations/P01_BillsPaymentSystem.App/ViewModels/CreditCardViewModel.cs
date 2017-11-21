namespace P01_BillsPaymentSystem.App.ViewModels
{
    using System;
    using System.Globalization;
    using System.Text;
    using P01_BillsPaymentSystem.Data.Models;

    public class CreditCardViewModel
    {
        public CreditCardViewModel(CreditCard creditCard)
        {
            this.Id = creditCard.CreditCardId;
            this.Limit = creditCard.Limit;
            this.MoneyOwed = creditCard.MoneyOwed;
            this.LimitLeft = creditCard.LimitLeft;
            this.ExpirationDate = creditCard.ExpirationDate;
        }

        public int Id { get; private set; }

        public decimal Limit { get; private set; }

        public decimal MoneyOwed { get; private set; }

        public decimal LimitLeft { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"-- ID: {this.Id}");
            sb.AppendLine($"--- Limit: {this.Limit:F2}");
            sb.AppendLine($"--- Money Owed: {this.MoneyOwed:F2}");
            sb.AppendLine($"--- Limit Left:: {this.LimitLeft:F2}");
            sb.AppendLine(
                $"--- Expiration Date: {this.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");

            return sb.ToString().Trim();
        }
    }
}

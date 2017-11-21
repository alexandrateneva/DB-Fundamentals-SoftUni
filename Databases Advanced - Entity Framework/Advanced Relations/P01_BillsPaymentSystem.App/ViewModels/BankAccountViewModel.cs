namespace P01_BillsPaymentSystem.App.ViewModels
{
    using System.Text;
    using P01_BillsPaymentSystem.Data.Models;

    public class BankAccountViewModel
    {
        public BankAccountViewModel(BankAccount bankAccount)
        {
            this.Id = bankAccount.BankAccountId;
            this.Balance = bankAccount.Balance;
            this.BankName = bankAccount.BankName;
            this.SwiftCode = bankAccount.SwiftCode;
        }

        public int Id { get; private set; }

        public decimal Balance { get; private set; }

        public string BankName { get; private set; }

        public string SwiftCode { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"-- ID: { this.Id}");
            sb.AppendLine($"--- Balance: {this.Balance:F2}");
            sb.AppendLine($"--- Bank: {this.BankName}");
            sb.AppendLine($"--- SWIFT: {this.SwiftCode}");

            return sb.ToString().Trim();
        }
    }
}

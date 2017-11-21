namespace P01_BillsPaymentSystem.Data.Models
{
    using System;

    public class BankAccount : IPaymentMethod
    {
        public int BankAccountId { get; set; }

        public decimal Balance { get; private set; }

        public string BankName { get; set; }

        public string SwiftCode { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }
            if (amount > this.Balance)
            {
                throw new ArgumentException("Insufficient funds!");
            }
            this.Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }
            this.Balance += amount;
        }

        public decimal GetAvailableFunds()
        {
            return this.Balance;
        }
    }
}

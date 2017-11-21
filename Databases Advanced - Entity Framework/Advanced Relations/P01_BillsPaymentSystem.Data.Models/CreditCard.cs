namespace P01_BillsPaymentSystem.Data.Models
{
    using System;

    public class CreditCard : IPaymentMethod
    {
        public int CreditCardId { get; set; }

        public decimal Limit { get; set; }

        public decimal MoneyOwed { get; private set; }

        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        public DateTime ExpirationDate { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }
            if (amount > this.LimitLeft)
            {
                throw new ArgumentException("Insufficient funds!");
            }
            this.MoneyOwed += amount;
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should not be negative.");
            }

            if (amount <= this.MoneyOwed)
            {
                this.MoneyOwed += amount;
            }
            else
            {
                this.Limit += amount - this.MoneyOwed;
                this.MoneyOwed = 0;
            }
        }

        public decimal GetAvailableFunds()
        {
            return this.LimitLeft;
        }
    }
}

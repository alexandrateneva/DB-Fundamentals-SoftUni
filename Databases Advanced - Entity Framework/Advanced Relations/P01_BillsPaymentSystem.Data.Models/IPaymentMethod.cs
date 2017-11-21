namespace P01_BillsPaymentSystem.Data.Models
{
    public interface IPaymentMethod
    {
        void Withdraw(decimal amount);

        void Deposit(decimal amount);

        decimal GetAvailableFunds();
    }
}

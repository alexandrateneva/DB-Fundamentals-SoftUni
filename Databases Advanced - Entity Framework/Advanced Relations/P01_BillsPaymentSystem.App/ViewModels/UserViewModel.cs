namespace P01_BillsPaymentSystem.App.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using P01_BillsPaymentSystem.Data.Models;

    public class UserViewModel
    {
        public UserViewModel(User user)
        { 
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.BankAccounts = user.PaymentMethods
                .Where(pm => pm.Type == PaymentMethodType.BankAccount)
                .Select(p => p.BankAccount)
                .OrderBy(ba => ba.BankAccountId)
                .ToList();
            this.CreditCards = user.PaymentMethods
                .Where(pm => pm.Type == PaymentMethodType.CreditCard)
                .Select(p => p.CreditCard)
                .OrderBy(cc => cc.CreditCardId)
                .ToList();
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public ICollection<BankAccount> BankAccounts { get; private set; } 

        public ICollection<CreditCard> CreditCards { get; private set; } 

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"User: {this.FirstName} {this.LastName}");

            if (this.BankAccounts.Any())
            {
                sb.AppendLine("Bank Accounts:");

                foreach (var bankAccount in this.BankAccounts)
                {
                    sb.AppendLine(new BankAccountViewModel(bankAccount).ToString());
                }
            }
            else
            {
                sb.AppendLine("There are no bank accounts.");
            }

            if (this.CreditCards.Any())
            {
                sb.AppendLine("Credit Cards:");

                foreach (var creditCard in this.CreditCards)
                {
                    sb.AppendLine(new CreditCardViewModel(creditCard).ToString());
                }
            }
            else
            {
                sb.AppendLine("There are no credit cards.");
            }
            
            return sb.ToString().Trim();
        }
    }
}

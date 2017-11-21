namespace P01_BillsPaymentSystem.App
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;

    public class DataGenerator
    {
        public static void ResetDatabase(BillsPaymentSystemContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Seed(context);
        }

        public static void Seed(BillsPaymentSystemContext context)
        {
            var users = new[]
            {
                new User
                {
                    FirstName = "Pesho", LastName = "Ivanov", Email = "pesho123@abv.bg", Password = "peshoEgotin"
                },
                new User
                {
                    FirstName = "Ivan", LastName = "Georgiev", Email = "ivan007@abv.bg", Password = "ivan123"
                },
                new User
                {
                    FirstName = "Maria", LastName = "Atanasova", Email = "mimi1987@gmail.bg", Password = "mimi_sunshine"
                }
            };

            context.Users.AddRange(users);

            var bankAccounts = new[]
            {
                new BankAccount()
                {
                    BankName = "Bulbank", SwiftCode = "UNCRBGSF"
                },
                new BankAccount()
                {
                    BankName = "Fibank", SwiftCode = "FINVBGSF"
                },
                new BankAccount()
                {
                    BankName = "Postbank", SwiftCode = "BPBIBGSF"
                }
            };

            bankAccounts[0].Deposit(15000m);
            bankAccounts[1].Deposit(50000m);
            bankAccounts[2].Deposit(78000m);
            
            context.BankAccounts.AddRange(bankAccounts);

            var creditCards = new[]
            {
                new CreditCard()
                {
                    Limit = 5000m, ExpirationDate = DateTime.Parse("15.12.2019")
                },
                new CreditCard()
                {
                    Limit = 2000m, ExpirationDate = DateTime.Parse("15.01.2020")
                },
                new CreditCard()
                {
                    Limit = 1000m, ExpirationDate = DateTime.Parse("31.12.2018")
                }
            };

            creditCards[0].Deposit(1000m);
            creditCards[1].Deposit(500m);
            creditCards[2].Deposit(250m);
            
            context.CreditCards.AddRange(creditCards);

            var paymentMethods = new[]
            {
                new PaymentMethod()
                {
                    UserId = users[0].UserId, Type = PaymentMethodType.BankAccount, BankAccountId = bankAccounts[0].BankAccountId
                },
                new PaymentMethod()
                {
                    UserId = users[0].UserId, Type = PaymentMethodType.BankAccount, BankAccountId = bankAccounts[2].BankAccountId
                },
                new PaymentMethod()
                {
                    UserId = users[1].UserId, Type = PaymentMethodType.CreditCard, CreditCardId = creditCards[1].CreditCardId
                },
                new PaymentMethod()
                {
                    UserId = users[2].UserId, Type = PaymentMethodType.BankAccount, BankAccountId = bankAccounts[1].BankAccountId
                },
                new PaymentMethod()
                {
                    UserId = users[2].UserId, Type = PaymentMethodType.CreditCard, CreditCardId = creditCards[2].CreditCardId
                },
                new PaymentMethod()
                {
                UserId = users[2].UserId, Type = PaymentMethodType.CreditCard, CreditCardId = creditCards[0].CreditCardId
                }
            };

            context.PaymentMethods.AddRange(paymentMethods);

            context.SaveChanges();
        }
    }
}
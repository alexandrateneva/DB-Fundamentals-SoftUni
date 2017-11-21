namespace P01_BillsPaymentSystem.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using P01_BillsPaymentSystem.App.ViewModels;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            var context = new BillsPaymentSystemContext();

            DataGenerator.ResetDatabase(context);

            EnterUserId:  Console.Write("Please, enter user Id: ");
            var userId = int.Parse(Console.ReadLine().Trim());

            var user = context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                Console.WriteLine($"User with id {userId} not found!");
                goto EnterUserId;
            }

            EnterCommandLetter:  Console.Write("You want to see details(D) or want to pay bills(B)? Please, enter the appropriate letter: ");
            var commandLetter = Console.ReadLine().Trim();

            var userViewModel = new UserViewModel(user);
            switch (commandLetter)
            {
                case "D":
                    Console.WriteLine(userViewModel);
                    break;
                case "B":
                    EnterAmount: Console.Write("Please, enter the amount: ");
                    var amount = decimal.Parse(Console.ReadLine().Trim());
                    if (amount <= 0)
                    {
                        Console.WriteLine("Amount should not be negative or zero.");
                        goto EnterAmount;
                    }
                    PayBill(userViewModel, amount);
                    context.SaveChanges();
                    Console.WriteLine("Bills have been successfully paid.");
                    break;
                default:
                    Console.WriteLine("You have entered wrong letter.");
                    goto EnterCommandLetter;
            }
        }

        public static void PayBill(UserViewModel userViewModel, decimal amount)
        {
            var paymentMethodsOfUser = new List<IPaymentMethod>();
            paymentMethodsOfUser.AddRange(userViewModel.BankAccounts);
            paymentMethodsOfUser.AddRange(userViewModel.CreditCards);

            var totalSum = paymentMethodsOfUser.Sum(pm => pm.GetAvailableFunds());

            if (totalSum < amount)
            {
                Console.WriteLine("Insufficient funds!");
                Environment.Exit(0);
            }

            while (true)
            {
                foreach (var paymentMethod in paymentMethodsOfUser)
                {
                    var currentSum = paymentMethod.GetAvailableFunds();

                    paymentMethod.Withdraw(currentSum >= amount ? amount : currentSum);

                    amount -= currentSum;
                    if (amount < 0)
                    {
                        return;
                    }
                }
            }
        }
    }
}

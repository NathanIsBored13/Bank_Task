using System;
using System.Linq;

namespace OOP_task
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader(string.Format("{0}\\Accounts", Environment.CurrentDirectory));
            Bank bank = new Bank(reader);
            string input;
            bool Exit = false;
            do
            {
                Console.Write("Welcome to the bank,What do you want to do?\n[R]egistar an account, [L]og on as an existing user or [E]xit:");
                input = Console.ReadLine().ToLower();
                if (input == "r") bank.Make_User(reader);
                else if (input == "l") Exit = bank.Login();
                else if (input != "e") Console.WriteLine("Please only enter 'r', 'l, or 'e'");
            } while (!new string[] { "r", "l", "e" }.Contains(input) || (!Exit && input != "e"));
        }
    }
}

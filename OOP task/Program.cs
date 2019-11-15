using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_task
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            string input;
            bool Exit = false;
            Console.WriteLine("No users exist, make one");
            bank.Make_Account();
            do
            {
                Console.Write("Welcome to the bank,What do you want to do?\n[1]Registar an account [2] Log into an existing account:");
                input = Console.ReadLine();
                if (input == "1") bank.Make_Account();
                else if (input == "2") Exit = bank.Login();
                else Console.WriteLine("Please only enter 1 or 2\n");
            } while (!new string[2] { "1", "2" }.Contains(input) || !Exit);
        }
    }
}

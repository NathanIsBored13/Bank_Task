using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_task
{
    class Program
    {
        static void Main(string[] args)
        {
            List <User> Accounts = new List<User>();
            string input;
            do
            {
                Console.Write("Welcome to the bank,What do you want to do?/n[1]Registar an account [2] Log into an existing account:");
                input = Console.ReadLine();
                if (input == "1") MakeAccount();
                else if (input == "2") Login(Accounts).Logged_In();
                else Console.WriteLine("Please only enter 1 or 2\n");
            } while (!new string[2] { "1", "2" }.Contains(input));
        }
        static void MakeAccount()
        {
            string Username, Password = "", input;
            do
            {
                Console.Write("Chose a username:");
                Username = Console.ReadLine();
                do
                {
                    Console.Write("\nenter a strong password: ");
                    input = Console.ReadLine();
                    if (input == "") Console.WriteLine("please enter a password");
                    else if (!input.Any(char.IsLower) || !input.Any(char.IsUpper) || !input.Any(char.IsDigit) || !input.Any(ch => !char.IsLetterOrDigit(ch)) || input.Length < 6)
                    {
                        Console.WriteLine("\npassword is too weak, it must meet all the folowing criteria:");
                        Console.WriteLine("must contain a lower case charecter: {0}", input.Any(char.IsLower) ? "True" : "False");
                        Console.WriteLine("must contain a upper case charecter: {0}", input.Any(char.IsUpper) ? "True" : "False");
                        Console.WriteLine("must contain a number:  {0}", input.Any(char.IsDigit) ? "True" : "False");
                        Console.WriteLine("must contain a special: charecter {0}", input.Any(ch => !char.IsLetterOrDigit(ch)) ? "True" : "False");
                        Console.WriteLine("must be at least 6 charecters: {0}", input.Length >= 6 ? "True" : "False");
                    }
                    else
                    {
                        Password = input;
                        Console.Write("\nre-enter your password: ");
                        input = Console.ReadLine();
                        if (input != Password) Console.WriteLine("\npasswords do not match, try again");
                    }
                } while (Password != input);
                Console.Write("Is this data correct: Username = {0}, Password = {1}?\nPlease enter [y]es or [n]o");
                input = Console.ReadLine();
                if (!new string[] { "y", "n" }.Contains(input.ToLower())) Console.WriteLine("Please only enter y or n");
            } while (input != "y");
        }
        static User Login(List<User> Accounts)
        {
            string input;
            int index = -1;
            do
            {
                Console.Write("Enter your account name:");
                input = Console.ReadLine();
                for (int i = 0; i < Accounts.Count; i++)
                {
                    if (Accounts[i].Get_Account_Name() == input) index = i;
                }
                if (index == -1) Console.WriteLine("This Bank Account is not registared, try again\n");
            } while (index == -1);
            do
            {
                Console.Write("Enter the password for {0}:", Accounts[index].Get_Account_Name());
                input = Console.ReadLine();
                if (!Accounts[index].Check_Password(input)) Console.WriteLine("That is not the password for this account\n");
            } while (!Accounts[index].Check_Password(input));
            return Accounts[index];
        }
    }
}

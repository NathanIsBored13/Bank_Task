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
                else if (input == "2") Menue(Login(Accounts));
                else Console.WriteLine("Please only enter 1 or 2\n");
            } while (!new string[2] { "1", "2" }.Contains(input));
        }
        static void MakeAccount()
        {

        }
        static User Login(List<User> Accounts)
        {
            string input;
            int index = -1;
            do
            {
                Console.Write("Enter your account name:");
                input = Console.ReadLine();
                if (Accounts.Any(x => input == x.Get_Account_Name())) index = Accounts.FindIndex(x => input == x.Get_Account_Name());
                else Console.WriteLine("This Bank Account is not registared, try again\n");
            } while (index == -1);
            do
            {
                Console.Write("Enter the password for {0}:", Accounts[index].Get_Account_Name());
                input = Console.ReadLine();
                if (!Accounts[index].Check_Password(input)) Console.WriteLine("That is not the password for this account\n");
            } while (!Accounts[index].Check_Password(input));
            return Accounts[index];
        }
        static void Menue(User Active_User)
        {
            Console.WriteLine("You are looged in as {0}, You have the accounts:\n{1}\nWhich one do you wish to operate with", Active_User.Get_Account_Name(), string.Join(",", Active_User.Get_Accounts()));
        }
    }
}

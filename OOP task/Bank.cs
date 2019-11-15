using System;
using System.Collections.Generic;
using System.Linq;
public class Bank
{
    private List<User> Accounts;
    public Bank()
	{
        Accounts = new List<User>();
	}
    public void Make_Account()
    {
        string Username = null, Holder_Name = null, Password = null, input;
        do
        {
            Console.Write("\nPlese enter your full name: ");
            Holder_Name = Console.ReadLine();
            do
            {
                Console.Write("\nChose a username: ");
                input = Console.ReadLine();
                if (!Accounts.Any(x => x.Get_Username() == input)) Username = input;
                else Console.WriteLine("This username already exists, please pick a different one");
            } while (Username == null);
            do
            {
                Console.Write("\nEnter a strong password: ");
                input = Console.ReadLine();
                if (!input.Any(char.IsLower) || !input.Any(char.IsUpper) || !input.Any(char.IsDigit) || !input.Any(ch => !char.IsLetterOrDigit(ch)) || input.Length < 6)
                {
                    Console.WriteLine("\nPassword is too weak, it must meet all the folowing criteria:");
                    Console.WriteLine("Must contain a lower case charecter: {0}", input.Any(char.IsLower) ? "True" : "False");
                    Console.WriteLine("Must contain a upper case charecter: {0}", input.Any(char.IsUpper) ? "True" : "False");
                    Console.WriteLine("Must contain a number:  {0}", input.Any(char.IsDigit) ? "True" : "False");
                    Console.WriteLine("Must contain a special: charecter {0}", input.Any(ch => !char.IsLetterOrDigit(ch)) ? "True" : "False");
                    Console.WriteLine("Must be at least 6 charecters: {0}", input.Length >= 6 ? "True" : "False");
                }
                else
                {
                    Password = input;
                    Console.Write("\nRe-enter your password: ");
                    input = Console.ReadLine();
                    if (input != Password) Console.WriteLine("Passwords do not match, try again");
                }
            } while (Password != input);
            Console.Write("\nPlease confirm this data:\nYour name = {0},\nUsername = {1},\n[Y/N]: ", Holder_Name, Username);
            input = Console.ReadLine().ToLower();
            if (!new string[] { "y", "n" }.Contains(input)) Console.WriteLine("Please only enter 'y' or 'n'");
        } while (input != "y");
        Accounts.Add(new User(Username, Holder_Name, Password));
        Console.WriteLine("\nAccount {0} created sucsessfuly\n", Username);
    }
    public bool Login()
    {
        string input;
        int index = -1;
        do
        {
            Console.Write("\nEnter your username name:");
            input = Console.ReadLine();
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Get_Username() == input) index = i;
            }
            if (index == -1) Console.WriteLine("This Bank Account is not registared, try again");
        } while (index == -1);
        do
        {
            Console.Write("\nHello {0}, please enter your Password:", Accounts[index].Get_Holder_Name());
            input = Console.ReadLine();
            if (!Accounts[index].Check_Password(input)) Console.WriteLine("That is not the password for this account");
        } while (!Accounts[index].Check_Password(input));
        Accounts[index].Selected();
        do
        {
            Console.Write("\nDo you wish to [L]ogin as another user or [E]xit: ");
            input = Console.ReadLine().ToLower();
            if (!new string[] { "l", "e" }.Contains(input)) Console.Write("Please only enter 'l' or 'e'");
        } while (!new string[] { "l", "e" }.Contains(input));
        return input == "e";
    }
}

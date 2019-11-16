using System;
using System.Collections.Generic;
using System.Linq;
public class Bank
{
    private List<User> users;
    public Bank(Reader reader)
	{
        users = new List<User>();
        foreach (string path in reader.Get_Contents()) users.Add(new User(reader.ReadFile(path), reader));
        if (users.Count() == 0)
        {
            Console.WriteLine("No users exist, Please create one");
            Make_User(reader);
        }
	}
    public void Make_User(Reader reader)
    {
        string username = null, holder_name = null, password = null, input;
        do
        {
            Console.Write("\nPlese enter your full name: ");
            holder_name = Console.ReadLine();
            do
            {
                Console.Write("\nChose a username: ");
                input = Console.ReadLine();
                if (!users.Any(x => x.Get_Username() == input)) username = input;
                else Console.WriteLine("This username already exists, please pick a different one");
            } while (username == null);
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
                    password = input;
                    Console.Write("\nRe-enter your password: ");
                    input = Console.ReadLine();
                    if (input != password) Console.WriteLine("Passwords do not match, try again");
                }
            } while (password != input);
            Console.Write("\nPlease confirm this data:\nYour name = {0},\nUsername = {1},\n[Y/N]: ", holder_name, username);
            input = Console.ReadLine().ToLower();
            if (!new string[] { "y", "n" }.Contains(input)) Console.WriteLine("Please only enter 'y' or 'n'");
        } while (input != "y");
        users.Add(new User(username, holder_name, password, reader));
        Console.WriteLine("\nAccount {0} created sucsessfuly\n", username);
    }
    public bool Login()
    {
        string input;
        int index = -1;
        do
        {
            Console.Write("\nEnter your username name:");
            input = Console.ReadLine();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Get_Username() == input) index = i;
            }
            if (index == -1) Console.WriteLine("This Bank Account is not registared, try again");
        } while (index == -1);
        do
        {
            Console.Write("\nHello {0}, please enter your Password:", users[index].Get_Holder_Name());
            input = Console.ReadLine();
            if (!users[index].Check_Password(input)) Console.WriteLine("That is not the password for this account");
        } while (!users[index].Check_Password(input));
        users[index].Selected();
        do
        {
            Console.Write("\nDo you wish to [L]ogin as another user or [E]xit: ");
            input = Console.ReadLine().ToLower();
            if (input == "l") Console.WriteLine();
            else if (input != "e") Console.Write("Please only enter 'l' or 'e'");
        } while (!new string[] { "l", "e" }.Contains(input));
        return input == "e";
    }
}

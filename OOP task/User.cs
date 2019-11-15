using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

public class User
{
    private string Username, Holder_Name;
    private int Salt;
    private string Password_Hash;
    List<Bank_Account> Accounts;
    public User(string Username, string Holder_Name, string Password)
	{
        Random random = new Random();
        this.Username = Username;
        this.Holder_Name = Holder_Name;
        Salt = random.Next(0, 100);
        Password_Hash = Hash(Password, Salt);
        Accounts = new List<Bank_Account>();
	}
    public string Get_Username() => Username;
    public string Get_Holder_Name() => Holder_Name;
    public bool Check_Password(string Password) => Password_Hash == Hash(Password, Salt);
    public void Selected()
    {
        string input = null;
        Console.WriteLine("\nYou are logged in as {0},", Username);
        do
        {
            if (Accounts.Count == 0) Console.Write("\nYou have no active accounts, do you want to [M]ake an account or [L]ogout: ");
            else Console.WriteLine("\nYou have the following accounts:\n{0}\nEnter the neme of the account you wish to select or [M]ake a new account or [L]ogout", string.Join(",\n", Get_Account_Names()));
            input = Console.ReadLine();
            if (input.ToLower() == "m") Make_New_Account();
            else if (Accounts.Any(x => x.Get_Name() == input)) Accounts.First(x => x.Get_Name() == input).Select();
            else Console.Write("plese only enter 'm', 'l' or the name of one of your accounts\n");
        } while (input.ToLower() != "l");
    }
    public void Make_New_Account()
    {
        string Account_Name = null, Startup_Funds, input;
        do
        {
            Console.WriteLine("\nEnter a name for this account");
            input = Console.ReadLine();
            if (Accounts.All(x => x.Get_Name() != input)) Account_Name = input;
            else Console.WriteLine("This account name already exists, please pick a diffrent name.");
        } while (Account_Name == null);
        Console.WriteLine("\nEnter the amount of money you wish to open this account with in GBP, the minimum is £5:\n5 - 99: 0.75%{0}\n100 - 499: 1.5%{0}\n500-4999: 2%{0}\n5000+: 2.5%{0}", " intrest per annum");
        Startup_Funds = Console.ReadLine();
        Accounts.Add(new Bank_Account(Account_Name, Convert.ToInt32(Startup_Funds)));
    }
    private List<string> Get_Account_Names()
    {
        List<string> ret = new List<string>();
        foreach (Bank_Account Account in Accounts) ret.Add(Account.Get_Name());
        return ret;
    }
    private string Hash(string data, int salt)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
            for (int i = 0; i < bytes.Length; i++) bytes[i] += (byte)salt;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
            return builder.ToString();
        }
    }
}

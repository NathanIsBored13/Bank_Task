using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
public class User
{
    private string username, holder_name;
    private int salt;
    private string password_hash;
    Reader reader;
    Bank parent;
    List<Bank_Account> accounts;
    public User(string username, string holder_name, string password, Reader reader, Bank parent)
    {
        accounts = new List<Bank_Account>();
        Random random = new Random();
        this.username = username;
        this.holder_name = holder_name;
        salt = random.Next(0, 100);
        password_hash = Hash(password, salt);
        this.reader = reader;
        this.parent = parent;
        Write_All();
	}
    public User (string[] file, Reader reader, Bank parent)
    {
        accounts = new List<Bank_Account>();
        username = file[0];
        holder_name = file[1];
        salt = Convert.ToInt32(file[2]);
        password_hash = file[3];
        this.reader = reader;
        this.parent = parent;
        for (int i = 4; i < file.Length; i++)
        {
            string[] buffer = file[i].Split(", ");
            accounts.Add(new Bank_Account(buffer[0], double.Parse(buffer[1]), double.Parse(buffer[2]), this));
        }
    }
    public string Get_Username() => username;
    public string Get_Holder_Name() => holder_name;
    public bool Check_Password(string Password) => password_hash == Hash(Password, salt);
    public void Selected()
    {
        string input = null;
        Console.WriteLine("\nYou are logged in as {0},", username);
        do
        {
            if (accounts.Count == 0) Console.Write("\nYou have no active accounts, do you want to [M]ake an account or [L]ogout: ");
            else Console.WriteLine("\nYou have the following accounts:\n{0}\nEnter the neme of the account you wish to select or [M]ake a new account or [L]ogout", string.Join(",\n", Get_Account_Names()));
            input = Console.ReadLine();
            if (input.ToLower() == "m") Make_New_Account();
            else if (accounts.Any(x => x.Get_Name() == input))
            {
                accounts.First(x => x.Get_Name() == input).Select();
                Write_All();
            }
            else Console.Write("plese only enter 'm', 'l' or the name of one of your accounts\n");
        } while (input.ToLower() != "l");
    }
    public void Make_New_Account()
    {
        string account_name = null, input;
        double startup_funds = -1;
        do
        {
            Console.WriteLine("\nEnter a name for this account");
            input = Console.ReadLine();
            if (accounts.All(x => x.Get_Name() != input)) account_name = input;
            else Console.WriteLine("This account name already exists, please pick a diffrent name.");
        } while (account_name == null);
        do
        {
            Console.WriteLine("\nEnter the amount of money you wish to open this account with in GBP, the minimum is £5:\n5 - 99: 0.75%{0}\n100 - 499: 1.5%{0}\n500-4999: 2%{0}\n5000+: 2.5%{0}", " intrest per annum");
            input = Console.ReadLine();
            if (input.All(x => char.IsDigit(x) || x == '.') && input.Where(x => x == '.').Count() < 2)
            {
                if (double.Parse(input) >= 5) startup_funds = double.Parse(input);
                else Console.WriteLine("the starting funds must be greater than or eaqule to £5.");
            }
            else Console.WriteLine("Please only enter a decimal or intiger value.");
        } while (startup_funds == -1);
        accounts.Add(new Bank_Account(account_name, startup_funds, this));
        Console.WriteLine("\nAccount {0} created sucsessfuly", account_name);
        Write_All();
    }
    public void Write_All()
    {
        File.Create(string.Format("{0}\\{1}.csv", reader.Get_Path(), username)).Close();
        string[] buffer = new string[accounts.Count()];
        for (int i = 0; i < accounts.Count(); i++) buffer[i] = accounts[i].Get_Values();
        File.AppendAllText(string.Format("{0}\\{1}.csv", reader.Get_Path(), username), string.Format("{0}\n{1}\n{2}\n{3}{4}{5}", username, holder_name, salt, password_hash, buffer.Length == 0? "" : "\n",string.Join("\n", buffer)));
    }
    public bool Transfer_Money(string account, double amount) => parent.Transfer_Money(account, amount);
    public bool Has_Account(string account) => accounts.Any(x => x.Get_UID() == account);
    public void Add_Money(string account, double amount) => accounts.First(x => x.Get_UID() == account).Add_Money(amount);
    private List<string> Get_Account_Names()
    {
        List<string> ret = new List<string>();
        foreach (Bank_Account Account in accounts) ret.Add(Account.Get_Name());
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

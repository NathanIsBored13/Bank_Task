using System;

public class Bank_Account
{
    private double balance;
    private string account_name, UID;
    private double intrest;
    private User parent;
    public Bank_Account(string account_name, double starting_funds, User parent)
    {
        this.account_name = account_name;
        balance = starting_funds;
        if (starting_funds < 100) intrest = 0.75;
        else if (starting_funds < 500) intrest = 1.5;
        else if (starting_funds < 5000) intrest = 2;
        else intrest = 2.5;
        this.parent = parent;
        UID = "test";
    }
    public Bank_Account(string account_name, double balance, double intrest, User parent)
    {
        this.account_name = account_name;
        this.balance = balance;
        this.intrest = intrest;
        this.parent = parent;
        UID = "test";
    }
    public void Select()
    {
        Console.WriteLine("\n{0}:\nyour balance is {1} and your intrest rate is {2}", account_name, balance, intrest);
    }
    public string Get_Name() => account_name;
    public string Get_UID() => UID;
    public string Get_Values() => string.Format("{0}, {1}, {2}", account_name, balance, intrest);
    public void Add_Money(int amount)
    {
        balance += amount;
    }
    private void Transfer_Money(string account, double amount)
    {
        balance -= amount;
        parent.Transfer_Money(account, amount);
    }
}

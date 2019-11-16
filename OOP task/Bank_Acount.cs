using System;

public class Bank_Account
{
    private double balance;
    private string account_name;
    private double intrest;
    public Bank_Account(string account_name, double starting_funds)
    {
        this.account_name = account_name;
        balance = starting_funds;
        if (starting_funds < 100) intrest = 0.75;
        else if (starting_funds < 500) intrest = 1.5;
        else if (starting_funds < 5000) intrest = 2;
        else intrest = 2.5;
    }
    public Bank_Account(string account_name, double balance, double intrest)
    {
        this.account_name = account_name;
        this.balance = balance;
        this.intrest = intrest;
    }
    public void Select()
    {
        Console.WriteLine("\n{0}:\nyour balance is {1} and your intrest rate is {2}", account_name, balance, intrest);
    }
    public string Get_Name() => account_name;
    public string Get_Values() => string.Format("{0}, {1}, {2}", account_name, balance, intrest);
}

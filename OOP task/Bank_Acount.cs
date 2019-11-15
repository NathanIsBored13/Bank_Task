using System;

public class Bank_Account
{
    private int Balance;
    private string Account_Name;
    private double Intrest;
    public Bank_Account(string Account_Name, int Starting_Funds)
    {
        this.Account_Name = Account_Name;
        Balance = Starting_Funds;
        if (Starting_Funds < 100) Intrest = 0.75;
        else if (Starting_Funds < 500) Intrest = 1.5;
        else if (Starting_Funds < 5000) Intrest = 2;
        else Intrest = 2.5;
    }
    public void Select()
    {
        Console.WriteLine("{0}:\nyour balance is {1} and your intrest rate is {2}", Account_Name, Balance, Intrest);
    }
    public string Get_Name() => Account_Name;
}

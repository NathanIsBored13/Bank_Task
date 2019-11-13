using System;

public class Bank_Account
{
    private int Balance;
    private string Account_Name;
    private int Intrest;
    public Bank_Account(string Account_Name, int Starting_Funds)
    {
        Account_Name = this.Account_Name;
        Balance = Starting_Funds;
	}
    public string Get_Name() => Account_Name;
}

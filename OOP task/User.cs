using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

public class User
{
    private string Holder_Name;
    private int Salt;
    private string Password_Hash;
    List<Bank_Account> Accounts;
    public User(string Account_Name, string Holder_Name, int Starting_Funds, string Password)
	{
        Random random = new Random();
        Holder_Name = this.Holder_Name;
        int Balance = Starting_Funds;
        Salt = random.Next(0, 100);
        Password_Hash = Hash(Password, Salt);
        Accounts = new List<Bank_Account>();
	}
    public string Get_Account_Name() => Holder_Name;
    public bool Check_Password(string Password) => Password_Hash == Hash(Password, Salt);
    public List<string> Get_Accounts()
    {
        List<string> ret = new List<string>();
        foreach (Bank_Account Account in Accounts) ret.Add(Account.Get_Name());
        return ret;
    }
    public void Logged_In()
    {

    }
    private static string Hash(string data, int salt)
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

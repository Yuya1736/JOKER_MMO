public static class AccountUtility
{
    public static bool CheckAccount(string account)
    {
        return account.Length >= 5 &&  account.Length <= 12;
    }

    public static bool CheckPassword(string password)
    {
        return password.Length >= 6 && password.Length <= 16;
    }
}
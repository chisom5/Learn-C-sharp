namespace OOP;

class Program
{

    static void Main(string[] args)
    {
        BankAccount account = new BankAccount("123456789", 1000.00m, "Chisom Okoye");

        account.Deposit(500.00m);
        Console.WriteLine($"Current Balance: {account.Balance:C}");
    }
}


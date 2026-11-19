namespace OOP;

public class BankAccount
{

    // private fields (data hiding)
    private string _accountNumber;
    private decimal _balance;
    private string _accountHolder;

    public BankAccount(string accountNumber, decimal initialBalance, string accountHolder)
    {
        _accountNumber = accountNumber;
        _balance = initialBalance;
        _accountHolder = accountHolder;
    }

    // method to deposit money
    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            _balance += amount;
            Console.WriteLine($"Deposited {amount:C} to account {_accountNumber}. New balance: {_balance}");
        }
        else
        {
            Console.WriteLine($"Deposited amount must be positive");
        }
    }

    public decimal Balance
    {
        get { return _balance; }
        private set {_balance = value;}
    }
    public string AccountHolder
    {
        get { return _accountHolder; }
    }
}

# OOP

Object Oriented Programming is a pattern that organises software design around objects and classes rather than functions and logic.
C# is fully object-oriented language that supports all four main OOP principles:

- Encapsulation
- Abstraction
- Inheritance
- and Polymorphism.

The benefits of using OOP is

1. Modularity: OOP promote modularity by breaking down complex systems into smaller, manageable parts (objects). with this it makes it easier to maintain

2. Resuability: OOP allows you to reuse existing code by creating new objects based on existing ones.

3. Flexibilty: OOP provides flexibility in designing and implementing software systems. You can easily modify and extend the functionality of objects without affecting other parts of the system.

4. Scalability: OOP supports scalability by allowing you to add new objects and classes as the system grows.

## Encapsulation

Encapsulation: Is hiding internal implementation details and exposing only what is neccessary using access modifiers.
i.e using private, public, protected etc. In essence, Encapsulation conceal the internal state of an object and only exposes the necessary information to the outside world.

Example

```csharp
public class BankAccount{

    // private fields (data hiding)
    private string _accountNumber;
    private decimal _balance;
    private string _accountHolder;

    public BankAccount(string accountNumber, decimal initialBalance, string accountHolder){
        _accountNumber = accountNumber;
        _balance = initialBalance;
        _accountHolder = accountHolder;
    }

    // method to deposit money
    public void Deposit(decimal amount){
        if(amount > 0){
            _balance +=amount;
            Console.WriteLine($"Deposited {amount} to account {_accountNumber}. New balance: {_balance}")
        }else{
            Console.WriteLine($"Deposited amount must be positive")
        }
    }

    public decimal Balance
    {
        get {return _balance;}
    }
    public string AccountHolder
    {
        get {return _accountHolder;}
    }
}
```

Key Points:

- from the example above we use access modifier (private, public, protected for data hiding)

- provide public methods to access and modify private data (getters/setters). to make the balance and account holder accessible we use public method.

- while private access modifier is use to proctect internal state of an object

### Abstraction

Abstraction means hiding complex implementation details and showing only the necessary features of an object. It helps to reduce complexity and increase efficiency by focusing on what an object does rather than how it does it.

Example

```csharp
// Abstract class
public abstract class Vehicle
{
    // Abstract method (no implementation)
    public abstract void Start();

    // Abstract method
    public abstract void Stop();

    // Concrete method
    public void DisplayInfo()
    {
        Console.WriteLine("This is a vehicle.");
    }
}

// Interface
public interface IDriveable
{
    void Accelerate();
    void Brake();
}

public class Car: Vehicle, IDriveable
{
    // handle the behaviour of the method.
    public override void Start()
    {
         Console.WriteLine("Car engine started.");
    }

    public override void Stop()
    {
        Console.WriteLine("Car engine stopped.");
    }

    public void Accelerate()
    {
        Console.WriteLine("Car is accelerating.");
    }

    public void Brake()
    {
        Console.WriteLine("Car is braking.");
    }
}

```

Key Points:

- Abstract classes and interfaces

- Showing only essential features

- Hiding background details

#### Inheritance

Inheritance is a mechanism where a class inherit methods, properties from another class. it promote code reusability and establishes a relationship between classes.

Base class (parent class) and derived class(child class). in C# we have different kind of inheirtance

- Single Inheritance: which is one child inherit from one parent class. To illustrate this, let's consider a real-world analogy. Imagine you are the only child of your father. i.e the single inheritance

- Multi-Level Inheritance: Here a class is derived from another derived class creating a chain of inheritance. e.g you are the child of your father, and your father is the child of your grandfather.

- Hierarchical Inheritance: Here the relationship is multiple derived class inherit from one parent or base class.


Example: Hierarchical inheritance.

```csharp
// Base class
public class Animal(string name)
{
    public string Name { get; set; } = name;


    public virtual void Eat()
    {
        Console.WriteLine($"{Name} is eating.");
    }

    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping.");
    }
}

// Derived class
public class Dog : Animal
{
    public string Breed { get; set; }

    public Dog(string name, string breed) : base(name)
    {
        Breed = breed;
    }

    // Method overriding
    public override void Eat()
    {
        Console.WriteLine($"{Name}, the {Breed}, is eating dog food.");
    }

    // New method specific to Dog 
    public void Bark()
    {
        Console.WriteLine($"{Name} is barking: Woof Woof!");
    }
}

// Another derived class
public class Cat : Animal
{
    public Cat(string name) : base(name) { }

    // Method overriding
    public override void Eat()
    {
        Console.WriteLine($"{Name} is eating cat food.");
    }

    // New method specific to Cat
    public void Meow()
    {
        Console.WriteLine($"{Name} says: Meow!");
    }
}
```
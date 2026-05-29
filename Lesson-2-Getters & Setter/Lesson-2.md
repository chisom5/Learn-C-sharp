# Getters & Setters

Getters and Setters are the controlled doorway into a property. A getter - reads a value while a setter - writes a value. 

example.

```csharp
 public class Musician(string name, Guitar guitar) : Extensions
    {
        public string Name { get; set; } = name;
        public Guitar Guitar { get; set; } = guitar;

        public void Print() => Console.WriteLine($"Musician: {Name}, Guitar: {Guitar.Brand}");
    }
```

From our example the property public string Name {get; set;} = name; what is actually happening here. 
we are setting Name = name and also indicating that we want to read the value.

the long way of writing this is
```csharp
private string _name; // the actual storage

public string Name
{
    get { return _name; }   // getter — reading
    set { _name = value; }  // setter — writing
}
```

but the syntax `public string Name {get; set;}` is auto-implmented meaning it is running the lonq way at the background.
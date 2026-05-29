# Interface & Abstract class

An Interface - Is a contract that defines a set of behaviour, methods, properties, events that a class must implement.

example. 
```csharp 
interface IDrawable
{
    void Draw();
}
interface IShape : IDrawable
{
    double Area { get; }
}

namespace MyFirstApp
{
    class Circle(double radius) : IShape
    {
        static void Main(string[] args)
        {
            Circle circle = new Circle(5);
            circle.Draw();
        }

        public double Area { get; } = Math.PI * radius * radius;
        public void Draw() => Console.WriteLine(Area);

    }

}
```
In this example we have an interface that describe what the class  `Circle` should do. and in this example
we understand that an interface can be inherited. just like the interface IShape inherit from IDrawable. 

## Difference between an interface & Abstract class.

Interface - A class can implement multiple interfaces. and it can not contains fields or constructor.
Abstract class -  A class can inherit only one abstract class. and it can have fields, properties and constructor.

```csharp
public abstract class Extensions
{
    public abstract void Print();
}

public class Musician(string name, Guitar guitar) : Extensions
    {
        public string Name { get; set; } = name;
        public Guitar Guitar { get; set; } = guitar;

        public override void Print() => Console.WriteLine($"Musician: {Name}, Guitar: {Guitar.Brand}");
    }

```
we have an abstract class Extension and we have the Musician class inheriting from it. for this Musician class
I can not have another abstract class to be inherited by it.

### Encapsulation, Abstraction

- Encapsulation - Is hiding internal implementation details and exposing only what is neccessary using access modifiers.
i.e using private, public, protected etc. 
Encapsulation focuses on how to hide the internal state of an object and 
bundle data with method.

- Abstraction - Is hiding Complex implementation details and showing only the essential features of an object. we having abstraction using interfaces and abstract class. Abstraction focuses on what an object does.
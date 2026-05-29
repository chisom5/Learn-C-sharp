# Class in C#

A namespace is like a wrapper that group related types together and prevents naming collisions.

class - is a reference type that defines a blueprint for an object.

example. 
``` class Program {
        
    static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
        }
}
```
the static method "Main" is what actually run the program. and in this case when we 
run the console application using dotent run. it shows in the console. "Hello World!".

## Modifiers 

# Access Modifier

Access modifier is what controls the visiblity of our classes, methods and variable.

we have different kinds
- Public : means that any other code in the project can see and use it.
- Private : means that only code inside the same class can see it.
- Protected : only visible within it class and subclass.
- Internal : means project-wide. so any code within the same project file can access it.

## Non-Access modifier

Non access modifier are modifier with this keyword abstract, static, sealed etc.

- Abstract : means unfinished blueprint. abstract class must be inherited by another class. 
you can't create an object instance from it.

- Static: belong to the class itself. just like our static void Main. from the code above.
you don't need to use the new keyword to invoke it.
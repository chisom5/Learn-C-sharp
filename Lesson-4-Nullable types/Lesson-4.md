# Null Saftey in C#

Null represents the absence of a value. So when you try to access a member on a null reference, by calling a method or reading a property, the runtime throws a NULLReferenceExecption.

C# gives different ways to write null-safe code.  
- Nullable Value Type 
- Nullable Reference Type

## Nullable Value type

Value type such as `int, bool, char, double, float` etc. can't hold `null` by default. so adding ? to the type name
to create a nullable value type that can hold a value or null.
e.g

``` csharp
 int? age = null;
    
```
on this example we are telling csharp that is it okay for my variable age to be assign to null.
N.B we can use this when we need to represent "No data" for a database column that might be absent.

## Nullable Reference type

Nullable reference types is a compiler feature that makes null intent explicit and catches mistakes at compile time. reference type such as `string, array and class instances` can hold null at runtime.  Hence by using the ? annotation, you declare your intent:

- string? — this reference might be null; meaning i want to make this optional. it can have a value or not.
- string — this reference should not be null; the compiler warns if you assign null to it. and it mean that it must have a value.

# By Value & By Reference

In c# data are store in memory as either value types or reference type.

- value type:  store their actual data directly in memory. meaning when assigned to a new variable, a copy of the value
is created, so therefore modifying one variable does not affect the original one.

All primitive types -  `int, float, double, char, enum, decmail etc.` are all value types.

- Reference type: store the reference to the aactual data, so when assigned to a new variable both the variables point to the same
memory location. meaning changing one value doesn't affect the other.
examples of data types that falls under this category are: `string, array, class, delegate etc.`

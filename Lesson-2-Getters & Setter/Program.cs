
namespace GettersAndSetters
{
    class Program
    {
        static void Main(string[] args)
        {
            Musician m = new Musician("Burna boy");
            m.Print();
        }
    }

    public class Musician(string name) 
    {
        public string Name { get; set; } = name;

        public void Print() => Console.WriteLine($"Musician: {Name}");
    }

}
interface IDrawable
{
    void Draw();
}
interface IShape : IDrawable
{
    double Area { get; }
}

public abstract class Extensions
{
    public abstract void Print();
}

namespace Interfaces
{
    class Circle(double radius) : IShape
    {
        static void Main(string[] args)
        {
            Circle circle = new Circle(5);
            circle.Draw();

            Guitar g = new Guitar("Fender");
            Musician m = new Musician("Chisom", g);

            m.Print();

        }

        public double Area { get; } = Math.PI * radius * radius;
        public void Draw() => Console.WriteLine(Area);

    }

    public class Musician(string name, Guitar guitar) : Extensions
    {
        public string Name { get; set; } = name;
        public Guitar Guitar { get; set; } = guitar;

        public override void Print() => Console.WriteLine($"Musician: {Name}, Guitar: {Guitar.Brand}");
    }

}
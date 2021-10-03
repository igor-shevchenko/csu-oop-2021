using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Program
    {
        static void Main(string[] args)
        {
            //var circle = new Circle(new Point(0, 0), 2);
            //Console.WriteLine(circle.GetArea());
            //Console.WriteLine(circle.IsInside(new Point(2, 2)));

            //var rectangle = new Rectangle(new Point(0, 0), 5, 2);
            //Console.WriteLine(rectangle.GetArea());
            //Console.WriteLine(rectangle.IsInside(new Point(1, 1)));

            var rectangle = new RectangleByTwoPoints(
                new Point(0, 0), 
                new Point(-1, 0)
                );

        }
    }

    class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public double GetDistanceTo(Point point)
        {
            return Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2));
        }
    }

    interface IShape
    {
        Point Center { get; }
        double GetArea();
        bool IsInside(Point point);
    }

    class Circle : IShape
    {
        public Circle(Point center, int radius)
        {
            Center = center;
            Radius = radius;
        }

        public Point Center { get; }
        public int Radius { get; }

        public double GetArea()
        {
            return Math.PI * Radius * Radius;
        }

        public bool IsInside(Point point)
        {
            return Center.GetDistanceTo(point) <= Radius;
        }
    }

    class Rectangle : IShape
    {
        public Rectangle(Point center, int width, int height)
        {
            Center = center;
            Width = width;
            Height = height;
        }

        public Point Center { get; }
        public int Width { get; }
        public int Height { get; }

        public double GetArea()
        {
            return Height * Width;
        }

        public bool IsInside(Point point)
        {
            return Math.Abs(Center.X - point.X) <= (double) Width / 2 &&
                   Math.Abs(Center.Y - point.Y) <= (double) Height / 2;
        }
    }

    class RectangleByTwoPoints : IShape
    {
        private readonly Point _leftTop;
        private readonly Point _rightBottom;

        public RectangleByTwoPoints(Point leftTop, Point rightBottom)
        {
            if (leftTop.X >= rightBottom.X || leftTop.Y <= rightBottom.Y)
            {
                throw new ArgumentException("Incorrect rectangle");
            }

            _leftTop = leftTop;
            _rightBottom = rightBottom;
        }

        public Point Center => new Point((_leftTop.X - _rightBottom.X) / 2 + _rightBottom.X,
            (_leftTop.Y - _rightBottom.Y) / 2 + _rightBottom.Y);

        public double GetArea()
        {
            return (_rightBottom.X - _leftTop.X) * (_leftTop.Y - _rightBottom.Y);
        }

        private bool IsBetween(int valueMin, int testValue, int valueMax)
        {
            return valueMin <= testValue && valueMax >= testValue;
        }

        public bool IsInside(Point point)
        {
            return IsBetween(_leftTop.X, point.X, _rightBottom.X)
                   && IsBetween(_rightBottom.Y, point.Y, _leftTop.Y);
        }
    }
}

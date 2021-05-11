using System;

namespace PVcase.Models
{
    public class Point : IEquatable<Point>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public bool Equals(Point other)
        {
            return other != null && other.X == this.X && other.Y == this.Y;
        }
    }
}

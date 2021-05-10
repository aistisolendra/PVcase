using System;

namespace PVcase.Models
{
    public class Line : IEquatable<Line>
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public bool Equals(Line other)
        {
            return other != null && this.Start.Equals(other.Start) && this.End.Equals(other.End);
        }
    }
}

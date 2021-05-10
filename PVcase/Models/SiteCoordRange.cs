using System;
using static System.Int32;

namespace PVcase.Models
{
    public class SiteCoordRange : IEquatable<SiteCoordRange>
    {
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }


        public SiteCoordRange()
        {
            MinX = MaxValue;
            MaxX = MinValue;
            MinY = MaxValue;
            MaxY = MinValue;
        }

        public bool Equals(SiteCoordRange other)
        {
            return other != null &&
                   this.MinX == other.MinX &&
                   this.MaxX == other.MaxX &&
                   this.MinY == other.MinY &&
                   this.MaxY == other.MaxY;
        }
    }
}

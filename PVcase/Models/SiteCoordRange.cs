using System;

namespace PVcase.Models
{
    public class SiteCoordRange
    {
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }


        public SiteCoordRange()
        {
            MinX = Int32.MaxValue;
            MaxX = Int32.MinValue;
            MinY = Int32.MaxValue;
            MaxY = Int32.MinValue;
        }
    }
}

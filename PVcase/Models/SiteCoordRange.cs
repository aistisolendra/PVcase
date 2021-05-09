using static System.Int32;

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
            MinX = MaxValue;
            MaxX = MinValue;
            MinY = MaxValue;
            MaxY = MinValue;
        }
    }
}

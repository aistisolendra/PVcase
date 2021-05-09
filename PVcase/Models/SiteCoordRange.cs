using static System.Int32;

namespace PVcase.Models
{
    public class SiteCoordRange
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
    }
}

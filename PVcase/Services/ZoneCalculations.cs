using System.Collections.Generic;
using System.Linq;
using PVcase.Models;

namespace PVcase.Services
{
    public class ZoneCalculations
    {
        public SiteCoordRange GetRange(List<Point> coordinates)
        {
            if (coordinates.Count == 0)
                return NewRange();

            var siteCoords = new SiteCoordRange
            {
                MaxX = coordinates.Max(p => p.X),
                MaxY = coordinates.Max(p => p.Y),
                MinX = coordinates.Min(p => p.X),
                MinY = coordinates.Min(p => p.Y)
            };

            return siteCoords;
        }

        public SiteCoordRange NewRange()
        {
            return new SiteCoordRange()
            {
                MaxY = 0,
                MaxX = 0,
                MinX = 0,
                MinY = 0
            };
        }
    }
}

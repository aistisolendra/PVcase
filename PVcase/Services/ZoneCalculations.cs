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
                return null;

            var siteCoords = new SiteCoordRange
            {
                MaxX = coordinates.Max(p => p.X),
                MaxY = coordinates.Max(p => p.Y),
                MinX = coordinates.Min(p => p.X),
                MinY = coordinates.Min(p => p.Y)
            };

            return siteCoords;
        }
    }
}

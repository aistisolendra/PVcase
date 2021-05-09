using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PVcase.Models;

namespace PVcase.Services
{
    public class ZoneCalculations
    {
        public SiteCoordRange GetRange(List<Point> coordinates)
        {
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

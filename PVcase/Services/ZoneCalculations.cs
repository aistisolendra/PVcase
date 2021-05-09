using PVcase.Models;

namespace PVcase.Services
{
    public class ZoneCalculations
    {
        public SiteCoordRange GetRange(int[,] coordinates)
        {
            var siteCoords = new SiteCoordRange();

            for (int i = 0; i < coordinates.GetLength(0); ++i)
            {
                if (siteCoords.MaxX < coordinates[i, 0])
                    siteCoords.MaxX = coordinates[i, 0];

                if (siteCoords.MinX > coordinates[i, 0])
                    siteCoords.MinX = coordinates[i, 0];

                if (siteCoords.MaxY < coordinates[i, 1])
                    siteCoords.MaxY = coordinates[i, 1];

                if (siteCoords.MinY > coordinates[i, 1])
                    siteCoords.MinY = coordinates[i, 1];
            }

            return siteCoords;
        }
    }
}

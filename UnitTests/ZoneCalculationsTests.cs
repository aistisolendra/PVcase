using System.Collections.Generic;
using NUnit.Framework;
using PVcase.Models;
using PVcase.Services;

namespace UnitTests
{
    public class ZoneCalculationsTests
    {
        [Test]
        public void GetRange_InputPointList_ReturnsSiteCoordRange()
        {
            var zoneCalculations = new ZoneCalculations();
            var coordinatesList = new List<Point>
            {
                new Point(4,5),
                new Point(1,10),
                new Point(6,15),
                new Point(10,2)
            };
            var expected = new SiteCoordRange()
            {
                MaxX = 10,
                MaxY = 15,
                MinX = 1,
                MinY = 2
            };

            var result = zoneCalculations.GetRange(coordinatesList);

            Assert.AreEqual(expected,result);
        }

        [Test]
        public void GetRange_InputNegativePointList_ReturnsSiteCoordRange()
        {
            var zoneCalculations = new ZoneCalculations();
            var coordinatesList = new List<Point>
            {
                new Point(-4,5),
                new Point(1,10),
                new Point(6,-15),
                new Point(-10,-2)
            };
            var expected = new SiteCoordRange()
            {
                MaxX = 6,
                MaxY = 10,
                MinX = -10,
                MinY = -15
            };

            var result = zoneCalculations.GetRange(coordinatesList);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetRange_InputEmptyPointList_ReturnsNewSiteCoordRange()
        {
            var zoneCalculations = new ZoneCalculations();
            var coordinatesList = new List<Point>();
            var expected = zoneCalculations.NewRange();

            var result = zoneCalculations.GetRange(coordinatesList);

            Assert.AreEqual(expected, result);
        }
    }
}

using System.Collections.Generic;
using NUnit.Framework;
using PVcase.Models;
using PVcase.Services;

namespace UnitTests
{
    public class ConverterTests
    {
        [Test]
        public void PointsToLines_InputEmptyList_ReturnsEmptyList()
        {
            var converter = new Converter();
            var expected = new List<Point>();

            var result = converter.PointsToLines(expected);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void PointsToLines_ListWithThreePoints_ReturnsListWithThreeLines()
        {
            var converter = new Converter();
            var pointList = new List<Point>()
            {
                new Point(0, 1),
                new Point(1, 2),
                new Point(2, 3)
            };
            var expected = new List<Line>()
            {
                new Line(new Point(0, 1), new Point(1, 2)),
                new Line(new Point(1, 2), new Point(2, 3)),
                new Line(new Point(2, 3), new Point(0, 1))
            };

            var result = converter.PointsToLines(pointList);

            CollectionAssert.AreEqual(expected, result);
        }
    }
}
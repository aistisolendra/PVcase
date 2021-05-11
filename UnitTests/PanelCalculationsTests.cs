using System.Collections.Generic;
using NUnit.Framework;
using PVcase.Models;
using PVcase.Services;

namespace UnitTests
{
    public class PanelCalculationsTests
    {
        [Test]
        public void CheckIfPointIsOnTheRight_PointIsOnTheRight_ReturnTrue()
        {
            var panelCalculations = new PanelCalculations();
            var currentPoint = new Point(10, 10);
            var previousPoint = new Point(2, 2);
            var testPoint = new Point(12, 5);

            bool result = panelCalculations.CheckIfPointIsOnTheRight(testPoint,
                                                                        currentPoint,
                                                                        previousPoint);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CheckIfPointIsOnTheRight_PointIsOnTheLeft_ReturnFalse()
        {
            var panelCalculations = new PanelCalculations();
            var currentPoint = new Point(10, 10);
            var previousPoint = new Point(2, 2);
            var testPoint = new Point(1, 5);

            bool result = panelCalculations.CheckIfPointIsOnTheRight(testPoint,
                                                                     currentPoint,
                                                                     previousPoint);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CheckIfPointIntersects_PointsIntersect_ReturnsTrue()
        {
            var panelCalculations = new PanelCalculations();
            var currentPoint = new Point(10, 2);
            var previousPoint = new Point(2, 10);
            var testPoint = new Point(7, 5);

            bool result = panelCalculations.CheckIfPointIntersects(testPoint,
                                                                   currentPoint,
                                                                   previousPoint);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CheckIfPointIntersects_PointsDoNotIntersect_ReturnsFalse()
        {
            var panelCalculations = new PanelCalculations();
            var currentPoint = new Point(10, 10);
            var previousPoint = new Point(2, 2);
            var testPoint = new Point(7, 1);

            bool result = panelCalculations.CheckIfPointIntersects(testPoint,
                currentPoint,
                previousPoint);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsPointInside_PointIsInside_ReturnsTrue()
        {
            var panelCalculations = new PanelCalculations();
            var polygon = new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10),
                new Point(0, 10)
            };
            var testPoint = new Point(5, 5);

            bool result = panelCalculations.IsPointInside(polygon, testPoint);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsPointInside_PointIsNotInside_ReturnsFalse()
        {
            var panelCalculations = new PanelCalculations();
            var polygon = new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10),
                new Point(0, 10)
            };
            var testPoint = new Point(12, 5);

            bool result = panelCalculations.IsPointInside(polygon, testPoint);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsAnyPointInside_OneInsideOneOutside_ReturnsTrue()
        {
            var panelCalculations = new PanelCalculations();
            var polygon = new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10),
                new Point(0, 10)
            };
            var testPointList = new List<Point>()
            {
                new Point(5, 5),
                new Point(12, 5)
            };

            bool result = panelCalculations.IsAnyPointInside(testPointList, polygon);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsAnyPointInside_AllOutside_ReturnsFalse()
        {
            var panelCalculations = new PanelCalculations();
            var polygon = new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10),
                new Point(0, 10)
            };
            var testPointList = new List<Point>()
            {
                new Point(80, 5),
                new Point(12, 5)
            };

            bool result = panelCalculations.IsAnyPointInside(testPointList, polygon);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsAnyPointInside_AllInside_ReturnsTrue()
        {
            var panelCalculations = new PanelCalculations();
            var polygon = new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10),
                new Point(0, 10)
            };
            var testPointList = new List<Point>()
            {
                new Point(5, 5),
                new Point(6, 5)
            };

            bool result = panelCalculations.IsAnyPointInside(testPointList, polygon);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsAllPointsInside_OneInsideOneOutside_ReturnsFalse()
        {
            var panelCalculations = new PanelCalculations();
            var polygon = new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10),
                new Point(0, 10)
            };
            var testPointList = new List<Point>()
            {
                new Point(12, 5),
                new Point(6, 5)
            };

            bool result = panelCalculations.IsAllPointsInside(testPointList, polygon);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsAllPointsInside_AllOutside_ReturnsFalse()
        {
            var panelCalculations = new PanelCalculations();
            var polygon = new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10),
                new Point(0, 10)
            };
            var testPointList = new List<Point>()
            {
                new Point(12, 5),
                new Point(18, 5)
            };

            bool result = panelCalculations.IsAllPointsInside(testPointList, polygon);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsAllPointsInside_AllInside_Returnstrue()
        {
            var panelCalculations = new PanelCalculations();
            var polygon = new List<Point>()
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10),
                new Point(0, 10)
            };
            var testPointList = new List<Point>()
            {
                new Point(5, 5),
                new Point(6, 5)
            };

            bool result = panelCalculations.IsAllPointsInside(testPointList, polygon);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreatePanelPoints_ValidInputs_ReturnsNewPointList()
        {
            var panelCalculations = new PanelCalculations();
            var testSolarPanel = new SolarPanel(10, 10, 10,
                10, 0, new Point(5, 5));
            var expected = new List<Point>()
            {
                new Point(5,5),
                new Point(15, 5),
                new Point(5, 15),
                new Point(15,15)
            };

            var result = panelCalculations.CreatePanelPoints(testSolarPanel);

            Assert.AreEqual(expected,result);
        }

        [Test]
        public void CreatePanelPoints_InputZeros_ReturnsNewPointList()
        {
            var panelCalculations = new PanelCalculations();
            var testSolarPanel = new SolarPanel(0,0,0,0,0, new Point(0,0));
            var expected = new List<Point>()
            {
                new Point(0,0),
                new Point(0,0),
                new Point(0,0),
                new Point(0,0)
            };

            var result = panelCalculations.CreatePanelPoints(testSolarPanel);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CanPanelFit_PanelCanFit_ReturnsTrue()
        {
            var panelCalculations = new PanelCalculations();
            var testSolarPanel = new SolarPanel(2, 2, 1, 1, 10, 2,2);
            var testSitePoints = new List<Point>()
            {
                new Point(0, 0),
                new Point(0, 100),
                new Point(100, 100),
                new Point(100, 0)
            };
            var restrictedPoints = new List<Point>()
            {
                new Point(20, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30)
            };

            bool result = panelCalculations.CanPanelFit(testSolarPanel, testSitePoints, restrictedPoints);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CanPanelFit_PanelCant_ReturnsFalse()
        {
            var panelCalculations = new PanelCalculations();
            var testSolarPanel = new SolarPanel(10, 10, 1, 1, 10, 2, 2);
            var testSitePoints = new List<Point>()
            {
                new Point(0, 0),
                new Point(0, 10),
                new Point(10, 10),
                new Point(10, 0)
            };
            var restrictedPoints = new List<Point>()
            {
                new Point(20, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30)
            };

            bool result = panelCalculations.CanPanelFit(testSolarPanel, testSitePoints, restrictedPoints);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CanPanelFit_PanelIsRestricted_ReturnsFalse()
        {
            var panelCalculations = new PanelCalculations();
            var testSolarPanel = new SolarPanel(10, 10, 1, 1, 10, 22, 22);
            var testSitePoints = new List<Point>()
            {
                new Point(0, 0),
                new Point(0, 100),
                new Point(100, 100),
                new Point(100, 0)
            };
            var restrictedPoints = new List<Point>()
            {
                new Point(20, 20),
                new Point(30, 20),
                new Point(30, 30),
                new Point(20, 30)
            };

            bool result = panelCalculations.CanPanelFit(testSolarPanel, testSitePoints, restrictedPoints);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void GetTiltedPanelWidth_TiltAngleIsZero_Return()
        {
            var panelCalculations = new PanelCalculations();
            var testSolarPanel = new SolarPanel(0, 0, 0, 0, 0, 0, 0);

            panelCalculations.GetTiltedPanelWidth(testSolarPanel);

            Assert.AreEqual(0, testSolarPanel.Width);
        }

        [Test]
        public void GetTiltedPanelWidth_TiltAngleIsTen_ReturnNewWidth()
        {
            var panelCalculations = new PanelCalculations();
            var testSolarPanel = new SolarPanel(10, 0, 0, 0, 10, 0, 0);

            panelCalculations.GetTiltedPanelWidth(testSolarPanel);

            Assert.AreEqual(9.84807753012208, testSolarPanel.Width);
        }

    }
}

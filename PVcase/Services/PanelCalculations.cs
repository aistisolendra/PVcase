using System;
using System.Collections.Generic;
using System.Linq;
using PVcase.Models;

namespace PVcase.Services
{
    public class PanelCalculations
    {
        public List<Point> GetPlacingPoints(SolarPanel solarPanel, List<Point> siteCoordinationPoints,
                                            List<Point> restrictionCoordinationPoints, ZoneCalculations zoneCalculations)
        {
            GetTiltedPanelWidth(solarPanel);
            var siteRange = zoneCalculations.GetRange(siteCoordinationPoints);

            return FindPlacingPoints(solarPanel, siteCoordinationPoints, restrictionCoordinationPoints, siteRange);
        }

        public List<Point> FindPlacingPoints(SolarPanel solarPanel, List<Point> sitePoints, List<Point> restrictionPoints, SiteCoordRange siteRange)
        {
            var pointsForPanelPlacing = new List<Point>();

            for (double yPoint = siteRange.MinY; yPoint < siteRange.MaxY; ++yPoint)
            {
                solarPanel.OriginPoint.Y = yPoint;

                var placingPointsInRow = FindRowPlacingPoints(solarPanel, siteRange, sitePoints, restrictionPoints);
                pointsForPanelPlacing.AddRange(placingPointsInRow);

                if (placingPointsInRow.Count > 0)
                    yPoint += solarPanel.Width + solarPanel.RowSpacing;
            }

            return pointsForPanelPlacing;
        }

        public List<Point> FindRowPlacingPoints(SolarPanel solarPanel, SiteCoordRange siteRange, List<Point> sitePoints,
              List<Point> restrictionPoints)
        {
            var rowPoints = new List<Point>();

            for (double xPoint = siteRange.MinX; xPoint < siteRange.MaxX; ++xPoint)
            {
                solarPanel.OriginPoint.X = xPoint;

                if (CanPanelFit(solarPanel, sitePoints, restrictionPoints))
                {
                    rowPoints.Add(new Point(solarPanel.OriginPoint));
                    xPoint += solarPanel.Length + solarPanel.ColumnSpacing;
                }
            }

            return rowPoints;
        }

        public void GetTiltedPanelWidth(SolarPanel panel)
        {
            if (panel.TiltAngle == 0) return;

            double radians = (Math.PI / 180) * panel.TiltAngle;
            panel.Width *= Math.Abs(Math.Cos(radians));

        }

        public bool CanPanelFit(SolarPanel solarPanel, List<Point> sitePoints, List<Point> restrictionPoints)
        {
            var panelPoints = CreatePanelPoints(solarPanel);

            return !IsAnyPointInside(panelPoints, restrictionPoints) &&
                   IsAllPointsInside(panelPoints, sitePoints);
        }

        public List<Point> CreatePanelPoints(SolarPanel solarPanelData)
        {
            var panelPoints = new List<Point>()
            {
                solarPanelData.OriginPoint,
                new Point(solarPanelData.OriginPoint.X + solarPanelData.Length, solarPanelData.OriginPoint.Y),
                new Point(solarPanelData.OriginPoint.X, solarPanelData.OriginPoint.Y + solarPanelData.Width),
                new Point(solarPanelData.OriginPoint.X + solarPanelData.Length, solarPanelData.OriginPoint.Y + solarPanelData.Width)
            };

            return panelPoints;
        }

        public bool IsAllPointsInside(List<Point> testPoints, List<Point> polygonPoints)
        {
            return testPoints.All(point => IsPointInside(polygonPoints, point) != false);
        }

        public bool IsAnyPointInside(List<Point> testPoints, List<Point> polygonPoints)
        {
            return testPoints.Any(point => IsPointInside(polygonPoints, point));
        }

        public bool IsPointInside(List<Point> polygon, Point testPoint)
        {
            bool result = false;
            int j = polygon.Count - 1;

            for (int i = 0; i < polygon.Count; i++)
            {
                var currentPoint = polygon[i];
                var previousPoint = polygon[j];

                if (CheckIfPointIntersects(testPoint, currentPoint, previousPoint) &&
                    CheckIfPointIsOnTheRight(testPoint, currentPoint, previousPoint))
                    result = !result;

                j = i;
            }

            return result;
        }
        public bool CheckIfPointIntersects(Point testPoint, Point currentPoint, Point previousPoint)
        {
            return currentPoint.Y < testPoint.Y && previousPoint.Y >= testPoint.Y ||
                   previousPoint.Y < testPoint.Y && currentPoint.Y >= testPoint.Y;
        }

        public bool CheckIfPointIsOnTheRight(Point testPoint, Point currentPoint, Point previousPoint)
        {
            double xPosition = currentPoint.X + (testPoint.Y - currentPoint.Y)
                / (previousPoint.Y - currentPoint.Y)
                * (previousPoint.X - currentPoint.X);

            return xPosition < testPoint.X;
        }
    }
}

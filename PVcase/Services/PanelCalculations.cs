using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PVcase.Models;

namespace PVcase.Services
{
    public class PanelCalculations
    {
        public List<Point> GetPanelPoints(SolarPanel panel, SiteCoordRange siteRange,
                                          List<Point> siteCoordinationPoints, List<Point> restrictionCoordinationPoints)
        {
            var allOriginPoints = new List<Point>();
            panel.Width = GetPanelWidth(panel);

            for (double y = siteRange.MinY; y < siteRange.MaxY; ++y)
            {
                bool panelFits = false;
                for (double x = siteRange.MinX; x < siteRange.MaxX; ++x)
                {
                    panel.OriginPoint = new Point(x, y);

                    if (CanPanelFit(panel, siteCoordinationPoints, restrictionCoordinationPoints))
                    {
                        allOriginPoints.Add(panel.OriginPoint);
                        x += panel.Length + panel.ColumnSpacing;
                        panelFits = true;
                    }
                }

                if (panelFits)
                    y += panel.Width + panel.RowSpacing;
            }

            return allOriginPoints;
        }

        public double GetPanelWidth(SolarPanel panel)
        {
            if (panel.TiltAngle == 0)
                return panel.Width;

            double radians = (Math.PI / 180) * panel.TiltAngle;
            return panel.Width * Math.Abs(Math.Cos(radians));
        }

        public bool CanPanelFit(SolarPanel solarPanel, List<Point> sitePoints, List<Point> restrictionPoints)
        {
            var panelPoints = new List<Point>()
            {
                solarPanel.OriginPoint,
                new Point(solarPanel.OriginPoint.X + solarPanel.Length, solarPanel.OriginPoint.Y),
                new Point(solarPanel.OriginPoint.X, solarPanel.OriginPoint.Y + solarPanel.Width),
                new Point(solarPanel.OriginPoint.X + solarPanel.Length, solarPanel.OriginPoint.Y + solarPanel.Width)
            };

            return !IsAnyPointInside(panelPoints, restrictionPoints) &&
                   IsAllPointsInside(panelPoints, sitePoints);
        }


        public bool IsAllPointsInside(List<Point> testPoints, List<Point> polygonPoints)
        {
            return testPoints.All(point => IsPointInside(polygonPoints, point) != false);
        }


        public bool IsAnyPointInside(List<Point> testPoints, List<Point> polygonPoints)
        {
            return testPoints.Any(point => IsPointInside(polygonPoints, point));
        }

        public static bool IsPointInside(List<Point> polygon, Point testPoint)
        {
            bool result = false;
            int j = polygon.Count - 1;
            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
    }
}

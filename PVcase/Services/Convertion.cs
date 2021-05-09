using System.Collections.Generic;
using System.Windows;
using PVcase.Models;

namespace PVcase.Services
{
    public class Convertion
    {
        public List<Point> CoordinatesToPoints(int[,] coordinates)
        {
            var pointList = new List<Point>();

            for (int i = 0; i < coordinates.GetLength(0); ++i)
            {
                var point = new Point(coordinates[i, 0], coordinates[i, 1]);
                pointList.Add(point);
            }

            return pointList;
        }

        public List<Line> PointsToLines(List<Point> points)
        {
            var linesList = new List<Line>();

            for (int i = 0; i < points.Count; ++i)
            {
                var start = points[i];
                var end = new Point();

                if (i != points.Count - 1)
                    end = points[i + 1];
                else
                    end = points[0];

                var line = new Line(start, end);
                linesList.Add(line);
            }

            return linesList;
        }

        public List<Line> CoordinatesToLines(int[,] coordinates)
        {
            var points = CoordinatesToPoints(coordinates);
            var lines = PointsToLines(points);

            return lines;
        }
    }
}

using System.Collections.Generic;
using PVcase.Models;

namespace PVcase.Services
{
    public class Converter
    {
        public List<Line> PointsToLines(List<Point> points)
        {
            var linesList = new List<Line>();

            for (int i = 0; i < points.Count; ++i)
            {
                var start = points[i];
                var end = new Point();

                end = i != points.Count - 1 ? points[i + 1] : points[0];

                var line = new Line(start, end);
                linesList.Add(line);
            }

            return linesList;
        }
    }
}

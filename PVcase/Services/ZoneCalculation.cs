﻿using System;
using System.Windows;
using PVcase.Coordinates;
using PVcase.Models;

namespace PVcase.Services
{
    public class PanelCalculations
    {
        public Convertion Convertion = new Convertion();
        public Borders GetBorders(int[,] coordinates)
        {
            var borders = new Borders();

            for (int i = 0; i < coordinates.GetLength(0); ++i)
            {
                if (borders.BiggestX < coordinates[i, 0])
                    borders.BiggestX = coordinates[i, 0];

                if (borders.SmallestX > coordinates[i, 0])
                    borders.SmallestX = coordinates[i, 0];

                if (borders.BiggestY < coordinates[i, 1])
                    borders.BiggestY = coordinates[i, 1];

                if (borders.SmallestY > coordinates[i, 1])
                    borders.SmallestY = coordinates[i, 1];
            }

            return borders;
        }



        public Point Rectangles()
        {
            Point point = new Point();
            var siteBorders = GetBorders(Site.Coordinates);
            var points = Convertion.CoordinatesToPoints(Site.Coordinates).ToArray();
            for (int y = siteBorders.SmallestY; y < siteBorders.BiggestY; ++y)
            {
                for (int x = siteBorders.SmallestX; x < siteBorders.BiggestX; ++x)
                {
                    point = new Point(x, y);
                    var asd = isInside(points, points.Length, point);
                    if (asd == true)
                        return point;
                }
            }

            return point;
        }


        static bool isInside(Point[] polygon, int n, Point p)
        {
            // There must be at least 3 vertices in polygon[]
            if (n < 3)
            {
                return false;
            }

            int INF = 1000;
            // Create a point for line segment from p to infinite
            Point extreme = new Point(INF, p.Y);

            // Count intersections of the above line
            // with sides of polygon
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to
                // 'extreme' intersects with the line
                // segment from 'polygon[i]' to 'polygon[next]'
                if (doIntersect(polygon[i],
                    polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line
                    // segment 'i-next', then check if it lies
                    // on segment. If it lies, return true, otherwise false
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                    {
                        return onSegment(polygon[i], p,
                            polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise
            return (count % 2 == 1); // Same as (count%2 == 1)
        }

        static bool doIntersect(Point p1, Point q1,
            Point p2, Point q2)
        {
            // Find the four orientations needed for
            // general and special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases
            // p1, q1 and p2 are colinear and
            // p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are colinear and
            // q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are colinear and
            // p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are colinear and
            // q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2))
            {
                return true;
            }

            // Doesn't fall in any of the above cases
            return false;
        }
        static int orientation(Point p, Point q, Point r)
        {
            double val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);

            if (val == 0)
            {
                return 0; // colinear
            }
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        static bool onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) &&
                q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) &&
                q.Y >= Math.Min(p.Y, r.Y))
            {
                return true;
            }
            return false;
        }
    }
}

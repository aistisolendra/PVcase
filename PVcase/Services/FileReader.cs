using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using PVcase.Models;

namespace PVcase.Services
{
    public class FileReader
    {
        private readonly char[] _delimiters = { ',', ';', ':' };
        public List<Point> ReadFromFile(string path)
        {
            var coordinates = new List<Point>();

            if (!File.Exists(path))
                return coordinates;

            using (var sr = new StreamReader(path))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    var point = CreateNewPoint(line);

                    if (point != null)
                        coordinates.Add(point);
                }
            }

            return coordinates;
        }

        public Point CreateNewPoint(string line)
        {
            string[] values = line.Split(_delimiters);

            if (values.Length <= 1 || values.Length > 2)
                return null;

            return ValueParseCheck(values);
        }

        public Point ValueParseCheck(string[] values)
        {
            bool xResult = double.TryParse(values[0], out double x);
            bool yResult = double.TryParse(values[1], out double y);

            if (xResult && yResult)
                return new Point(x, y);

            return null;
        }

        public List<Point> PointsFromFile()
        {
            var fileDialog = new OpenFileDialog();

            return fileDialog.ShowDialog() == true ? ReadFromFile(fileDialog.FileName) : new List<Point>();
        }
    }
}

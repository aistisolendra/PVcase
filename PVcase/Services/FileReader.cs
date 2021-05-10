using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace PVcase.Services
{
    public class FileReader
    {
        private readonly char[] _delimiters = { ',', '.', ';', ':' };
        public List<Point> ReadFromFile(string path)
        {
            var coordinates = new List<Point>();

            using (var sr = new StreamReader(path))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                    coordinates.Add(CreateNewPoint(line));
            }

            return coordinates;
        }

        private Point CreateNewPoint(string line)
        {
            string[] values = line.Split(_delimiters);

            int.TryParse(values[0], out int x);
            int.TryParse(values[1], out int y);

            var point = new Point(x, y);
            return point;
        }

        public List<Point> PointsFromFile()
        {
            var fileDialog = new OpenFileDialog();

            return fileDialog.ShowDialog() == true ? ReadFromFile(fileDialog.FileName) : new List<Point>();
        }
    }
}

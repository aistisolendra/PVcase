using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using PVcase.Models;
using PVcase.Services;

namespace UnitTests
{
    public class FileReaderTests
    {
        private readonly string _threeCoordinatesFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
            "..\\..\\..\\",
            @"TestFiles\ThreeCoordinates.txt"));

        private readonly string _negativeCoordinateFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
            "..\\..\\..\\",
            @"TestFiles\NegativeCoordinates.txt"));

        private readonly string _typeDoubleCoordinateFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
            "..\\..\\..\\",
            @"TestFiles\TypeDoubleCoordinates.txt"));

        [Test]
        public void ReadFromFile_InputBadFilePath_ReturnsEmptyList()
        {
            var fileReader = new FileReader();
            var expected = new List<Point>();
            const string path = "";

            var result = fileReader.ReadFromFile(path);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadFromFile_InputThreeCoordinates_ReturnsPointList()
        {
            var fileReader = new FileReader();
            var expected = new List<Point>
            {
                new Point(11,22),
                new Point(33,44),
                new Point(55,66)
            };

            var result = fileReader.ReadFromFile(_threeCoordinatesFilePath);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadFromFile_InputNegativeCoordinates_ReturnsPointList()
        {
            var fileReader = new FileReader();
            var expected = new List<Point>
            {
                new Point(-11,22),
                new Point(-33,44),
                new Point(55,-66),
                new Point(88,-99)
            };

            var result = fileReader.ReadFromFile(_negativeCoordinateFilePath);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ReadFromFile_InputTypeDoubleCoordinates_ReturnsPointList()
        {
            var fileReader = new FileReader();
            var expected = new List<Point>
            {
                new Point(11.11,22.22),
                new Point(33.33,44.44),
                new Point(55.55,66.66)
            };

            var result = fileReader.ReadFromFile(_typeDoubleCoordinateFilePath);

            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void CreateNewPoint_InputEmptyLine_ReturnsNull()
        {
            var fileReader = new FileReader();
            string input = "";

            var result = fileReader.CreateNewPoint(input);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CreateNewPoint_InputThreeCoordinates_ReturnsNull()
        {
            var fileReader = new FileReader();
            string input = "8:9;10";

            var result = fileReader.CreateNewPoint(input);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void CreateNewPoint_InputTwoCoordinates_ReturnsTwoPoints()
        {
            var fileReader = new FileReader();
            string input = "8;5";
            var expected = new Point(8, 5);

            var result = fileReader.CreateNewPoint(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ValueParseCheck_InputTwoLetters_ReturnsNull()
        {
            var fileReader = new FileReader();
            string[] input = { "abc", "cba" };

            var result = fileReader.ValueParseCheck(input);

             Assert.AreEqual(null, result);
        }

        [Test]
        public void ValueParseCheck_InputTwoInts_ReturnsTwoInts()
        {
            var fileReader = new FileReader();
            string[] input = {"5", "4"};
            var expected = new Point(5, 4);

            var result = fileReader.ValueParseCheck(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ValueParseCheck_InputTwoDoubles_Returns()
        {
            var fileReader = new FileReader();
            string[] input = {"8.5", "7.5"};
            var expected = new Point(8.5,7.5);

            var result = fileReader.ValueParseCheck(input);

            Assert.AreEqual(expected,result);
        }
    }
}

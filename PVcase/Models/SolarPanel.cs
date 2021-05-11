using System;
using Caliburn.Micro;

namespace PVcase.Models
{
    public class SolarPanel : PropertyChangedBase, IEquatable<SolarPanel>
    {
        public Point OriginPoint { get; set; }

        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }

        private double _length;
        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                NotifyOfPropertyChange(() => Length);
            }
        }

        private double _tiltAngle;
        public double TiltAngle
        {
            get => _tiltAngle;
            set
            {
                _tiltAngle = value;
                NotifyOfPropertyChange(() => TiltAngle);
            }
        }

        private double _rowSpacing;
        public double RowSpacing
        {
            get => _rowSpacing;
            set
            {
                _rowSpacing = value;
                NotifyOfPropertyChange(() => RowSpacing);
            }
        }

        private double _columnSpacing;
        public double ColumnSpacing
        {
            get => _columnSpacing;
            set
            {
                _columnSpacing = value;
                NotifyOfPropertyChange(() => ColumnSpacing);
            }
        }

        public SolarPanel(double width = 0, double length = 0, int rowSpacing = 0,
            int columnSpacing = 0, int tiltAngle = 0, Point originPoint = null)
        {
            if (originPoint == null)
                originPoint = new Point();

            OriginPoint = originPoint;
            Length = length;
            Width = width;
            RowSpacing = rowSpacing;
            ColumnSpacing = columnSpacing;
            TiltAngle = tiltAngle;
        }

        public SolarPanel(double width = 0, double length = 0, int rowSpacing = 0,
            int columnSpacing = 0, int tiltAngle = 0, double x = 0, double y = 0)
        {
            OriginPoint = new Point(x,y);
            Length = length;
            Width = width;
            RowSpacing = rowSpacing;
            ColumnSpacing = columnSpacing;
            TiltAngle = tiltAngle;
        }

        public void ResetPanel()
        {
            this.Width = 0;
            this.Length = 0;
            this.ColumnSpacing = 0;
            this.RowSpacing = 0;
            this.TiltAngle = 0;
        }

        public bool Equals(SolarPanel other)
        {
            return other != null &&
                   this.Width == other.Width &&
                   this.Length == other.Length &&
                   this.ColumnSpacing == other.ColumnSpacing &&
                   this.RowSpacing == other.RowSpacing &&
                   this.TiltAngle == other.TiltAngle &&
                   this.OriginPoint.Equals(other.OriginPoint);
        }
    }
}

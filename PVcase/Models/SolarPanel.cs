using Caliburn.Micro;

namespace PVcase.Models
{
    public class SolarPanel : PropertyChangedBase
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

        public void ResetPanel()
        {
            this.Width = 0;
            this.Length = 0;
            this.ColumnSpacing = 0;
            this.RowSpacing = 0;
            this.TiltAngle = 0;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using PVcase.Models;
using PVcase.Services;

namespace PVcase.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly Converter _converter;
        private readonly PanelCalculations _panelCalculations;
        private readonly FileReader _fileReader;
        private readonly ZoneCalculations _zoneCalculations;

        private const int ScaleOnStartupConst = 2;
        private const string ErrorTextConst = "Bad values";
        public decimal MinScale => 0.1m;
        public decimal ScaleStep => 0.1m;
        public decimal MaxScale => 4m;

        public ShellViewModel(Converter converter, PanelCalculations panelCalculations,
                              FileReader fileReader, ZoneCalculations zoneCalculations)
        {
            _converter = converter;
            _panelCalculations = panelCalculations;
            _fileReader = fileReader;
            _zoneCalculations = zoneCalculations;

            CreateMenuPanel();
            SetScale();
        }

        public bool CheckIfDataIsValid()
        {
            return FileDataIsValid() &&
                   PanelDataIsValid();
        }

        private bool PanelDataIsValid()
        {
            return !(SolarPanelData.Width <= 0 ||
                     SolarPanelData.Length <= 0 ||
                     SolarPanelData.RowSpacing < 0 ||
                     SolarPanelData.ColumnSpacing < 0 ||
                     SolarPanelData.TiltAngle < 0 ||
                     SolarPanelData.TiltAngle > 60);
        }

        private bool FileDataIsValid()
        {
            return (_siteZonePoints.Count <= 0 ||
                _restrictionZonePoints.Count <= 0);
        }

        public void SetScale()
        {
            Scale = ScaleOnStartupConst;
        }

        public void CreateMenuPanel()
        {
            SolarPanelData = new SolarPanel();
            _siteZonePoints = new List<Point>();
            _restrictionZonePoints = new List<Point>();
        }

        public void Generate()
        {
            bool result = CheckIfDataIsValid();

            if (result)
            {
                SetErrorText(result);
                DrawZones();
                DrawSolarPanels();
            }
            else
                SetErrorText(result);
        }

        public void SetErrorText(bool isValid)
        {
            ErrorText = isValid ? string.Empty : ErrorTextConst;
        }

        public void OpenSiteFile()
        {
            _siteZonePoints = _fileReader.PointsFromFile();
        }

        public void OpenRestrictionFile()
        {
            _restrictionZonePoints = _fileReader.PointsFromFile();
        }


        public void DrawZones()
        {
            SiteLines.AddRange(_converter.PointsToLines(_siteZonePoints));
            RestrictionLines.AddRange(_converter.PointsToLines(_restrictionZonePoints));
        }

        public void DrawSolarPanels()
        {
            SolarPanels.Clear();
            var panelPlacingPoints = _panelCalculations.GetPlacingPoints(SolarPanelData, _siteZonePoints, _restrictionZonePoints, _zoneCalculations);

            foreach (var panel in panelPlacingPoints.Select(CreateNewSolarPanel))
            {
                SolarPanels.Add(panel);
            }

            SolarPanelData.ResetPanel();
        }

        private SolarPanel CreateNewSolarPanel(Point point)
        {
            var panel = new SolarPanel()
            {
                OriginPoint = new Point(point.X, point.Y),
                Length = SolarPanelData.Width,
                Width = SolarPanelData.Length
            };

            return panel;
        }

        private BindableCollection<SolarPanel> _solarPanels = new BindableCollection<SolarPanel>();
        public BindableCollection<SolarPanel> SolarPanels
        {
            get => _solarPanels;
            set => _solarPanels = value;
        }

        private BindableCollection<Line> _siteLines = new BindableCollection<Line>();
        public BindableCollection<Line> SiteLines
        {
            get => _siteLines;
            set => _siteLines = value;
        }

        private BindableCollection<Line> _restrictionLines = new BindableCollection<Line>();
        public BindableCollection<Line> RestrictionLines
        {
            get => _restrictionLines;
            set => _restrictionLines = value;
        }

        private SolarPanel _solarPanelData;
        public SolarPanel SolarPanelData
        {
            get => _solarPanelData;
            set
            {
                _solarPanelData = value;
                NotifyOfPropertyChange(() => SolarPanelData);
            }
        }

        private decimal _scale;
        public decimal Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                NotifyOfPropertyChange(() => Scale);
            }
        }

        private string _errorText;
        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                NotifyOfPropertyChange(() => ErrorText);
            }
        }

        private List<Point> _siteZonePoints;
        private List<Point> _restrictionZonePoints;
    }
}

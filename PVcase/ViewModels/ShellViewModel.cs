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
        public ShellViewModel(Converter converter, PanelCalculations panelCalculations,
                              FileReader fileReader, ZoneCalculations zoneCalculations)
        {
            _converter = converter;
            _panelCalculations = panelCalculations;
            _fileReader = fileReader;
            _zoneCalculations = zoneCalculations;

            CreateMenuPanel();
        }

        public void CreateMenuPanel()
        {
            SolarPanelData = new SolarPanel();
        }

        public void Generate()
        {
            DrawZones();
            DrawSolarPanels();
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
            set => _solarPanelData = value;
        }

        private List<Point> _siteZonePoints;
        private List<Point> _restrictionZonePoints;
    }
}

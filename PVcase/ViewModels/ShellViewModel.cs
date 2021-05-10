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
        public ShellViewModel(Converter converter, PanelCalculations panelCalculations, FileReader fileReader)
        {
            _converter = converter;
            _panelCalculations = panelCalculations;
            _fileReader = fileReader;

            CreateMenuPanel();
        }

        public void CreateMenuPanel()
        {
            SolarPanel = new SolarPanel();
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

        public void ResetMenuPanel()
        {
            SolarPanel.ResetPanel();
        }

        public void DrawZones()
        {
            SiteLines.AddRange(_converter.PointsToLines(_siteZonePoints));
            RestrictionLines.AddRange(_converter.PointsToLines(_restrictionZonePoints));
        }

        public void DrawSolarPanels()
        {
            SolarPanels.Clear();
            var panelPlacingPoints = _panelCalculations.GetPlacingPoints(SolarPanel, _siteZonePoints, _restrictionZonePoints);

            foreach (var panel in panelPlacingPoints.Select(CreateNewSolarPanel))
            {
                SolarPanels.Add(panel);
            }
            
            ResetMenuPanel();
        }

        private SolarPanel CreateNewSolarPanel(Point point)
        {
            var panel = new SolarPanel()
            {
                OriginPoint = new Point(point.X, point.Y),
                Length = SolarPanel.Width,
                Width = SolarPanel.Length
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

        private SolarPanel _solarPanel;
        public SolarPanel SolarPanel
        {
            get => _solarPanel;
            set => _solarPanel = value;
        }

        private List<Point> _siteZonePoints;
        private List<Point> _restrictionZonePoints;
    }
}

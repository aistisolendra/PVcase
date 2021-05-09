using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using PVcase.Models;
using PVcase.Services;

namespace PVcase.ViewModels
{
    public class ShellViewModel : Screen
    {
        public Converter Converter = new Converter();
        public PanelCalculations PanelService = new PanelCalculations();
        public ZoneCalculations ZoneService = new ZoneCalculations();
        public FileReader FileReader = new FileReader();
        public ShellViewModel()
        {
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
            _siteZonePoints = FileReader.PointsFromFile();
        }

        public void OpenRestrictionFile()
        {
            _restrictionZonePoints = FileReader.PointsFromFile();
        }

        public void ResetMenuPanel()
        {
            SolarPanel.Width = 0;
            SolarPanel.Length = 0;
            SolarPanel.ColumnSpacing = 0;
            SolarPanel.RowSpacing = 0;
            SolarPanel.TiltAngle = 0;
        }

        public void DrawZones()
        {
            SiteLines.AddRange(Converter.PointsToLines(_siteZonePoints));
            RestrictionLines.AddRange(Converter.PointsToLines(_restrictionZonePoints));
        }

        public void DrawSolarPanels()
        {
            SolarPanels.Clear();
            var panelPlacingPoints = PanelService.GetPlacingPoints(SolarPanel, _siteZonePoints, _restrictionZonePoints);

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

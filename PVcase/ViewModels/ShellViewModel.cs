using System.Windows;
using Caliburn.Micro;
using PVcase.Coordinates;
using PVcase.Models;
using PVcase.Services;

namespace PVcase.ViewModels
{
    public class ShellViewModel : Screen
    {
        public Convertion Convertion = new Convertion();
        public PanelCalculations PanelService = new PanelCalculations();
        public ZoneCalculations ZoneService = new ZoneCalculations();

        public ShellViewModel()
        {
            CreateMenuPanel();
            DrawSite();
            DrawRestriction();
        }

        public void CreateMenuPanel()
        {
            SolarPanel = new SolarPanel();
        }

        public void ResetMenuPanel()
        {
            SolarPanel.Width = 0;
            SolarPanel.Length = 0;
            SolarPanel.ColumnSpacing = 0;
            SolarPanel.RowSpacing = 0;
            SolarPanel.TiltAngle = 0;
        }

        public void DrawSite()
        {
            var siteCoordinationLines = Convertion.CoordinatesToLines(Site.Coordinates);

            foreach (var line in siteCoordinationLines)
            {
                SiteLines.Add(line);
            }
        }

        public void DrawRestriction()
        {
            var zoneCoordinationLines = Convertion.CoordinatesToLines(RestrictionZone.Coordinates);

            foreach (var line in zoneCoordinationLines)
            {
                RestrictionLines.Add(line);
            }
        }

        public void DrawSolarPanels()
        {
            SolarPanels.Clear();

            var sitePoints = Convertion.CoordinatesToPoints(Site.Coordinates);
            var restrictionPoints = Convertion.CoordinatesToPoints(RestrictionZone.Coordinates);
            var siteBorders = ZoneService.GetRange(Site.Coordinates);

            var originPoints = PanelService.GetPanelPoints(SolarPanel, siteBorders, sitePoints, restrictionPoints);

            foreach (var point in originPoints)
            {
                var panel = new SolarPanel()
                {
                    OriginPoint = new Point(point.X, point.Y),
                    Length = SolarPanel.Width,
                    Width = SolarPanel.Length
                };

                SolarPanels.Add(panel);
            }

            ResetMenuPanel();
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
    }
}

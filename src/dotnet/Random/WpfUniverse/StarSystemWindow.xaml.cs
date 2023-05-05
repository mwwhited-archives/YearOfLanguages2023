using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfUniverse
{
    /// <summary>
    /// Interaction logic for StarSystemWindow.xaml
    /// </summary>
    public partial class StarSystemWindow : Window
    {
        public StarSystemWindow()
        {
        }

        public StarSystemWindow(SimpleStarSystem simple)
        {
            InitializeComponent();

            system.Width = 1024;
            system.Height = 1024;

            // https://youtu.be/ZZY9YE7rZJw?t=1708

            var factor = 32.0;
            var sectorsX = (int)(system.Width / factor);
            var sectorsY = (int)(system.Height / factor);

            this.DataContext = simple;
            var star = (simple as StarSystem) ?? new StarSystem(simple);

            var maxDistance = star.Planets.Count == 0 ? 0 : star.Planets.Select(p => p.Diameter / 2 + p.Distance).Last();
            var realStarSize = star.Diameter * 100;

            var maxSize = maxDistance - realStarSize * .9;
            var scaleFactor = system.Width / maxSize;

            var starEllipse = new Ellipse()
            {
                Width = (realStarSize - 2) * scaleFactor,
                Height = (realStarSize - 2) * scaleFactor,
                Fill = new SolidColorBrush(star.Color.GetColor()),
                DataContext = star,
            };

            var starCenter = (
                y: system.Height / 2 - (realStarSize * scaleFactor) / 2,
                x: 0 - (realStarSize * scaleFactor) * .9
                );
            Canvas.SetTop(starEllipse, starCenter.y);
            Canvas.SetLeft(starEllipse, starCenter.x);
            system.Children.Add(starEllipse);

            foreach (var planet in star.Planets)
            {
                var planetEllipse = new Ellipse()
                {
                    Width =(planet.Diameter - 2) * scaleFactor,
                    Height = (planet.Diameter - 2) * scaleFactor,
                    Fill = new SolidColorBrush(planet.GetColor()),
                    DataContext = star,
                };

                var planetButton = new Button()
                {
                    Content = planetEllipse,
                };

                var planetCenter = (
                    y: system.Height / 2 - (planet.Diameter / 2 * scaleFactor),
                    x: (planet.Distance - realStarSize * .9 - planet.Diameter / 2) * scaleFactor
                    );
                Debug.WriteLine($"planetCenter: {planetCenter} / {planet}");
                //systemBtn.Click += SystemBtn_Click;
                Canvas.SetTop(planetButton, planetCenter.y);
                Canvas.SetLeft(planetButton, planetCenter.x);
                system.Children.Add(planetButton);
            }
        }
    }
}

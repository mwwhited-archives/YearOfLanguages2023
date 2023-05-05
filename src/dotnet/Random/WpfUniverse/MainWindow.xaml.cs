using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfUniverse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            world.Width = 1024;
            world.Height = 1024;

            // https://youtu.be/ZZY9YE7rZJw?t=1708

            var factor = 32.0;
            var sectorsX = (int)(world.Width / factor);
            var sectorsY = (int)(world.Height / factor);

            var possible = from x in Enumerable.Range(0, sectorsX)
                           from y in Enumerable.Range(0, sectorsY)
                           select new SimpleStarSystem(x, y);
            var exists = possible.Where(i => i.Exists).ToList();

            foreach (var star in exists)
            {
                var system = new Ellipse()
                {
                    Width = star.Diameter - 2,
                    Height = star.Diameter - 2,
                    Fill = new SolidColorBrush(star.Color.GetColor()),
                    DataContext = star,
                };

                var systemBtn = new Button()
                {
                    Content = system,
                };
                systemBtn.Click += SystemBtn_Click;

                //system.CommandBindings.Add(new bind)
                Canvas.SetTop(systemBtn, star.Y * factor + factor / 2 - star.Diameter / 2 + 1);
                Canvas.SetLeft(systemBtn, star.X * factor + factor / 2 - star.Diameter / 2 + 1);
                world.Children.Add(systemBtn);
            }

            //for (var x = 0; x < sectorsX; x++)
            //{
            //    var line = new Line()
            //    {
            //        X1 = x * factor,
            //        X2 = x * factor,
            //        Y1 = 0.0,
            //        Y2 = world.Height,
            //        Stroke = new SolidColorBrush(Colors.Green),
            //    };
            //    world.Children.Add(line);
            //}

            //for (var y = 0; y < sectorsY; y++)
            //{
            //    var line = new Line()
            //    {
            //        X1 = 0,
            //        X2 = world.Width,
            //        Y1 = y * factor,
            //        Y2 = y * factor,
            //        Stroke = new SolidColorBrush(Colors.Green),
            //    };
            //    world.Children.Add(line);
            //}
        }

        private void SystemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Content is Ellipse el && el.DataContext is SimpleStarSystem star)
            {
                new StarSystemWindow(star) { }.Show();
            }
        }

    }
}
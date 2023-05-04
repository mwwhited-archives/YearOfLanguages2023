using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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

            // https://youtu.be/ZZY9YE7rZJw?t=1708

            var sectorsX = (int)(world.ActualHeight / 16);
            var sectorsY = (int)(world.ActualWidth / 16);

            var possible = from x in Enumerable.Range(0, sectorsX)
                           from y in Enumerable.Range(0, sectorsY)
                           select new SimpleStarSystem(x, y);
            var exist = possible.Where(i => i.Exists).ToList();

            //var leh = new Lehmer32();
            //for (var x = 0; x < 10; x++)
            //{
            //    Debug.WriteLine(leh.Next());
            //}
        }
    }
}
using System.Windows;
using System.Windows.Media;

namespace WpfUniverse
{
    public static class ColorsExtensions
    {
        public static Color GetColor(this SimpleStarSystem star) => star.Color.GetColor();
        public static Color GetColor(this StarColors star) =>
            star switch
            {
                StarColors.Blue => Colors.Blue,
                StarColors.Orange => Colors.Orange,
                StarColors.Red => Colors.Red,
                StarColors.White => Colors.White,
                StarColors.Yellow => Colors.Yellow,
                _ => Colors.Salmon
            };

        public static Color GetColor(this Planet planet) =>
            Color.FromArgb(
                (byte)(planet.Gases * 255),
                (byte)(planet.Minerals * 255),
                (byte)(planet.Foliage * 255),
                (byte)(planet.Water * 255)
                );
    }
}
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Editor.Widgets.Editor
{
    public class PointsToGeometryConverter : IValueConverter
    {
        public static PointsToGeometryConverter Instance { get; } = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not ObservableCollection<Point> points || points.Count < 3)
                return null;

            var geometry = new StreamGeometry();

            using (var ctx = geometry.Open())
            {
                ctx.BeginFigure(points[0], false);

                int fullSegments = (points.Count - 1) / 2; 

                for (int i = 0; i < fullSegments; i++)
                {
                    int ctrlIndex = 1 + i * 2;
                    int endIndex = 2 + i * 2;

                    if (endIndex < points.Count)
                        ctx.QuadraticBezierTo(points[ctrlIndex], points[endIndex]);
                }

                ctx.EndFigure(false);
            }

            return geometry;
        }


        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

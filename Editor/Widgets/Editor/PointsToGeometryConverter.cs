using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            if (value is not ObservableCollection<Point> points || points.Count < 2)
                return null;

            var geometry = new StreamGeometry();

            using (var ctx = geometry.Open())
            {
                ctx.BeginFigure(points[0], false);

                if (points.Count == 2)
                {
                    ctx.LineTo(points[1]);
                }
                else
                {
                    Debug.WriteLine("qbc bze");
                    ctx.QuadraticBezierTo(points[1], points[2]);
                }

                ctx.EndFigure(false);
            }

            return geometry;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

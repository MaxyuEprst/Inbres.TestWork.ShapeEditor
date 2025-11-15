using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Editor.Entities.Shape.Models;
using System.Text.Json.Serialization;

namespace Editor.Entities.Shape.DTOs
{
    public class ShapeDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("points")]
        public List<PointDto> Points { get; set; } = new();

        public EditorShape? ToShape()
        {
            if (Type == "Oval")
            {
                return new OvalShape
                {
                    X = X,
                    Y = Y,
                    Width = Width,
                    Height = Height
                };
            }
            else if (Type == "Bezier")
            {
                var points = new ObservableCollection<Point>();
                foreach (var pointDto in Points)
                {
                    points.Add(new Point(pointDto.X, pointDto.Y));
                }

                return new BezCurShape
                {
                    X = X,
                    Y = Y,
                    Points = points
                };
            }

            return null;
        }
    }
}
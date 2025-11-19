using System.Text.Json;
using System.Collections.Generic;
using Editor.Entities.Shape.Models;

namespace Editor.Features.Saving
{
    public class ShapeSerializer
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            Converters = { new PointConverter() },
        };

        public string Serialize(IEnumerable<EditorShape> shapes)
        {
            return JsonSerializer.Serialize(shapes, Options);
        }

        public List<EditorShape> Deserialize(string json)
        {
            return JsonSerializer.Deserialize<List<EditorShape>>(json, Options)
                ?? new List<EditorShape>();
        }
    }
}
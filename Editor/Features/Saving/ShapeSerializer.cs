using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Editor.Entities.Shape.Models;
using Editor.Entities.Shape.DTOs;
using Editor.Features.Saving.JsonContexts;

namespace Editor.Features.Saving
{
    public class ShapeSerializer
    {
        public string Serialize(IEnumerable<EditorShape> shapes)
        {
            var shapeDtos = shapes.Select(shape => shape.ToDto()).ToList();

            return JsonSerializer.Serialize(shapeDtos, ShapeJsonContext.Default.ListShapeDto);
        }

        public List<EditorShape> Deserialize(string json)
        {
            var shapeDtos = JsonSerializer.Deserialize(json, ShapeJsonContext.Default.ListShapeDto);

            if (shapeDtos == null)
                return new List<EditorShape>();

            var result = new List<EditorShape>();
            foreach (var dto in shapeDtos)
            {
                var shape = dto.ToShape();
                if (shape != null)
                    result.Add(shape);
            }
            return result;
        }
    }
}
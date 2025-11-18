using System.Text.Json;
using System.Collections.Generic;
using Editor.Entities.Shape.Models;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Editor.Features.Saving
{
    public class ShapeSerializer
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            Converters = { new PointConverter() },
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { EnablePolymorphism }
            }
        };

        private static void EnablePolymorphism(JsonTypeInfo typeInfo)
        {
            if (typeInfo.Type == typeof(EditorShape))
            {
                typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "$type",
                    IgnoreUnrecognizedTypeDiscriminators = true,
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                    DerivedTypes =
                    {
                        new JsonDerivedType(typeof(OvalShape), "oval"),
                        new JsonDerivedType(typeof(BezCurShape), "bezier")
                    }
                };
            }
        }


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
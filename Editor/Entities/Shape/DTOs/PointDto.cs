using System.Text.Json.Serialization;

namespace Editor.Entities.Shape.DTOs
{
    public class PointDto
    {
        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }
    }
}
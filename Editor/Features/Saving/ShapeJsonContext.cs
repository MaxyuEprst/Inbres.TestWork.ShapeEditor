using System.Collections.Generic;
using System.Text.Json.Serialization;
using Editor.Entities.Shape.DTOs;

namespace Editor.Features.Saving.JsonContexts
{
    [JsonSerializable(typeof(List<ShapeDto>))]
    [JsonSerializable(typeof(ShapeDto))]
    [JsonSerializable(typeof(PointDto))]
    [JsonSerializable(typeof(List<PointDto>))]
    public partial class ShapeJsonContext : JsonSerializerContext
    {
    }
}
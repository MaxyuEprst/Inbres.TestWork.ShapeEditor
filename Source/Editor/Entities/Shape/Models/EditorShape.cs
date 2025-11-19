using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Shared;

namespace Editor.Entities.Shape.Models
{
    [JsonDerivedType(typeof(OvalShape), typeDiscriminator: "oval")]
    [JsonDerivedType(typeof(BezCurShape), typeDiscriminator: "bezier")]
    public abstract partial class EditorShape : ObservableObject
    {
        public abstract ShapeType Type { get; }

        [ObservableProperty]
        private double _x;

        [ObservableProperty]
        private double _y;

        [ObservableProperty]
        private double _width;

        [ObservableProperty]
        private double _height;
    }
}

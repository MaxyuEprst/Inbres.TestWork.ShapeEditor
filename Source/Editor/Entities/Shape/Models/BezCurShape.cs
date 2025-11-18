using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Shared;
using System.Collections.ObjectModel;
using System.Linq;

namespace Editor.Entities.Shape.Models
{
    public partial class BezCurShape : EditorShape
    {
        public override ShapeType Type => ShapeType.BezierCurve;

        [ObservableProperty]
        private ObservableCollection<Point> _points = new();

        public BezCurShape()
        {
            _points.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(Points));
            };
        }

    }
}

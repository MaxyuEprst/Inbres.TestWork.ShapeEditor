using System;
using System.Collections.ObjectModel;
using Avalonia;
using Editor.Entities.Shapes;

namespace Editor.Widgets.Editor
{
    internal class ShapeEditorModel
    {
        public ObservableCollection<EditorShape> Shapes { get; } = new();

        public EditorShape CreateShape(ShapeType shapeType, Point point, double width = 0, double height = 0)
        {
            EditorShape newShape = shapeType switch
            {
                ShapeType.Oval => new OvalShape {Width= width, Height= height, X = point.X, Y = point.Y },
                ShapeType.BezierCurve => new BezCurShape { X = point.X, Y = point.Y },
                _ => throw new NotImplementedException()
            };

            Shapes.Add(newShape);
            return newShape;
        }

        public void RemoveShape(EditorShape shape)
        {
            Shapes.Remove(shape);
        }

        public void ClearShapes()
        {
            Shapes.Clear();
        }
    }
}

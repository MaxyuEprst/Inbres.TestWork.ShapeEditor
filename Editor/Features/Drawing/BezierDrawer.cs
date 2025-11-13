using Avalonia;
using Editor.Entities.Shape.Models;
using Editor.Shared;

namespace Editor.Features.Drawing
{
    internal class BezierDrawer : IShapeDrawer
    {
        private readonly ShapeEditorModel _model;
        private BezCurShape? _currentShape;
        private bool _isBezierControlPhase;
        private int _bezierPointsCount;

        public bool IsDrawing { get; private set; }

        public BezierDrawer(ShapeEditorModel model)
        {
            _model = model;
        }

        public EditorShape? OnPointerPressed(Point position)
        {
            if (!_isBezierControlPhase)
            {
                var bez = new BezCurShape();
                _currentShape = bez;

                bez.Points.Add(position); 
                bez.Points.Add(position); 
                bez.Points.Add(position); 

                _bezierPointsCount = 1;
                IsDrawing = true;
                _isBezierControlPhase = true;

                return bez; 
            }

            if (_currentShape is BezCurShape bezier)
            {
                _bezierPointsCount++;
                int lastIndex = bezier.Points.Count - 1;

                if (_bezierPointsCount == 2)
                {
                    bezier.Points[lastIndex - 1] = position;
                }
                else if (_bezierPointsCount == 3)
                {
                    bezier.Points[lastIndex] = position;

                    var lastPoint = position;
                    bezier.Points.Add(lastPoint);
                    bezier.Points.Add(lastPoint);

                    _bezierPointsCount = 1;
                }
            }

            return null;
        }

        public void OnPointerMoved(Point position)
        {
            if (!_isBezierControlPhase || _currentShape is not BezCurShape bezier || bezier.Points.Count < 3)
                return;

            int lastIndex = bezier.Points.Count - 1;

            if (_bezierPointsCount == 1)
                bezier.Points[lastIndex - 1] = position;
            else if (_bezierPointsCount == 2)
                bezier.Points[lastIndex] = position;
        }

        public EditorShape? OnPointerReleased(Point position)
        {
            return null;
        }

        public void Cancel()
        {
            if (_currentShape is BezCurShape bezier && _bezierPointsCount == 1)
            {
                if (bezier.Points.Count >= 2)
                {
                    bezier.Points.RemoveAt(bezier.Points.Count - 1);
                    bezier.Points.RemoveAt(bezier.Points.Count - 1);
                }
            }

            IsDrawing = false;
            _isBezierControlPhase = false;
            _bezierPointsCount = 0;
            _currentShape = null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Editor.Entities.Shape.Models;
using Editor.Shared;

namespace Editor.Features.Drawing
{
    internal class OvalDrawer : IShapeDrawer
    {
        private readonly ShapeEditorModel _model;
        private EditorShape? _currentShape;
        private Point _startPoint;

        public bool IsDrawing { get; private set; }

        public OvalDrawer(ShapeEditorModel model)
        {
            _model = model;
        }

        public EditorShape? OnPointerPressed(Point position)
        {
            _startPoint = position;
            _currentShape = _model.CreateShape(ShapeType.Oval, position, 0, 0);
            IsDrawing = true;
            return _currentShape;
        }

        public void OnPointerMoved(Point position)
        {
            if (!IsDrawing || _currentShape == null) return;

            var x = Math.Min(_startPoint.X, position.X);
            var y = Math.Min(_startPoint.Y, position.Y);
            _currentShape.X = x;
            _currentShape.Y = y;
            _currentShape.Width = Math.Abs(position.X - _startPoint.X);
            _currentShape.Height = Math.Abs(position.Y - _startPoint.Y);
        }

        public EditorShape? OnPointerReleased(Point position)
        {
            if (_currentShape == null) return null;

            if (_currentShape.Width < 2 || _currentShape.Height < 2)
            {
                _model.RemoveShape(_currentShape);
                _currentShape = null;
                IsDrawing = false;
                return null;
            }

            var finished = _currentShape;
            _currentShape = null;
            IsDrawing = false;
            return finished;
        }

        public void Cancel()
        {
            if (_currentShape != null)
                _model.RemoveShape(_currentShape);
            _currentShape = null;
            IsDrawing = false;
        }
    }

}

using Avalonia;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Entities.Shape.Models;
using Editor.Shared;
using System;
using System.Collections.ObjectModel;

namespace Editor.ViewModels
{
    public partial class EditorViewModel : ViewModelBase
    {
        private readonly ShapeEditorModel _model;
        private Point _startPoint;
        private EditorShape? _currentShape;

        private bool _isBezierControlPhase = false;
        private int _bezierPointsCount = 0;


        [ObservableProperty]
        private bool _isDrawing = false;

        [ObservableProperty]
        private ShapeType _currentShapeType = ShapeType.None;

        [ObservableProperty]
        private EditorShape? _selectedShape;

        public EditorViewModel()
        {
            _model = new ShapeEditorModel();
        }
        public ObservableCollection<EditorShape> Shapes => _model.Shapes;

        [RelayCommand]
        private void ChangeMod(ShapeType shapeType)
        {
            CurrentShapeType = shapeType;
            CancelCurrentDrawing();
        }

        [RelayCommand]
        private void ClearAll()
        {
            _model.ClearShapes();
        }


        private void CompleteBezierDrawing()
        {
            if (_currentShape is BezCurShape bezier && _bezierPointsCount == 1)
            {
                bezier.Points.RemoveAt(bezier.Points.Count - 1);
                bezier.Points.RemoveAt(bezier.Points.Count - 1);
            }
            IsDrawing = false;
            _isBezierControlPhase = false;
            _bezierPointsCount = 0;
            _currentShape = null;
        }

        public void OnPointerPressed(Point position)
        {
            if (CurrentShapeType == ShapeType.None)
                return;

            if (CurrentShapeType == ShapeType.Oval)
            {
                _startPoint = position;
                IsDrawing = true;
                _currentShape = _model.CreateShape(ShapeType.Oval, position, 0, 0);
                return;
            }

            if (CurrentShapeType == ShapeType.BezierCurve)
            {
                if (!_isBezierControlPhase)
                {
                        var bez = new BezCurShape();
                    Shapes.Add(bez);
                    _currentShape = bez;

                    bez.Points.Add(position); 
                    bez.Points.Add(position);
                    bez.Points.Add(position); 

                    _bezierPointsCount = 1;
                    IsDrawing = true;
                    _isBezierControlPhase = true;
                }
                else if (_currentShape is BezCurShape bezier)
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

                        if (_isBezierControlPhase)
                        {
                            var lastPoint = position;
                            bezier.Points.Add(lastPoint); 
                            bezier.Points.Add(lastPoint); 
                        }

                        _bezierPointsCount = 1;
                    }
                }

            }
        }

        public void OnPointerMoved(Point position)
        {
            if (CurrentShapeType == ShapeType.None)
                return;

            if (CurrentShapeType == ShapeType.Oval)
            {
                if (!IsDrawing || _currentShape == null) return;
                UpdateOval(position);
            }
            else if (CurrentShapeType == ShapeType.BezierCurve)
            {
                if (_isBezierControlPhase && _currentShape is BezCurShape bezier && bezier.Points.Count >= 3)
                {
                    int lastIndex = bezier.Points.Count - 1;

                    if (_bezierPointsCount == 1)
                    {
                        bezier.Points[lastIndex - 1] = position;
                    }
                    else if (_bezierPointsCount == 2)
                    {
                        bezier.Points[lastIndex] = position;
                    }
                }
            }
        }


        public void OnPointerReleased(Point position)
        {
            if (CurrentShapeType == ShapeType.None) return;

            if (CurrentShapeType == ShapeType.Oval)
            {
                if (!IsDrawing || _currentShape == null)
                {
                    IsDrawing = false;
                    _currentShape = null;
                    return;
                }

                if (_currentShape.Width < 2 || _currentShape.Height < 2)
                {
                    _model.RemoveShape(_currentShape);
                }

                IsDrawing = false;
                _currentShape = null;
            }

        }

        private void UpdateOval(Point position)
        {
            var x = Math.Min(_startPoint.X, position.X);
            var y = Math.Min(_startPoint.Y, position.Y);
            var width = Math.Abs(position.X - _startPoint.X);
            var height = Math.Abs(position.Y - _startPoint.Y);

            if (_currentShape == null) return;

            _currentShape.X = x;
            _currentShape.Y = y;
            _currentShape.Width = width;
            _currentShape.Height = height;
        }


        private void CancelCurrentDrawing()
        {
            if (_currentShape != null)
            {
                if (_currentShape is BezCurShape || _currentShape is OvalShape)
                {
                    if (Shapes.Contains(_currentShape))
                        _model.RemoveShape(_currentShape);
                }

                _currentShape = null;
            }

            IsDrawing = false;
            _isBezierControlPhase = false;
        }

        internal void OnKeyPressed(Key key)
        {
            if (key == Key.Escape && (_isBezierControlPhase || IsDrawing))
            {
                CompleteBezierDrawing();
            }
        }
    }
}
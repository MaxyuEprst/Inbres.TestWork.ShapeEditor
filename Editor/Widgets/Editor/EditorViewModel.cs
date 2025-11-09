using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Entities.Shapes;
using Editor.Shared;
using Editor.Widgets.Editor;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Editor.ViewModels
{
    public partial class EditorViewModel : ViewModelBase
    {
        private readonly ShapeEditorModel _model;
        private Point _startPoint;
        private EditorShape? _currentDrawingShape;

        [ObservableProperty]
        private bool _isDrawing = false;

        [ObservableProperty]
        private ShapeType _currentShapeType = ShapeType.Oval;

        [ObservableProperty]
        private EditorShape? _selectedShape;

        [RelayCommand]
        private void SetOvalMode()
        {
            CurrentShapeType = CurrentShapeType == ShapeType.Oval ? ShapeType.None : ShapeType.Oval;
        }

        public EditorViewModel()
        {
            _model = new ShapeEditorModel();
            _model.Shapes.CollectionChanged += (s, e) => OnPropertyChanged(nameof(Shapes));
        }

        public ObservableCollection<EditorShape> Shapes => _model.Shapes;

        [RelayCommand]
        private void AddOval()
        {
            var oval = new OvalShape
            {
                X = 100,
                Y = 100,
                Width = 80,
                Height = 60
            };
            Shapes.Add(oval);
        }

        public void StartCreatingShape(Point position)
        {
            if (CurrentShapeType == ShapeType.None) return;

            _startPoint = position;
            IsDrawing = true;

            _currentDrawingShape = _model.CreateShape(CurrentShapeType, position, 0, 0);
        }

        public void UpdateShapeSize(Point currentPoint)
        {
            if (!IsDrawing || _currentDrawingShape == null) return;

            var x = System.Math.Min(_startPoint.X, currentPoint.X);
            var y = System.Math.Min(_startPoint.Y, currentPoint.Y);
            var width = System.Math.Abs(currentPoint.X - _startPoint.X);
            var height = System.Math.Abs(currentPoint.Y - _startPoint.Y);

            // Обновляем свойства фигуры
            _currentDrawingShape.X = x;
            _currentDrawingShape.Y = y;
            _currentDrawingShape.Width = width;
            _currentDrawingShape.Height = height;
        }

        public void FinishCreatingShape()
        {
            if (!IsDrawing) return;

            IsDrawing = false;

            if (_currentDrawingShape != null)
            {
                // Если фигура слишком маленькая, удаляем её
                if (_currentDrawingShape.Width < 5 || _currentDrawingShape.Height < 5)
                {
                    Shapes.Remove(_currentDrawingShape);
                }
                else
                {
                    // Выбираем созданную фигуру
                    SelectedShape = _currentDrawingShape;
                }

                _currentDrawingShape = null;
            }
        }
    }
}

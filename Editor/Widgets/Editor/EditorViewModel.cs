using Avalonia;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Entities.Shape.Models;
using Editor.Features.Drawing;
using Editor.Shared;
using System.Collections.ObjectModel;

namespace Editor.ViewModels
{
    public partial class EditorViewModel : ViewModelBase
    {
        private readonly ShapeEditorModel _model;
        private IShapeDrawer? _currentDrawer;
        private ShapeType _currentShapeType;

        public ObservableCollection<EditorShape> Shapes => _model.Shapes;

        [ObservableProperty]
        private EditorShape? _selectedShape;

        public ShapeType CurrentShapeType
        {
            get => _currentShapeType;
            set
            {
                if (_currentShapeType == value) return;
                _currentShapeType = value;
                CancelCurrentDrawing();
                SetDrawerForShape(value);
            }
        }

        public EditorViewModel()
        {
            _model = new ShapeEditorModel();
            SetDrawerForShape(ShapeType.None);
        }

        [RelayCommand]
        private void ChangeMod(ShapeType shapeType) => CurrentShapeType = shapeType;

        [RelayCommand]
        private void ClearAll() => _model.ClearShapes();

        private void SetDrawerForShape(ShapeType type)
        {
            _currentDrawer = type switch
            {
                ShapeType.Oval => new OvalDrawer(_model),
                ShapeType.BezierCurve => new BezierDrawer(_model),
                _ => null
            };
        }

        public void OnPointerPressed(Point position, PointerUpdateKind updateKind)
        {
            if (updateKind == PointerUpdateKind.RightButtonPressed)
            {
                _currentDrawer?.Cancel();
                return;
            }

            var shape = _currentDrawer?.OnPointerPressed(position);
            if (shape != null && !_model.Shapes.Contains(shape))
                _model.Shapes.Add(shape);
        }

        public void OnPointerMoved(Point position) =>
            _currentDrawer?.OnPointerMoved(position);

        public void OnPointerReleased(Point position)
        {
            var shape = _currentDrawer?.OnPointerReleased(position);
            if (shape != null && !_model.Shapes.Contains(shape))
                _model.Shapes.Add(shape);
        }

        private void CancelCurrentDrawing() => _currentDrawer?.Cancel();

        public void OnKeyPressed(Key key)
        {
            if (key == Key.Escape)
                _currentDrawer?.Cancel();
        }
    }
}
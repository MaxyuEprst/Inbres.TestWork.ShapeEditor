using Avalonia;

namespace Editor.Shared
{
    public interface IShapeDrawer
    {
        bool IsDrawing { get; }
        Editor.Entities.Shape.Models.EditorShape? OnPointerPressed(Point position);
        void OnPointerMoved(Point position);
        Editor.Entities.Shape.Models.EditorShape? OnPointerReleased(Point position);
        void Cancel();
    }
}

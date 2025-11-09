using Avalonia.Controls;
using Avalonia.Input;
using Editor.ViewModels;

namespace Editor
{
    public partial class EditorControl : UserControl
    {
        public EditorControl()
        {
            InitializeComponent();
            DataContext = new EditorViewModel();
        }
        private void OnCanvasPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (DataContext is EditorViewModel viewModel && sender is Border border)
            {
                var position = e.GetCurrentPoint(border).Position;

                viewModel.StartCreatingShape(position);

                e.Pointer.Capture(border);
            }
        }

        private void OnCanvasPointerMoved(object? sender, PointerEventArgs e)
        {
            if (DataContext is EditorViewModel viewModel && sender is Border border)
            {
                var currentPosition = e.GetCurrentPoint(border).Position;
                viewModel.UpdateShapeSize(currentPosition);
            }
        }

        private void OnCanvasPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (DataContext is EditorViewModel viewModel)
            {
                viewModel.FinishCreatingShape();
                e.Pointer.Capture(null);
            }
        }
    }
}

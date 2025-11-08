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
                var position = e.GetPosition(border);
                viewModel.CreateShapeAtPoint(position);
            }
        }
        //private void OnCanvasPointerMoved(object? sender, PointerEventArgs e)
        //{
        //    if (DataContext is EditorViewModel viewModel && sender is Border border)
        //    {
        //        var position = e.GetPosition(border);
        //        viewModel.UpdatePreview(position);
        //    }
        //}
    }
}

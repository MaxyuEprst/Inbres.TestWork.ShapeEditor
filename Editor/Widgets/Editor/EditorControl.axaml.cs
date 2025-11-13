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

            this.Focusable = true;
            this.Focus();
        }

        private void OnCanvasPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (DataContext is EditorViewModel vm && sender is Border border)
            {
                var point = e.GetCurrentPoint(border);
                var position = point.Position;
                var updateKind = point.Properties.PointerUpdateKind;

                vm.OnPointerPressed(position, updateKind);
                e.Pointer.Capture(border);
            }
        }

        private void OnCanvasPointerMoved(object? sender, PointerEventArgs e)
        {
            if (DataContext is EditorViewModel vm && sender is Border border)
            {
                var position = e.GetCurrentPoint(border).Position;
                vm.OnPointerMoved(position);
            }
        }

        private void OnCanvasPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (DataContext is EditorViewModel vm && sender is Border border)
            {
                var position = e.GetCurrentPoint(border).Position;
                vm.OnPointerReleased(position);
                e.Pointer.Capture(null);
            }
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (DataContext is EditorViewModel vm)
            {
                if (e.Key == Key.Escape)
                {
                    vm.OnPointerPressed(default, PointerUpdateKind.RightButtonPressed);
                }
            }
        }
    }
}
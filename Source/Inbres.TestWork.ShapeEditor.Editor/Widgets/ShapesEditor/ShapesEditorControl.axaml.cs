using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Inbres.TestWork.ShapeEditor.Editor.Widgets.ShapesEditor;

namespace Inbres.TestWork.ShapeEditor.Editor;

public partial class ShapesEditorControl : UserControl
{
    public ShapesEditorControl()
    {
        InitializeComponent();
        DataContext = new ViewModelBase();
    }
}
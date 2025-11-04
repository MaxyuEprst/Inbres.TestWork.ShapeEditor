using Avalonia.Controls;
using Editor.ViewModels;

namespace Editor {
    public partial class EditorControl : UserControl
    {
        public EditorControl()
        {
            InitializeComponent();
            DataContext = new EditorViewModel();
        }
    }
}

using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Input;
using Editor.Entities.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Editor.ViewModels
{
    public partial class EditorViewModel : ViewModelBase
    {
        public ObservableCollection<EditorShape> Shapes { get; set; }

        public EditorViewModel()
        {
            Shapes = new ObservableCollection<EditorShape>(new List<EditorShape>
            {
                new OvalShape { X = 50, Y = 100, Width = 40, Height = 40 },
                new OvalShape { X = 150, Y = 200, Width = 60, Height = 30 }
            });
        }

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

        public void AddOvalAtPosition(double x, double y)
        {
            var oval = new OvalShape
            {
                X = x + 40,
                Y = y + 30,
                Width = 80,
                Height = 60
            };
            Shapes.Add(oval);
        }
    }
}

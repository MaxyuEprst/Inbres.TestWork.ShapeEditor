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
                new OvalShape
                {
                    X = 10,
                    Y = 10,
                    Width = 10,
                    Height = 10
                },
            });
        }

        [RelayCommand]
        private void AddOval()
        {
            var oval = new OvalShape
            {
                X = 10,
                Y = 10,
                Width = 80,
                Height = 60
            };
            Shapes.Add(oval);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Entities.Shapes
{
    public abstract class EditorShape
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public abstract ShapeType Type { get; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public enum ShapeType
    {
        Oval,
        BezierCurve
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbres.TestWork.ShapeEditor.Editor.Entities.Shapes
{
    public abstract class Shape
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public abstract ShapeType Type { get; }
    }

    public enum ShapeType
    {
        Oval,
        BezierCurve
    }
}

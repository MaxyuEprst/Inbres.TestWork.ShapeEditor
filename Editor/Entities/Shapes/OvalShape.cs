namespace Editor.Entities.Shapes
{
    public class OvalShape : EditorShape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override ShapeType Type => ShapeType.Oval;
    }
}

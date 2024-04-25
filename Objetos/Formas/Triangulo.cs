using System.Drawing;

public class Triangulo : Objeto
{
    private static Bitmap _image = null;
    private static Bitmap image {
        get {
            if (_image is null)
            {
                int width = 100;
                int height = 100;
                _image = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(_image))
                {
                    Brush brush = Brushes.Orange;

                    Point[] points = {
                        new Point(width / 2, 0),
                        new Point(0, height),
                        new Point(width, height)
                    };

                    g.FillPolygon(brush, points);
                }
            }
            return _image;
        }
    }
    
    public Triangulo(PointF position, int valor = 500)
    {
        this.Name = "Triangulo";
        this.Valor = valor;
        this.Image = image;
        this.Move(position);
        this.Size = new SizeF(100, 100);
    }
    public Triangulo(int valor = 500) : this(new PointF(0, 0), valor) {}

    public override PointF Center => new PointF(
        Position.X + Size.Width / 2,
        (Position.Y + (Position.Y + Size.Height) * 2) / 3
        );

    public override Triangulo Clone()
        => new Triangulo(this.Valor);
}
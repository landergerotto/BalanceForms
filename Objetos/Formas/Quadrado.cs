using System.Drawing;

public class Quadrado : Objeto
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
                    Brush brush = Brushes.CadetBlue;

                    Point[] points = {
                        new Point(0, 0),
                        new Point(width, 0),
                        new Point(width, height),
                        new Point(0, height)
                    };

                    g.FillPolygon(brush, points);
                }
            }
            return _image;
        }
    }

    public Quadrado(PointF position, int valor = 500)
    {
        this.Name = "Quadrado";
        this.Valor = valor;
        this.Image = image;
        this.Move(position);
        this.Size = new SizeF(100, 100);
    }
    public Quadrado(int valor = 500) : this(new PointF(0, 0), valor) {}

    public override Quadrado Clone()
        => new Quadrado(this.Valor);
}
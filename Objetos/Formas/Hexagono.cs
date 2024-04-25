using System.Drawing;

public class Hexagono : Objeto
{
    private static Bitmap _image = null;
    private static Bitmap image
    {
        get
        {
            if (_image is null)
            {
                int width = 100;
                int height = 100;
                _image = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(_image))
                {
                    Brush brush = Brushes.Red;

                    Point[] points = {
                        new Point(width / 4, 0),
                        new Point(width * 3 / 4, 0),
                        new Point(width, height / 2),
                        new Point(width * 3 / 4, height),
                        new Point(width / 4, height),
                        new Point(0, height / 2)
                    };

                    g.FillPolygon(brush, points);
                }
            }
            return _image;
        }
    }

    public Hexagono(PointF position, int valor = 500)
    {
        this.Name = "Hexagono";
        this.Valor = valor;
        this.Image = image;
        this.Move(position);
        this.Size = new SizeF(100, 100);
    }
    public Hexagono(int valor = 500) : this(new PointF(0, 0), valor) { }

    public override Hexagono Clone()
        => new Hexagono(this.Valor);
}
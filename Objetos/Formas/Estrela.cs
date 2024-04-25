using System;
using System.Drawing;

public class Estrela : Objeto
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
                        new Point(width / 5 * 1, height),
                        new Point(width / 2, 0),
                        new Point(width / 5 * 4, height),
                        new Point(0, height / 5 * 2),
                        new Point(width, height / 5 * 2),
                    };
                    g.FillPolygon(brush, points);

                    Point[] cpoints = new Point[5];
                    int radius = 22;
                    for (int i = 0; i < 5; i++)
                    {
                        double angle = Math.PI * 2 / 5 * i + Math.PI / 12;
                        int x = (int)(width / 2 + radius * Math.Cos(angle));
                        int y = (int)(height / 2 + radius * Math.Sin(angle) + 7);
                        cpoints[i] = new Point(x, y);
                    }
                    g.FillPolygon(brush, cpoints);
                }
            }
            return _image;
        }
    }

    public Estrela(PointF position, int valor = 500)
    {
        this.Name = "Estrela";
        this.Valor = valor;
        this.Image = image;
        this.Move(position);
        this.Size = new SizeF(100, 100);
    }
    public Estrela(int valor = 500) : this(new PointF(0, 0), valor) {}

    public override Estrela Clone()
        => new Estrela(this.Valor);
}
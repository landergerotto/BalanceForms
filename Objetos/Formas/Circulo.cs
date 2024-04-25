using System.Drawing;

public class Circulo : Objeto
{
    private static Bitmap _image = null;
    private static Bitmap image
    {
        get
        {
            if (_image is null)
            {
                int diameter = 100;
                _image = new Bitmap(diameter, diameter);
                using (Graphics g = Graphics.FromImage(_image))
                {
                    Brush brush = Brushes.Green;

                    g.FillEllipse(brush, 0, 0, diameter, diameter);
                }
            }
            return _image;
        }
    }

    public Circulo(PointF position, int valor = 500)
    {
        this.Name = "Circulo";
        this.Valor = valor;
        this.Image = image;
        this.Move(position);
        this.Size = new SizeF(100, 100);
    }
    public Circulo(int valor = 500) : this(new PointF(0, 0), valor) {}

    public override Circulo Clone()
        => new Circulo(this.Position, this.Valor);
}
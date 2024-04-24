using System.Drawing;

public class Triangulo : Objeto
{
    public Triangulo(int valor = 500)
    {
        this.Name = "Triangulo";
        this.Valor = valor;
        this.image = Bitmap.FromFile("assets/trianglo.png");
        this.Position = new Point(500, 500);
        this.Size = new SizeF(100, 100);
    }
}
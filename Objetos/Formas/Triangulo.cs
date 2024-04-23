using System.Drawing;

public class Triangulo : Objeto
{
    public Triangulo(float valor = 500)
    {
        this.Name = "Triangulo";
        this.Valor = valor;
        this.image = Bitmap.FromFile("asstes/trianglo.png");
        this.Position = new Point(500, 500);
    }
}
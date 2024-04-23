using System.Drawing;

public class Balanca
{
    public Prato[] Pratos { get; private set; }
    public Prato Esquerdo => Pratos[0];
    public Prato Direito => Pratos[1];
    public Image Image { get; private set; }
    public PointF Position { get; set; }

    


    public virtual void Draw(Graphics g)
    {
        g.DrawImage(Image, Position, new SizeF(1300, 450));
    }

}
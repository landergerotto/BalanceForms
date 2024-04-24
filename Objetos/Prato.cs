using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Prato
{
    public RectangleF Hitbox { get; set; }
    public List<Objeto> Objetos { get; set; } = new List<Objeto>();
    public Balanca Balanca { get; set; }
    public int Peso => Objetos.Sum(obj => obj.Valor);
    public Pen Pen = Pens.Blue;

    public void Draw(Graphics g)
    {
        if (Balanca is not null)
            g.DrawRectangleOnScreen(this.Pen,
                RealHitbox((this.Equals(Balanca.Esquerdo) ? 1 : -1) * ((int)Balanca.Equilibrio - 2)));
    }

    private RectangleF RealHitbox(int equilibrio)
    {
        return new RectangleF(
                Hitbox.X + (equilibrio == 0 ? 0 : (Math.Sign(Balanca.X + Balanca.Height / 2 - Hitbox.X) * (Balanca.Width * 0.005f))),
                Hitbox.Y + Balanca.Height * 0.0852f * equilibrio,
                Hitbox.Width,
                Hitbox.Height
            );
    }
}
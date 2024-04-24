using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class Prato
{
    public RectangleF Hitbox { get; set; }
    public List<Objeto> Objetos { get; set; } = new List<Objeto>();
    public Balanca Balanca { get; set; }
    public int Peso => Objetos.Sum(obj => obj.Valor);

    public void Update() 
    { 
        
    }

    public void Draw(Graphics g)
    {
        g.DrawRectangle(Pens.Blue, Hitbox);
    }
}
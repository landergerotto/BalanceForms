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

    public int Count => Objetos.Count;

    public void Draw(Graphics g)
    {
        if (Balanca is not null)
        {
            Dictionary<Type, List<Objeto>> types = new();
        
            foreach (var obj in Objetos)
            {
                Type objType = obj.GetType();
                if (!types.ContainsKey(objType))
                    types[objType] = new List<Objeto>();
                types[objType].Add(obj);
            }

            RectangleF area = RealHitbox((this.Equals(Balanca.Esquerdo) ? 1 : -1) * ((int)Balanca.Equilibrio - 2));
            SizeF size = new SizeF(50, 50);
            float x = area.X;
            float y = area.Y + area.Height - size.Height;

            int max_column = (int)(Hitbox.Width / size.Width);
            float error = Hitbox.Width % size.Width;
            int column = 0;
            int line = 0;
            foreach (var type in types)
            {
                PointF position = new PointF(x + (size.Width * column) + error / 2, y - (size.Height * line));
                var obj = type.Value[0].Clone();
                obj.Size = size;
                obj.Move(position);
                obj.Draw(g);

                float fontsize = 12;
                PointF center = obj.Center;
                string text = type.Value.Count.ToString();
                RectangleF textRect = ClientScreen.OnScreen(
                    center.X - (fontsize / 2) * text.Length,
                    center.Y - (fontsize / 2) * text.Length,
                    fontsize * text.Length, fontsize
                );

                Font font = new Font("Arial", textRect.Height);
                SolidBrush brush = new SolidBrush(Color.Black);

                g.DrawString(text, font, brush, new PointF(textRect.X, textRect.Y));
                
                column++;
                if (!(column < max_column))
                {
                    column = 0;
                    line++;
                }
            }
            // g.DrawRectangleOnScreen(this.Pen,
            //     RealHitbox((this.Equals(Balanca.Esquerdo) ? 1 : -1) * ((int)Balanca.Equilibrio - 2)));
        }
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
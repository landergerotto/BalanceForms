using System;
using System.Drawing;

public abstract class Objeto
{
    public string Name { get; protected set; }
    public float Valor { get; protected set; }
    public Image image { get; protected set; }
    public Rectangle Hitbox { get; protected set; }
    public PointF Position { get; set; }

    public virtual void Draw(Graphics g)
    {
        g.DrawImage(image, Position, new SizeF(100, 100));
    }

    public virtual void Move(PointF position)
    {
        throw new NotImplementedException();
    }
}
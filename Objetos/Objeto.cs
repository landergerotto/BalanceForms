using System;
using System.Drawing;

public abstract class Objeto
{
    public string Name { get; protected set; }
    public int Valor { get; protected set; }
    public Image Image { get; protected set; }
    public SizeF Size { get; set; }
    public PointF Position { get; private set; }
    
    public float X => Position.X;
    public float Y => Position.Y;
    public float Width => Size.Width;
    public float Height => Size.Height;
    public RectangleF Hitbox => new RectangleF(Position, Size);

    public virtual PointF Center => new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);

    public virtual void Draw(Graphics g)
    {
        g.DrawImageOnScreen(Image, Position, Size);
    }

    public virtual void Move(PointF position)
        => this.Position = position;

    public abstract Objeto Clone();
}
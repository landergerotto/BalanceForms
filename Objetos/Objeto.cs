using System;
using System.Drawing;

public abstract class Objeto
{
    public string Name { get; protected set; }
    public int Valor { get; protected set; }
    public Image image { get; protected set; }
    public SizeF Size { get; set; }
    public PointF Position { get; set; }
    
    public float Width => Size.Width;
    public float Height => Size.Height;
    public RectangleF Hitbox => new RectangleF(Position, Size);

    public virtual void Update()
    {

    }

    public virtual void Draw(Graphics g)
    {
        g.DrawImageOnScreen(image, Position, Size);
    }

    public virtual void Move(PointF position)
    {
        throw new NotImplementedException();
    }
}
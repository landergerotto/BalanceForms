using System.Drawing;

public class Balanca
{
    public Prato[] Pratos { get; private set; }
    public Prato Esquerdo => Pratos[0];
    public Prato Direito => Pratos[1];
    public Image Image { get; private set; }
    public PointF Position { get; set; }
    public SizeF Tamanho { get; set; } = new SizeF(800, 450);
    public Equilibrio Equilibrio { get; set; } = (Equilibrio)2;

    public float X => Position.X;
    public float Y => Position.Y;
    public float Width => Tamanho.Width;
    public float Height => Tamanho.Height;

    public Balanca(float x, float y, float width = 800, float height = 450)
    {
        this.Position = new PointF(x, y);
        this.Tamanho = new SizeF(width, height);

        this.Pratos = new Prato[] { new Prato(), new Prato() };
        Esquerdo.Balanca = this;
        Direito.Balanca = this;

        SizeF size = new SizeF(Width * 0.197699f, Height * 0.444444f);

        Esquerdo.Hitbox = new RectangleF(X + (Width * 0.08f), Y + (Height * 0.254f) - size.Height, size.Width, size.Height);
        Direito.Hitbox = new RectangleF(X + (Width * 0.72f), Y + (Height * 0.254f) - size.Height, size.Width, size.Height);

        this.Image = Bitmap.FromFile("assets/Balanca/balanca2.png");
    }

    public void Update()
    {
        int sum1 = Esquerdo.Peso;
        int sum2 = Direito.Peso;
        int temp = (int)Equilibrio;

        Equilibrio = (Equilibrio)(sum1.CompareTo(sum2) + 2);
        if (temp != (int)Equilibrio && temp != 2 && (int)Equilibrio != 2)
            Equilibrio = Equilibrio.Equilibrado;

        this.Image = Bitmap.FromFile($"assets/Balanca/balanca{(int)Equilibrio}.png");
        foreach (var prato in Pratos)
            prato.Update();
    }

    public void Draw(Graphics g)
    {
        Update();
        g.DrawImage(Image, Position, Tamanho);
        Esquerdo.Draw(g);
        Direito.Draw(g);
    }

}
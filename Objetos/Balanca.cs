using System.Drawing;

public class Balanca
{
    public Prato[] Pratos { get; private set; }
    public Prato Esquerdo => Pratos[0];
    public Prato Direito => Pratos[1];
    public Image Image { get; private set; }
    public PointF Position { get; set; }
    public SizeF Tamanho { get; set; } = new SizeF(800, 450);
    public Equilibrio equilibrio { get; set; } = (Equilibrio)2;

    public Balanca(float x, float y, float width = 800, float height = 450)
    {
        this.Position = new PointF(x, y);
        this.Tamanho = new SizeF(width, height);

        this.Pratos = new Prato[] { new Prato(), new Prato() };
        Esquerdo.Balanca = this;
        Direito.Balanca = this;
        Esquerdo.Hitbox = new RectangleF(63, 365, Tamanho.Width * 0.197699f, Tamanho.Height * 0.444444f);
        Direito.Hitbox = new RectangleF(1750, 450, 100, 100);

        this.Image = Bitmap.FromFile("assets/Balanca/balanca2.png");
    }

    public void Update()
    {
        int sum1 = 0;
        int sum2 = 0;
        foreach (var forma in Esquerdo.Objetos)
            sum1 += forma.Valor;

        foreach (var forma in Direito.Objetos)
            sum2 += forma.Valor;

        if (sum1 > sum2)
            equilibrio = Equilibrio.Pesado;
        else if (sum2 > sum1)
            equilibrio = Equilibrio.Leve;
        else
            equilibrio = Equilibrio.Equilibrado;

        switch (equilibrio)
        {
            case Equilibrio.Leve:
                // iamgem igual a balanca em diaginal correta
                break;

            case Equilibrio.Pesado:
                // iamgem igual a balanca em diaginal invertida
                break;

            case Equilibrio.Equilibrado:
                // iamgem igual a balanca horizontal
                break;
        }

    }

    public void Draw(Graphics g)
    {
        g.DrawImage(Image, Position, Tamanho);
        Esquerdo.Draw(g);
        Direito.Draw(g);
    }

}
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class Balanca
{
    public Prato[] Pratos { get; private set; }
    public Prato Esquerdo => Pratos[0];
    public Prato Direito => Pratos[1];
    public Image Image { get; private set; }
    public PointF Position { get; set; }
    public SizeF Tamanho { get; set; } = new SizeF(800, 450);
    public Equilibrio Equilibrio { get; set; } = (Equilibrio)2;
    public int Testes { get; private set; } = 0;
    public RectangleF BotaoTeste { get; set; }

    public float X => Position.X;
    public float Y => Position.Y;
    public float Width => Tamanho.Width;
    public float Height => Tamanho.Height;
    public int Count => Pratos.Sum(prato => prato.Count);

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

        var dim = Utils.ProportionalSize(Image, new SizeF(width, height));
        this.BotaoTeste = new RectangleF(X + dim.Width * .25f, Y + dim.Height, dim.Width / 2, dim.Height * .25f);
    }

    public void Update()
    {
        this.Image = Bitmap.FromFile($"assets/Balanca/balanca{(int)Equilibrio}.png");
    }

    public void Draw(Graphics g)
    {
        g.DrawImageOnScreen(Image, Position, Tamanho);
        Esquerdo.Draw(g);
        Direito.Draw(g);
        g.DrawRectangleOnScreen(Pens.Red, BotaoTeste);

        string text = "Pesar";
        Font font = new Font("Arial", 12);
        Brush brush = Brushes.Black;

        float centerX = BotaoTeste.X + BotaoTeste.Width / 2;
        float centerY = BotaoTeste.Y + BotaoTeste.Height / 2;

        SizeF textSize = g.MeasureString(text, font);
        PointF textPosition = new PointF(centerX - textSize.Width / 2, centerY - textSize.Height / 2);

        g.DrawText(text, textPosition, font, brush);
    }

    public void Testar()
    {
        int sum1 = Esquerdo.Peso;
        int sum2 = Direito.Peso;

        Equilibrio = (Equilibrio)(sum1.CompareTo(sum2) + 2);
        this.Testes++;
    }
}
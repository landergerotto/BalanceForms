using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Tutorial : IGame
{
    private List<Objeto> objetosJogo = new List<Objeto>();
    public List<Objeto> ObjetosJogo => objetosJogo;


    private Balanca[] balancas = new Balanca[2];
    public Balanca[] Balancas => balancas;

    private List<Objeto> mesa = new List<Objeto>();
    public List<Objeto> Mesa => mesa;
    private RectangleF mesaRect;


    public Dictionary<Objeto, int> Formas = new();

    public List<int> QuantidadeObjeto => QuantidadeObjeto;


    public Dictionary<Type, List<Objeto>> MesaTypes
    {
        get
        {
            Dictionary<Type, List<Objeto>> types = new();

            foreach (var obj in Mesa)
            {
                Type objType = obj.GetType();
                if (!types.ContainsKey(objType))
                    types[objType] = new List<Objeto>();
                types[objType].Add(obj);
            }

            return types;
        }
    }

    public TestResult result { get; set; }

    private HttpRequester requester = new("http://127.0.0.1:5000/");
    private Respostas apiResponse;
    public Tutorial()
    {
        var b1 = new Balanca(0, 450);
        var b2 = new Balanca(800, 450);

        balancas[0] = b1; balancas[1] = b2;

        Formas[new Circulo(new PointF(0, 0), 300)] = 5;
        Formas[new Quadrado(new PointF(125, 0), 400)] = 5;
        Formas[new Triangulo(new PointF(250, 0), 500)] = 5;
        Formas[new Estrela(new PointF(375, 0), 600)] = 5;
        Formas[new Hexagono(new PointF(500, 0), 700)] = 5;

        float x0 = ClientScreen.Width;
        float x1 = 0;
        float y0 = ClientScreen.Height;
        float y1 = 0;
        foreach (var obj in Formas)
        {
            Objeto _objeto = obj.Key;
            x0 = Math.Min(x0, _objeto.X);
            x1 = Math.Max(x1, _objeto.X + _objeto.Width);
            y0 = Math.Min(y0, _objeto.Y);
            y1 = Math.Max(y1, _objeto.Y + _objeto.Height);
            for (int i = 0; i < obj.Value; i++)
            {
                Objeto objeto = _objeto.Clone();
                Mesa.Add(objeto);
                objetosJogo.Add(objeto);
            }
        }

        SizeF mesa_size = new SizeF(x1 - x0, y1 - y0);
        PointF mesa_pos = new PointF(x0 + ClientScreen.Center.X - mesa_size.Width / 2, y0 + ClientScreen.Height - mesa_size.Height - 50);
        foreach (var obj in Mesa)
            obj.Move(new PointF(obj.Position.X + mesa_pos.X - x0, obj.Position.Y + mesa_pos.Y - y0));


        float rectBorder = 25;
        this.mesaRect = new RectangleF(
            new PointF(mesa_pos.X - rectBorder, mesa_pos.Y - rectBorder),
            new SizeF(mesa_size.Width + rectBorder * 2, mesa_size.Height + rectBorder * 2)
        );
    }
    public async void Update(Panel panel, string nome, string nasc)
    {
        await TestRequestAsync();
        foreach (var balanca in balancas)
            balanca.Update();

        foreach (var type in MesaTypes)
        {
            PointF position = type.Value[0].Position;
            foreach (var obj in type.Value)
            {
                if (ClientCursor.Objeto != obj)
                    obj.Move(position);
            }
        }
    }

    public void Draw(Graphics g)
    {

        string titulo = "Bem-vindo ao Tutorial";
        Font fonte = new Font("Arial", 20);
        Brush brush1 = Brushes.Black;
        g.DrawText(titulo, (ClientScreen.Width - g.MeasureString(titulo, fonte).Width) / 2, 20, fonte, brush1);

        fonte = new Font("Arial", 12);
        string comentario = "Aqui você tem 5 figuras geométricas, cada figura tem um peso, sabendo que o triângulo tem o peso de 500, descubra o peso \ndas outras figuras colocando nas balanças. Duas figuras tem o peso menor de 500 e duas maior de 500.";
        string importante = "Importante: Quando a figura é colocada na balança, você não consegue removê-la.";
        string aviso = "Aviso: Esta é uma fase de teste para você entender o funcionamento. Coloque os valores e envie para passar para a próxima fase.";

        g.DrawText(comentario, (ClientScreen.Width - g.MeasureString(comentario, fonte).Width) / 2, 100, fonte, brush1);
        g.DrawText(importante, (ClientScreen.Width - g.MeasureString(importante, fonte).Width) / 2, 150, fonte, brush1);
        g.DrawText(aviso, (ClientScreen.Width - g.MeasureString(aviso, fonte).Width) / 2, 200, fonte, Brushes.Red);

        foreach (var balanca in balancas)
            balanca.Draw(g);

        SolidBrush rectbrush = new SolidBrush(Color.LightGray);
        g.FillRectangle(rectbrush, this.mesaRect.OnScreen());
        rectbrush.Dispose();

        foreach (var obj in ObjectManager.Objetos)
            obj.Draw(g);

        foreach (var type in MesaTypes)
        {
            var obj = type.Value[0];
            float fontsize = 15;
            PointF center = obj.Center;
            string text = type.Value.Count.ToString();
            RectangleF textRect = ClientScreen.OnScreen(
                center.X - (fontsize / 2) * text.Length,
                center.Y - (fontsize / 2) * text.Length,
                fontsize * text.Length, fontsize
            );

            Font font = new Font("Arial", textRect.Height);
            SolidBrush brush = new SolidBrush(Color.Black);

            g.DrawString(
                (type.Value.Count - (ClientCursor.Objeto?.GetType() == obj.GetType() ? 1 : 0)).ToString(),
                font, brush,
                new PointF(textRect.X, textRect.Y));
        }
    }
    private async Task TestRequestAsync()
    {
        string a = await requester.GetAsync("test");
        var b = Json.DeserializeResponse(a);
        this.apiResponse = b.response;
    }

    public void Enviar(Panel panel, string nome, string nasc, Form form)
    {
        if (apiResponse != Respostas.Comecado)
        {
            string info = "";
            int valor = 200;
            foreach (Control control in panel.Controls)
            {
                if (valor == 500)
                    valor += 100;

                if (control is TextBox)
                {
                    var text = ((TextBox)control).Text;
                    if (text is "")
                        text = "0";
                    info += text + " - " + valor.ToString() + "\n";
                    valor += 100;
                }
            }
            MessageBox.Show(info, "Informações dos Inputs");
        }

        if (apiResponse == Respostas.Comecado)
        {
            MessageBox.Show("O desafio de verdade começa agora.", "Aviso");
            GameEngine.Current.ChangeLevel(panel, result);
        }
    }
}

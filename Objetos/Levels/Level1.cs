using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

public class Level1 : IGame
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
    private int count = 0;
    private List<int> Valores = new List<int> {1000, 750, 200, 100};

    private int Testes = 0;
    private RectangleF botaoTeste;
    public RectangleF BotaoTeste { get => botaoTeste; set => botaoTeste = value; }

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
    private bool timerCount = false;

    public Level1()
    {
        var b1 = new Balanca(0, 450);
        var b2 = new Balanca(800, 450);

        balancas[0] = b1;
        balancas[1] = b2;

        Utils.Shuffle(this.Valores);

        Formas[new Circulo(new PointF(0, 0), Valores[1])] = 5;
        Formas[new Quadrado(new PointF(125, 0), Valores[0])] = 5;
        Formas[new Triangulo(new PointF(250, 0), 500)] = 5;
        Formas[new Estrela(new PointF(375, 0), Valores[2])] = 5;
        Formas[new Hexagono(new PointF(500, 0), Valores[3])] = 5;

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
        PointF mesa_pos = new PointF(
            x0 + ClientScreen.Center.X - mesa_size.Width / 2,
            y0 + ClientScreen.Height - mesa_size.Height - 50
        );
        foreach (var obj in Mesa)
            obj.Move(
                new PointF(obj.Position.X + mesa_pos.X - x0, obj.Position.Y + mesa_pos.Y - y0)
            );

        float rectBorder = 25;
        this.mesaRect = new RectangleF(
            new PointF(mesa_pos.X - rectBorder, mesa_pos.Y - rectBorder),
            new SizeF(mesa_size.Width + rectBorder * 2, mesa_size.Height + rectBorder * 2)
        );

        var x = ClientScreen.Center.X;
        var width = 300;
        this.BotaoTeste = new RectangleF(x - width / 2, 750, width, 100);

    }

    public async void Update(Panel panel, string nome, string nasc)
    {
        if (!timerCount)
        {
            TestTimer.Start();
            timerCount = true;
        }
        await TestRequestAsync(panel, nome, nasc);
        foreach (var balanca in balancas)
        {
            balanca.Update();
        }

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
        string tituloN1 = "Bem-vindo ao Nível 1";
        Font fonte = new Font("Arial", 20);
        Brush brush1 = Brushes.Black;
        g.DrawText(tituloN1, (ClientScreen.Width - g.MeasureString(tituloN1, fonte).Width) / 2, 20, fonte, brush1);

        fonte = new Font("Arial", 12);
        string comentario = "1- Aqui você tem 5 figuras geométricas, cada figura tem um peso, sabendo que o triângulo tem o peso de 500, descubra o peso \ndas outras figuras colocando nas balanças.";
        string msg = "Você está no nível 1. Tome cuidado para não usar peças demais.";
        string importante = "Importante: Quando a figura é colocada na balança, você não consegue removê-la.";


        g.DrawText(comentario, (ClientScreen.Width - g.MeasureString(comentario, fonte).Width) / 2, 100, fonte, brush1);
        g.DrawText(msg, (ClientScreen.Width - g.MeasureString(msg, fonte).Width) / 2, 150, fonte, brush1);
        g.DrawText(importante, (ClientScreen.Width - g.MeasureString(importante, fonte).Width) / 2, 200, fonte, brush1);



        foreach (var balanca in balancas)
            balanca.Draw(g);

        SolidBrush rectbrush = new SolidBrush(Color.LightGray);
        g.FillRectangle(rectbrush, this.mesaRect.OnScreen());
        rectbrush.Dispose();

        foreach (var obj in ObjectManager.Objetos)
        {
            obj.Draw(g);
        }

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

        Brush boschBlue = new SolidBrush(Color.FromArgb(0, 82, 155));
        g.DrawRectangleOnScreen(boschBlue, BotaoTeste);

        string textp = "Pesar";
        Font fontp = new Font("Arial", 18);
        Brush brushp = Brushes.White;

        float centerX = BotaoTeste.X + BotaoTeste.Width / 2;
        float centerY = BotaoTeste.Y + BotaoTeste.Height / 2;

        SizeF textSize = g.MeasureString(textp, fontp);
        PointF textPosition = new PointF(centerX - textSize.Width / 2, centerY - textSize.Height / 2);

        g.DrawText(textp, textPosition, fontp, brushp);
    }

    private async Task TestRequestAsync(Panel panel, string nome, string nasc)
    {
        string a = await requester.GetAsync("test");
        var b = Json.DeserializeResponse(a);
        this.apiResponse = b.response;

        if (apiResponse == Respostas.Parou)
        {
            if (count > 0)
                return;

            this.result = BuildJson(panel, nome, nasc);

            var serialized = Json.SerializeToJson(this.result);
            await requester.PostAsync("test", serialized);
            count++;
            MessageBox.Show("O instrutor finalizou o teste. Suas respostas foram enviadas automaticamente e você não pode mais enviá-las");
        }
    }

    public void Enviar(Panel panel, string nome, string nasc, Form form)
    {
        if (apiResponse == Respostas.Comecado)
        {
            this.result = BuildJson(panel, nome, nasc);

            ConfirmationForm confirmationForm = new ConfirmationForm("Tem certeza que você quer enviar? Isso o fará avançar de nível.");
            DialogResult result = confirmationForm.ShowDialog();
            if (result == DialogResult.Yes)
                GameEngine.Current.ChangeLevel(panel, this.result);

            else
            {
                MessageBox.Show(
                    "Você está no nível 1. Você não avançou.",
                    "Informação"
                );
                return;
            }
        }
    }

    private TestResult BuildJson(Panel panel, string nome, string nasc)
    {
        int ind = 0;
        TextBox[] textboxes = new TextBox[4];

        for (int i = 0; i < panel.Controls.Count; i++)
        {
            if (i == 1)
                continue;

            if (panel.Controls[i] is TextBox)
            {
                textboxes[ind] = (TextBox)panel.Controls[i];
                ind++;
            }
        }

        textboxes[0].Text = textboxes[0].Text == "" ? "0" : textboxes[0].Text;
        textboxes[1].Text = textboxes[1].Text == "" ? "0" : textboxes[1].Text;
        textboxes[2].Text = textboxes[2].Text == "" ? "0" : textboxes[2].Text;
        textboxes[3].Text = textboxes[3].Text == "" ? "0" : textboxes[3].Text;

        float acertos = 0;

        int quadrado = 0;
        int circulo = 0;
        int estrela = 0;
        int hexagono = 0;

        if (textboxes[0].Text == Valores[0].ToString() && textboxes[0].Text != "0")
        {
            quadrado = 2;
            acertos++;
        }
        else
            quadrado = 1;

        if (textboxes[1].Text == Valores[1].ToString() && textboxes[1].Text != "0")
        {
            circulo = 2;
            acertos++;
        }
        else
            circulo = 1;

        if (textboxes[2].Text == Valores[2].ToString() && textboxes[1].Text != "0")
        {
            estrela = 2;
            acertos++;
        }
        else
            estrela = 1;

        if (textboxes[3].Text == Valores[3].ToString() && textboxes[3].Text != "0")
        {
            hexagono = 2;
            acertos++;
        }
        else
            hexagono = 1;

        var json = new TestResult
        {
            nome = nome,
            nascimento = nasc,
            prova1 = new Prova
            {
                triangulo = 500,
                quadrado = quadrado,
                circulo = circulo,
                estrela = estrela,
                hexagono = hexagono,
                tempo = (int)TestTimer.Stop().TotalSeconds,
                quantidade = Balancas.Sum(balanca => balanca.Count),
                tentativas = this.Testes,
                acertos = acertos / 4
            },
            prova2 = new Prova { },
        };

        return json;
    }

    public int lascount = 0;
    public void Testar()
    {
        foreach (var balanca in balancas)
        {
            balanca.Testar();
        }
        int count = balancas.Sum(balanca => balanca.Count);
        if (lascount != count)
            this.Testes++;
        lascount = count;
        
    }
}

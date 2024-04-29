using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Level1 : IGame
{
    private List<Objeto> objetosJogo = new List<Objeto>();
    public List<Objeto> ObjetosJogo => objetosJogo;

    private Balanca[] balancas = new Balanca[2];
    public Balanca[] Balancas => balancas;

    private List<Objeto> mesa = new List<Objeto>();
    public List<Objeto> Mesa => mesa;

    public Dictionary<Objeto, int> Formas = new();

    public List<int> QuantidadeObjeto => QuantidadeObjeto;
    private int count = 0;

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

        Formas[new Circulo(new PointF(0, 0), 750)] = 5;
        Formas[new Quadrado(new PointF(100, 0), 1000)] = 5;
        Formas[new Triangulo(new PointF(200, 0), 500)] = 5;
        Formas[new Estrela(new PointF(300, 0), 200)] = 5;
        Formas[new Hexagono(new PointF(400, 0), 100)] = 5;

        foreach (var obj in Formas)
        {
            for (int i = 0; i < obj.Value; i++)
            {
                Objeto objeto = obj.Key.Clone();
                Mesa.Add(objeto);
                ObjetosJogo.Add(objeto);
            }
        }
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
        foreach (var balanca in balancas)
            balanca.Draw(g);

        foreach (var obj in ObjectManager.Objetos)
        {
            obj.Draw(g);
        }

        foreach (var type in MesaTypes)
        {
            var obj = type.Value[0];

            Font font = new Font("Arial", 15);
            SolidBrush brush = new SolidBrush(Color.Black);
            PointF center = obj.Center;
            g.DrawString(
                (
                    type.Value.Count - (ClientCursor.Objeto?.GetType() == obj.GetType() ? 1 : 0)
                ).ToString(),
                font,
                brush,
                center.X - font.Size / 2,
                center.Y - font.Size / 2
            );
        }
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
        }
    }

    public void Enviar(Panel panel, string nome, string nasc, Form form)
    {
        if (apiResponse == Respostas.Comecado)
        {
            this.result = BuildJson(panel, nome, nasc);

            MessageBox.Show(
                "O teste de verdade começa agora. Você está no nível desfaio",
                "Informações dos Inputs"
            );
            GameEngine.Current.ChangeLevel(panel, this.result);
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

        if (textboxes[0].Text == "1000")
            acertos++;
        if (textboxes[1].Text == "750")
            acertos++;
        if (textboxes[2].Text == "200")
            acertos++;
        if (textboxes[3].Text == "100")
            acertos++;

        var json = new TestResult
        {
            nome = nome,
            nascimento = nasc,
            prova1 = new Prova
            {
                triangulo = 500,
                quadrado = int.Parse(textboxes[0].Text),
                circulo = int.Parse(textboxes[1].Text),
                estrela = int.Parse(textboxes[2].Text),
                hexagono = int.Parse(textboxes[3].Text),
                tempo = (int)TestTimer.Stop().TotalSeconds,
                quantidade = Balancas.Sum(balanca => balanca.Count),
                acertos = acertos / 4
            },
            prova2 = new Prova { },
        };

        return json;
    }
}

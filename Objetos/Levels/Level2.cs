using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Level2 : IGame
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
    public Level2()
    {
        var b1 = new Balanca(0, 450);
        var b2 = new Balanca(800, 450);

        balancas[0] = b1; balancas[1] = b2;

        Formas[new Circulo(new PointF(0, 0), 600)] = 5;
        Formas[new Quadrado(new PointF(100, 0), 675)] = 5;
        Formas[new Triangulo(new PointF(200, 0), 500)] = 5;
        Formas[new Estrela(new PointF(300, 0), 50)] = 5;
        Formas[new Hexagono(new PointF(400, 0), 25)] = 5;

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
            g.DrawString((type.Value.Count - ( ClientCursor.Objeto?.GetType() == obj.GetType() ? 1 : 0)).ToString(), font, brush, center.X - font.Size / 2, center.Y - font.Size / 2);
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

            int ind = 0;
            TextBox[] textboxes = new TextBox[4]; 
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox)
                {
                    textboxes[ind] = (TextBox)control;
                    ind++;
                }
            }

            this.result.prova2 = new Prova
            {
                quadrado = int.Parse(textboxes[0].Text),
                circulo = int.Parse(textboxes[1].Text),
                estrela = int.Parse(textboxes[2].Text),
                hexagono = int.Parse(textboxes[3].Text),
            };
            
            var serialized = Json.SerializeToJson(this.result);
            await requester.PostAsync("test",  serialized);
            count++;
        }
    }
  
    public async void Enviar(Panel panel, string nome, string nasc)
    {
        if (apiResponse == Respostas.Comecado)
        {
            if (count > 0)
               return;

            int ind = 0;
            TextBox[] textboxes = new TextBox[4]; 
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox)
                {
                    textboxes[ind] = (TextBox)control;
                    ind++;
                }
            }

            this.result.prova2 = new Prova
            {
                quadrado = int.Parse(textboxes[0].Text),
                circulo = int.Parse(textboxes[1].Text),
                estrela = int.Parse(textboxes[2].Text),
                hexagono = int.Parse(textboxes[3].Text),
            };
            
            var serialized = Json.SerializeToJson(this.result);
            await requester.PostAsync("test",  serialized);
            count++;
        }
    }

}

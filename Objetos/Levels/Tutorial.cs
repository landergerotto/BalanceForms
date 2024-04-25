using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Tutorial : IGame
{
    private List<Objeto> objetosJogo = new List<Objeto>();
    public List<Objeto> ObjetosJogo => objetosJogo;


    private Balanca[] balancas = new Balanca[2];
    public Balanca[] Balancas => balancas;

    private List<Objeto> mesa = new List<Objeto>();
    public List<Objeto> Mesa => mesa;


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

    private HttpRequester requester = new("http://127.0.0.1:5000/");


    public Tutorial()
    {
        var b1 = new Balanca(0, 450);
        var b2 = new Balanca(800, 450);

        balancas[0] = b1; balancas[1] = b2;

        Formas[new Circulo(new PointF(0, 0), 500)] = 5;
        Formas[new Quadrado(new PointF(100, 0), 500)] = 5;
        Formas[new Triangulo(new PointF(200, 0), 500)] = 5;
        Formas[new Estrela(new PointF(300, 0), 500)] = 5;
        Formas[new Hexagono(new PointF(400, 0), 500)] = 5;

        foreach (var obj in Formas)
        {
            for (int i = 0; i < obj.Value; i++)
            {
                Objeto objeto = obj.Key.Clone();
                Mesa.Add(objeto);
                ObjetosJogo.Add(objeto);
            }
        }

        ObjectManager.SetList(ObjetosJogo);
    }
    public async void Update()
    {
        string a = await requester.GetAsync("test");
        var b = Json.DeserializeResponse(a);
        // MessageBox.Show(b.ToString());
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

    public void Enviar()
    {
        throw new System.NotImplementedException();
    }

}

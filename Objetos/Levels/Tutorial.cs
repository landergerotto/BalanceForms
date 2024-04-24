using System.Collections.Generic;
using System.Drawing;

public class Tutorial : IGame
{
    private List<Objeto> objetosJogo = new List<Objeto>();
    public List<Objeto> ObjetosJogo => objetosJogo;

    private Balanca[] balancas = new Balanca[2];
    public Balanca[] Balancas => balancas;

    private List<Objeto> mesa = new List<Objeto>();
    public List<Objeto> Mesa => mesa;

    public Tutorial()
    {
        var t = new Triangulo();

        var b1 = new Balanca(0, 450);
        var b2 = new Balanca(800, 450);

        balancas[0] = b1; balancas[1] = b2;

        Mesa.Add(t);
        objetosJogo.Add(t);

        ObjectManager.SetList(ObjetosJogo);
    }
    public void Update()
    {
        foreach (var balanca in balancas)
        {
            balanca.Update();
        }

        foreach (var forma in ObjetosJogo)
        {
            forma.Update();
        }
    }

    public void Draw(Graphics g)
    {
        foreach (var balanca in balancas)
            balanca.Draw(g);

        foreach (var obj in ObjectManager.Objetos)
            obj.Draw(g);
    }

    public void Enviar()
    {
        throw new System.NotImplementedException();
    }

}

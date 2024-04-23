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
        Mesa.Add(t);
        objetosJogo.Add(t);
        


        ObjectManager.SetList(ObjetosJogo);
    }

    public void Draw(Graphics g)
    {
        foreach (var obj in ObjectManager.Objetos)
            obj.Draw(g);
    }

    public void Enviar()
    {
        throw new System.NotImplementedException();
    }
}

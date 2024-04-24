using System.Drawing;
using System.Windows.Forms;

public static class ClientCursor
{
    public static Objeto Objeto { get; set; } = null;

    public static PointF position;
    public static PointF Position 
    {
        get => position; 
        set {
            if (Objeto is not null)
                Objeto.Position = new PointF(value.X - Objeto.Width / 2, value.Y - Objeto.Height / 2);
            position = value;
        }
    }

    public static void Clicar()
    {
        foreach (var obj in GameEngine.Current.JogoAtual.Mesa)
        {
            if (obj.Hitbox.Contains(Position))
                Objeto = obj;
        }
    }

    public static void Soltar()
    {
        if (Objeto is null)
            return;
        foreach (var balanca in GameEngine.Current.JogoAtual.Balancas)
            foreach (var prato in balanca.Pratos)
            {
                if (prato.Hitbox.Contains(Position))
                {
                    GameEngine.Current.JogoAtual.Mesa.Remove(Objeto);
                    prato.Objetos.Add(Objeto);
                }
            }
        Objeto = null;
    }
}
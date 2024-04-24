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
            foreach (var balanca in GameEngine.Current.JogoAtual.Balancas)
                foreach (var prato in balanca.Pratos)
                {
                    if (prato.Hitbox.Contains(Position))
                        prato.Pen = Pens.Red;
                    else
                        prato.Pen = Pens.Blue;
                }
            if (Objeto is not null)
                Objeto.Position = value;
            position = value;
        }
    }

    public static void Clicar()
    {
        
    }

    public static void Soltar()
    {

    }
}
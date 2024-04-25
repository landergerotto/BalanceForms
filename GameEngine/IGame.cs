using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public interface IGame
{
    List<Objeto> ObjetosJogo { get; }
    List<int> QuantidadeObjeto { get; }
    Balanca[] Balancas { get; }
    List<Objeto> Mesa { get; }
    void Update();
    void Draw(Graphics g);
    void Enviar(Panel panel);
}
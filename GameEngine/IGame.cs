using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public interface IGame
{
    List<Objeto> ObjetosJogo { get; }
    List<int> QuantidadeObjeto { get; }
    Balanca[] Balancas { get; }
    List<Objeto> Mesa { get; }
    TestResult result { get; set;}
    void Update(Panel panel, string nome, string nasc);
    void Draw(Graphics g);
    void Enviar(Panel panel, string nome, string nasc);
}
using System.Collections.Generic;
using System.Drawing;

public interface IGame
{
    List<Objeto> ObjetosJogo { get; }
    List<int> QuantidadeObjeto { get; }
    Balanca[] Balancas { get; }
    List<Objeto> Mesa { get; }
    void Update();
    void Draw(Graphics g);
    void Enviar();
}
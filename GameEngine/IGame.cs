using System.Collections.Generic;
using System.Drawing;

public interface IGame
{
    List<Objeto> ObjetosJogo { get; }
    Balanca[] Balancas { get; }
    List<Objeto> Mesa { get; }
    void Draw(Graphics g);
    void Enviar();
}
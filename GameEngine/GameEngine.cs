using System.Collections.Generic;
using System.Drawing;

public class GameEngine
{
    private static GameEngine current;
    public static GameEngine Current => current;

    public List<IGame> Games { get; private set; }
    public IGame JogoAtual { get; private set; }

    private GameEngine( ) { }

    public void StartUp( )
    {
        Games = new List<IGame>(){
            new Tutorial()
        };
        JogoAtual = Games[0];
    }

    public void Update( ) { }
    public void Draw (Graphics g)
        => JogoAtual.Draw(g);

    public static void New() => current = new GameEngine( );
}
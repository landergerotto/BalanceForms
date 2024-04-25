using System.Collections.Generic;
using System.Drawing;

public class GameEngine
{
    private static GameEngine current;
    public static GameEngine Current => current;

    public List<IGame> Games { get; private set; }
    public IGame JogoAtual { get; private set; }

    private int _index = 0;
    private int index
    {
        get => _index;
        set {
            _index = value;
            JogoAtual = Games[index];
            ObjectManager.SetList(JogoAtual.ObjetosJogo);
        }
    }
    private GameEngine( ) { }

    public void StartUp( )
    {
        Games = new List<IGame>(){
            new Tutorial(),
            new Level1()
        };
        JogoAtual = Games[index];
        ObjectManager.SetList(JogoAtual.ObjetosJogo);

    }
    public void ChangeLevel()
    {
        index++;
    }
    public void Update( ) 
    { 
        JogoAtual.Update( );
    }
    public void Draw (Graphics g)
        => JogoAtual.Draw(g);

    public static void New() => current = new GameEngine( );
}
public class GameEngine
{
    private static GameEngine current;
    public static GameEngine Current => current;

    private GameEngine( ) { }

    public void StartUp( ) { }

    public void Update( ) { }
    public void Draw ( ) { }

    public static void New() => current = new GameEngine( );
}
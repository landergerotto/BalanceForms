using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
        set
        {
            _index = value;
            JogoAtual = Games[index];
            ObjectManager.SetList(JogoAtual.ObjetosJogo);
        }
    }
    private GameEngine() { }

    public void StartUp()
    {
        Games = new List<IGame>(){
            new Tutorial(),
            new Level1(),
            new Level2()
        };
        JogoAtual = Games[index];
        ObjectManager.SetList(JogoAtual.ObjetosJogo);

    }
    public void ChangeLevel(Panel panel, TestResult result)
    {
        index++;
        JogoAtual.result = result;
        for (int i = 0; i < panel.Controls.Count; i++)
        {
            if (panel.Controls[i] is TextBox && i != 1)
            {
                // MessageBox.Show(i.ToString());
                ((TextBox)panel.Controls[i]).Text = "";
            }
        }
    }
    public void Update(Panel panel, string nome, string nasc)
    {
        JogoAtual.Update(panel, nome, nasc);
    }
    public void Draw(Graphics g)
        => JogoAtual.Draw(g);

    public static void New() => current = new GameEngine();
}
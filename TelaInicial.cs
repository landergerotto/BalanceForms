using System;
using System.Drawing;
using System.Windows.Forms;

public class TelaInicialForm : Form
{
    private TextBox input1;
    private TextBox input2;
    private Button startButton;

    public TelaInicialForm()
    {
        Text = "Tela Inicial";
        Size = new Size(300, 150);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.LightGray;

        Label label1 = new Label { Text = "Input 1:", AutoSize = true };

        input1 = new TextBox();

        Label label2 = new Label { Text = "Input 2:", AutoSize = true };

        input2 = new TextBox();

        startButton = new Button
        {
            Text = "Start",
            Location = new Point(110, 90),
            Size = new Size(80, 30)
        };
        startButton.Click += StartButton_Click;

        // Calcula as posições para centralizar os inputs
        int labelWidth = Math.Max(label1.Width, label2.Width);
        int inputWidth = 150;
        int startX = (ClientSize.Width - labelWidth - inputWidth - 20) / 2;

        label1.Location = new Point(startX, 20);
        input1.Location = new Point(startX + labelWidth + 5, 20);
        label2.Location = new Point(startX, 50);
        input2.Location = new Point(startX + labelWidth + 5, 50);

        Controls.Add(label1);
        Controls.Add(input1);
        Controls.Add(label2);
        Controls.Add(input2);
        Controls.Add(startButton);
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        // Aqui você pode adicionar lógica para o que acontece quando o botão "Start" é clicado
        // Por exemplo, você pode fechar esta janela e iniciar o jogo
        string input1Text = input1.Text;
        string input2Text = input2.Text;

        if (string.IsNullOrWhiteSpace(input1Text) || string.IsNullOrWhiteSpace(input2Text))
        {
            MessageBox.Show("Please fill in all fields.");
            return; // Stop further execution if validation fails
        }

        this.Hide(); // Esconde a tela inicial
        MainForm mainForm = new MainForm(input1Text, input2Text); // Cria a instância do formulário principal do jogo
        mainForm.ShowDialog(); // Mostra o formulário principal do jogo
        this.Close(); // Fecha a tela inicial quando o formulário principal do jogo é fechado
    }
}

public partial class MainForm : Form
{
    // O código original da MainForm que você forneceu permanece o mesmo
}

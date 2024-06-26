using System;
using System.Drawing;
using System.Windows.Forms;

public class CloseForm : Form
{
    private TextBox input1;
    private TextBox input2;
    private Button startButton;
    public Form ParentFormToClose { get; set; }

    public CloseForm(Form parentFormToClose)
    {
        this.ParentFormToClose = parentFormToClose;
        Text = "Tela Inicial";
        TopMost = true;
        Size = new Size(500, 250);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.LightGray;

        Label label1 = new Label { Text = "Usuário:", AutoSize = true };

        input1 = new TextBox();
        input1.Width = 250;

        Label label2 = new Label { 
            Text = "Senha:", 
            AutoSize = true };

        input2 = new TextBox{ PasswordChar = '*' };
        input2.Width = 250;
        input2.UseSystemPasswordChar = true;

        startButton = new Button
        {
            Text = "Fechar",
            Location = new Point(200, y: 150),
            Size = new Size(80, 30)
        };
        startButton.Click += StartButton_Click;

        // Calcula as posições para centralizar os inputs
        int labelWidth = Math.Max(label1.Width, label2.Width);
        int inputWidth = 340;
        int startX = (ClientSize.Width - labelWidth - inputWidth - 20) / 2;

        label1.Location = new Point(startX, 60);
        input1.Location = new Point(startX + labelWidth + 20, 60);
        label2.Location = new Point(startX, 90);
        input2.Location = new Point(startX + labelWidth + 20, 90);

        Controls.Add(label1);
        Controls.Add(input1);
        Controls.Add(label2);
        Controls.Add(input2);
        Controls.Add(startButton);

        Timer focusTimer = new Timer();
        focusTimer.Interval = 100; // Set interval to 100 milliseconds
        focusTimer.Tick += (sender, e) =>
        {
            if (!this.Focused)
            {
                this.Activate(); // Try to bring the form to the front and focus it
            }
        };
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

        string validUsername = "admin";
        string validPassword = "password";

        if (input1Text == validUsername && input2Text == validPassword)
        {
            this.ParentFormToClose?.Close(); // Close the parent form
            this.Close(); // Close the login form
        }
        else
        {
            MessageBox.Show("Invalid username or password");
        }
    }
}

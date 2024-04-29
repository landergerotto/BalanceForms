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
        Size = new Size(500, 250);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.LightGray;

        Label label1 = new Label { Text = "Nome Completo:", AutoSize = true };
        input1 = new TextBox { Width = 250, PlaceholderText = "Seu Nome" };

        Label label2 = new Label { Text = "Data de Nascimento:", AutoSize = true };
        input2 = new TextBox { Width = 250, PlaceholderText = "dd/mm/yyyy" };
        input2.Validating += new System.ComponentModel.CancelEventHandler(Input2_Validating);

        startButton = new Button
        {
            Text = "Iniciar",
            Location = new Point(200, 150),
            Size = new Size(80, 30)
        };
        startButton.Click += StartButton_Click;

        // Calculate positions to center the inputs
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
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        string input1Text = input1.Text;
        string input2Text = input2.Text;

        if (string.IsNullOrWhiteSpace(input1Text) || string.IsNullOrWhiteSpace(input2Text))
        {
            MessageBox.Show("Please fill in all fields.");
            return; // Stop further execution if validation fails
        }

        this.Hide(); // Hide the initial screen
        MainForm mainForm = new MainForm(input1Text, input2Text); // Create an instance of the main form of the game
        mainForm.ShowDialog(); // Show the main form of the game
        this.Close(); // Close the initial screen when the main game form is closed
    }

    private void Input2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (!DateTime.TryParse(input2.Text, out _))
        {
            MessageBox.Show("Please enter a valid date in the format dd/mm/yyyy.");
            e.Cancel = true; // Prevents focus from being lost if validation fails
        }
    }
}

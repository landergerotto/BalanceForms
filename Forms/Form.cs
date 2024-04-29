using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public partial class MainForm : Form
{
    private Bitmap bitmap = null;
    private Graphics g = null;
    private Timer timer;
    private PictureBox pb = new PictureBox { Dock = DockStyle.Fill };
    private Panel inputPanel;
    private PictureBox navBarPictureBox;

    private string NomeAluno;
    private string NascimentoAluno;

    public MainForm(string nome, string nascimento)
    {
        this.NomeAluno = nome;
        this.NascimentoAluno = nascimento;

        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;

        bitmap = new Bitmap(pb.Width, pb.Height);
        pb.Image = bitmap;

        this.KeyPreview = true;
        this.KeyDown += KeyboardDown;

        this.pb.MouseDown += CursorDown;
        this.pb.MouseUp += CursorUp;
        this.pb.MouseMove += CursorMove;

        timer = new Timer { Interval = 16 };

        Load += Form_Load;
        timer.Tick += Timer_Tick;

        this.KeyPreview = true;
        this.KeyDown += KeyboardDown;

        navBarPictureBox = new PictureBox
        {
            Dock = DockStyle.Top,
            Image = Image.FromFile("assets/Navbar/boschnavbar.png"),
            Height = 11,
            Width = this.Width,
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        this.Resize += MainForm_Resize;


        Controls.Add(pb); // Adiciona a PictureBox ao formulário
        Controls.Add(navBarPictureBox);

        Text = "Teixto";
        Color boschBlue = Color.FromArgb(0, 82, 155);
        Color boschYellow = Color.FromArgb(255, 204, 0);

        // Adiciona o painel para os inputs acima da PictureBox
        float width = 250;
        float height = 210;
        RectangleF inputPanelRect = ClientScreen.OnScreen(ClientScreen.Width - width, ClientScreen.Height / 2 - height / 2, width, height);
        inputPanel = new Panel
        {
            BackColor = boschBlue,
            Size = new Size((int)inputPanelRect.Width, (int)inputPanelRect.Height),
            Location = new Point((int)inputPanelRect.X, (int)inputPanelRect.Y)
        };

        // Adiciona os inputs e labels ao painel
        string[] shapes = new string[] {"▲", "■", "🟠", "★", "⬣" }; // Square, Circle, Star, Hexagon in Unicode
        float fontsize = 10 * Screen.PrimaryScreen.Bounds.Height / ClientScreen.Height;
        Font font = new Font("Arial", fontsize, FontStyle.Regular);
        SizeF maxLabelSize = Utils.MeasureText(shapes, font);
        for (int i = 0; i < shapes.Length; i++)
        {
            Label label = new Label
            {
                Text = shapes[i] + ":",
                AutoSize = true,
                Location = new Point(10, (int)(fontsize * 2 + (int)(fontsize * 2.5) * i)),
                Font = font
            };
            inputPanel.Controls.Add(label);

            TextBox textBox = new TextBox
            {
                Font = font,
                Text = i == 0 ? "500" : "",
                ReadOnly = i == 0,
                Size = new Size((int)((inputPanel.Width - maxLabelSize.Width - fontsize) * 0.8), 0),
                Location = new Point((int)(label.Location.X + maxLabelSize.Width + fontsize), label.Top)
            };
            textBox.KeyPress += TextBox_KeyPress;
            inputPanel.Controls.Add(textBox);
        }

        Size buttonSize = new Size((int)(inputPanel.Width * .75), (int)(inputPanel.Height * .17));
        Button getInfoButton = new Button
        {
            Text = "Enviar Respostas",
            Font = font,
            Size = buttonSize,
            Location = new Point(inputPanel.Width / 2 - buttonSize.Width / 2, (int)(inputPanel.Height - buttonSize.Height * 1.5)),
            BackColor = boschYellow,
            ForeColor = Color.Black
        };
        getInfoButton.Click += GetInfoButton_Click;
        inputPanel.Controls.Add(getInfoButton);

        Controls.Add(inputPanel); // Adiciona o painel ao formulário
        inputPanel.BringToFront(); // Garante que o painel esteja na frente da PictureBox
    }

    private void Form_Load(object sender, EventArgs e)
    {
        GameEngine.New();
        this.bitmap = new Bitmap(pb.Width, pb.Height);

        this.g = Graphics.FromImage(this.bitmap);
        this.g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        this.g.Clear(Color.White);
        this.pb.Image = bitmap;
        GameEngine.Current.StartUp();
        this.timer.Start();

        navBarPictureBox.Width = this.Width;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        g.Clear(Color.White);
        GameEngine.Current.Update(inputPanel, this.NomeAluno, this.NascimentoAluno);
        GameEngine.Current.Draw(g);
        pb.Refresh();
    }

    private void KeyboardDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Escape:
                CloseForm cf = new CloseForm(this);
                cf.Show();
                break;

            case Keys.ControlKey:
                break;
        }
    }

    private void GetInfoButton_Click(object sender, EventArgs e)
    {
        GameEngine.Current.JogoAtual.Enviar(inputPanel, this.NomeAluno, this.NascimentoAluno, this);
    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        // Check if the character is not a digit and not a control character (like backspace)
        if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
        {
            e.Handled = true; // Handle the event by setting Handled to true, thereby rejecting the character
        }
    }

    private void CursorMove(object sender, MouseEventArgs e)
    {
        ClientCursor.Position = ClientScreen.PositionOnScreen(e.Location);
    }

    private void CursorDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            ClientCursor.Clicar();
    }

    private void CursorUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            ClientCursor.Soltar();
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        navBarPictureBox.Width = this.Width;
        navBarPictureBox.Height = 10;
    }
}

using System;
using System.CodeDom;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;

public partial class MainForm : Form
{
    private Bitmap bitmap = null;
    private Graphics g = null;
    private Timer timer;
    private PictureBox pb = new PictureBox { Dock = DockStyle.Fill };
    public MainForm()
    {
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;

        bitmap = new Bitmap(pb.Width, pb.Height);
        pb.Image = bitmap;

        timer = new Timer { Interval = 16 };

        Load += Form_Load;
        timer.Tick += Timer_Tick;

        this.KeyPreview = true;
        this.KeyDown += KeyboardDown;

        Controls.Add(this.pb);

        Text = "Teixto";
    }

    private void Form_Load(object sender, EventArgs e)
    {
        this.bitmap = new Bitmap(pb.Width, pb.Height);

        this.g = Graphics.FromImage(this.bitmap);
        this.g.InterpolationMode = InterpolationMode.NearestNeighbor;
        this.g.Clear(Color.White);
        this.pb.Image = bitmap;
        this.timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        g.Clear(Color.White);
        // Captura o conteúdo do pb como um bitmap
        Rectangle rect = new Rectangle(0, 0, pb.Width, pb.Height);
        pb.DrawToBitmap(bitmap, rect);

        pb.Refresh();
    }

    private void KeyboardDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Escape:
                Application.Exit();
                break;

            case Keys.ControlKey:
                break;

        }
    }
}
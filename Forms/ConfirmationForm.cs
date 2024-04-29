using System;
using System.Drawing;
using System.Windows.Forms;

public class ConfirmationForm : Form
{
    private Button yesButton;
    private Button noButton;
    private Label messageLabel;

    public ConfirmationForm(string text)
    {
        Text = "Confirmation Required";
        Size = new Size(300, 150);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.LightGray;

        // Setup the message label
        messageLabel = new Label
        {
            Text = text,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 50
        };

        // Setup the yes button
        yesButton = new Button
        {
            Text = "Sim",
            DialogResult = DialogResult.Yes,
            Location = new Point(50, 70),
            Size = new Size(75, 30)
        };

        // Setup the no button
        noButton = new Button
        {
            Text = "Não",
            DialogResult = DialogResult.No,
            Location = new Point(175, 70),
            Size = new Size(75, 30)
        };

        Controls.Add(messageLabel);
        Controls.Add(yesButton);
        Controls.Add(noButton);
    }
}

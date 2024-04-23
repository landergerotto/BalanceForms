using System;
using System.Drawing;
using System.Windows.Forms;
public static class ClientScreen
{
    public static SizeF Size { get; set; } = new SizeF(1920, 1080);

    public static void DrawImage(this Graphics g, Image image, PointF position, SizeF size)
    {
        SizeF relativeSize = Utils.ProportionalSize(image.Width, image.Height, size);

        // Calculate the scaling factors for X and Y coordinates
        float scaleX = Screen.PrimaryScreen.Bounds.Width / Size.Width; // originalScreenWidth is the width of the screen when the application started
        float scaleY = Screen.PrimaryScreen.Bounds.Height / Size.Height; // originalScreenHeight is the height of the screen when the application started

        // Scale the position
        PointF scaledPosition = new PointF(position.X * scaleX, position.Y * scaleY);
        SizeF scaledSize = Utils.ProportionalSize(relativeSize.Width, relativeSize.Height, Math.Min(scaleX, scaleY));

        g.DrawImage(image, new RectangleF(scaledPosition, scaledSize));
    }

    // public static RectangleF rectOnScreen(PointF position, SizeF size)
    // {

    // }
}
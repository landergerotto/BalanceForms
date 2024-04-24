using System;
using System.Drawing;
using System.Windows.Forms;
public static class ClientScreen
{
    public static SizeF Size { get; set; } = new SizeF(1920, 1080);

    public static RectangleF OnScreen(float x, float y, float width, float height)
    {
        float scaleX = Screen.PrimaryScreen.Bounds.Width / Size.Width;
        float scaleY = Screen.PrimaryScreen.Bounds.Height / Size.Height;

        PointF scaledPosition = new PointF(x * scaleX, y * scaleY);
        SizeF scaledSize = Utils.ProportionalSize(width, height, Math.Min(scaleX, scaleY));

        return new RectangleF(scaledPosition, scaledSize);
    }
    public static RectangleF OnScreen(PointF position, SizeF size)
        => OnScreen(position.X, position.Y, size.Width, size.Height);

    public static void DrawImageOnScreen(this Graphics g, Image image, PointF position, SizeF size)
    {
        SizeF relativeSize = Utils.ProportionalSize(image.Width, image.Height, size);

        g.DrawImage(image, OnScreen(position, relativeSize));
    }

    public static void DrawRectangleOnScreen(this Graphics g, Pen Pen, RectangleF rect)
        => g.DrawRectangle(Pen, OnScreen(rect.X, rect.Y, rect.Width, rect.Height));

    public static PointF PositionOnScreen(PointF position)
    {
        float scaleX = Size.Width / Screen.PrimaryScreen.Bounds.Width;
        float scaleY = Size.Height / Screen.PrimaryScreen.Bounds.Height;

        return new PointF(position.X * scaleX, position.Y * scaleY);
    }
}
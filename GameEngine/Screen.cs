using System;
using System.Drawing;
using System.Windows.Forms;
public static class ClientScreen
{
    public static SizeF Size { get; set; } = new SizeF(1920, 1080);
    public static float Width => Size.Width;
    public static float Height => Size.Height;
    public static PointF Center => new PointF(Width / 2, Height / 2);

    public static RectangleF OnScreen(float x, float y, float width, float height)
    {
        float scaleX = Screen.PrimaryScreen.Bounds.Width / Size.Width;
        float scaleY = Screen.PrimaryScreen.Bounds.Height / Size.Height;

        PointF scaledPosition = new PointF(x * scaleX, y * scaleY);
        // SizeF scaledSize = Utils.ProportionalSize(width, height, Math.Min(scaleX, scaleY));
        SizeF scaledSize = new SizeF(width * scaleX, height * scaleY);

        return new RectangleF(scaledPosition, scaledSize);
    }
    public static RectangleF OnScreen(PointF position, SizeF size)
        => OnScreen(position.X, position.Y, size.Width, size.Height);
    public static RectangleF OnScreen(this RectangleF rect)
        => OnScreen(rect.X, rect.Y, rect.Width, rect.Height);

    public static void DrawImageOnScreen(this Graphics g, Image image, PointF position, SizeF size)
    {
        SizeF relativeSize = Utils.ProportionalSize(image.Width, image.Height, size);

        g.DrawImage(image, OnScreen(position, relativeSize));
    }

    public static void DrawRectangleOnScreen(this Graphics g, Pen Pen, RectangleF rect)
        => g.DrawRectangle(Pen, OnScreen(rect.X, rect.Y, rect.Width, rect.Height));

    public static PointF PositionOnScreen(float x, float y)
    {
        float scaleX = Size.Width / Screen.PrimaryScreen.Bounds.Width;
        float scaleY = Size.Height / Screen.PrimaryScreen.Bounds.Height;

        return new PointF(x * scaleX, y * scaleY);
    }
    public static PointF PositionOnScreen(PointF position)
        => PositionOnScreen(position.X, position.Y);

    public static SizeF SizeOnScreen(float width, float height)
    {
        float scaleX = Screen.PrimaryScreen.Bounds.Width / Size.Width;
        float scaleY = Screen.PrimaryScreen.Bounds.Height / Size.Height;

        return new SizeF(width * scaleX, height * scaleY);
    }
    public static SizeF SizeOnScreen(SizeF size)
        => SizeOnScreen(size.Width, size.Height);

    public static void DrawText(this Graphics g, string text, float x, float y, Font font, Brush brush)
    {
        float fontsize = font.Size;
        RectangleF textRect = ClientScreen.OnScreen(
            x, y, fontsize * text.Length, fontsize
        );
        font = new Font(font.Name, textRect.Height);
        g.DrawString(text, font, brush, new PointF(textRect.X, textRect.Y));
    }
    public static void DrawText(this Graphics g, string text, PointF position, Font font, Brush brush)
        => DrawText(g, text, position.X, position.Y, font, brush);
}
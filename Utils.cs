using System;
using System.Collections.Generic;
using System.Drawing;

public static class Utils
{
    public static SizeF ProportionalSize(float Width, float Height, float scale)
    {
        return new SizeF(
            Width * scale,
            Height * scale
        );
    }
    public static SizeF ProportionalSize(float Width, float Height, SizeF scaledSize)
        => ProportionalSize(Width, Height,
            Math.Min(scaledSize.Width / (float)Width, scaledSize.Height / (float)Height)
        );
    public static SizeF ProportionalSize(this Image image, float scale)
        => ProportionalSize(image.Width, image.Height, scale);
    public static SizeF ProportionalSize(this Image image, SizeF scaledSize)
        => ProportionalSize(image,
            Math.Min(scaledSize.Width / image.Width, scaledSize.Height / image.Height)
        );

    public static Bitmap FloodFill(this Bitmap bitmap, Point startPoint, Color fillColor)
    {
        Queue<Point> queue = new Queue<Point>();
        queue.Enqueue(startPoint);
        Color targetColor = bitmap.GetPixel(startPoint.X, startPoint.Y);

        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();
            if (current.X < 0 || current.Y < 0 || current.X >= bitmap.Width || current.Y >= bitmap.Height)
                continue;

            if (bitmap.GetPixel(current.X, current.Y) != targetColor)
                continue;

            bitmap.SetPixel(current.X, current.Y, fillColor);

            queue.Enqueue(new Point(current.X - 1, current.Y));
            queue.Enqueue(new Point(current.X + 1, current.Y));
            queue.Enqueue(new Point(current.X, current.Y - 1));
            queue.Enqueue(new Point(current.X, current.Y + 1));
        }

        return bitmap;
    }
}